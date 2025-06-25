using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public class Pick : MonoBehaviour
{
    struct FoodInfo
    {
        public float impalingProgress;
        public float currentImpalingSpeed;
        public float baseImpalingSpeed;
        public GameObject food;
        public bool isSlidingOff;
        public float slideProgress;
        public float slideDuration;

        public FoodInfo(float progress, float speed, GameObject food)
        {
            impalingProgress = progress;
            baseImpalingSpeed = speed;
            currentImpalingSpeed = speed;
            this.food = food;
            isSlidingOff = false;
            slideProgress = 0f;
            slideDuration = 1f;
        }
    }

    [SerializeField]
    float slotsOffset = 0.2f;
    float slotsSpacing = 0f;
    [SerializeField]
    public int maxSlotsCount = 4;
    private DynamicArray<FoodInfo> foodSlots = new();


    void Start()
    {
        slotsSpacing = (1.0f - slotsOffset) / maxSlotsCount;
        foodSlots.Reserve(maxSlotsCount);
    }

    void Update()
    {
        UpdateImpalingSpeed();

        float zRot = transform.eulerAngles.z;
        float xRot = transform.eulerAngles.x;
        bool shouldSlideOff = (zRot >= 140f && zRot <= 220f) && (xRot >= 320f || xRot <= 40f);

        for (int i = 0; i < foodSlots.size; ++i)
        {
            var info = foodSlots[i];

            if (info.isSlidingOff)
            {
                // Im ni¿szy index, tym d³u¿ej spada
                float indexFactor = (float)(foodSlots.size - 1 - i) / Mathf.Max(1, foodSlots.size - 1); // 0 dla najwy¿szego, 1 dla najni¿szego
                info.slideDuration = Mathf.Lerp(0.2f, 1.2f, indexFactor); // przyk³adowe czasy zsuwania

                info.slideProgress += Time.deltaTime / info.slideDuration;
                float slideT = Mathf.SmoothStep(0f, 1f, info.slideProgress);

                Vector3 startPos = transform.position - transform.up * (1.0f - slotsOffset - slotsSpacing * i);
                Vector3 endPos = startPos + transform.up * 0.3f; // ruch w górê w lokalnych, ale mo¿e trzeba -transform.up (patrz ni¿ej)

                info.food.transform.position = Vector3.Lerp(startPos, endPos, slideT);

                if (info.slideProgress >= 1f)
                {
                    // opcjonalnie: odpinanie i fizyka
                    var rb = info.food.GetComponent<Rigidbody>();
                    if (rb != null)
                    {
                        rb.isKinematic = false;
                        rb.linearVelocity = Vector3.zero;
                        info.food.transform.SetParent(null);
                    }

                    foodSlots.RemoveAt(i);
                    --i;
                    continue;
                }

                foodSlots[i] = info;
                continue;
            }


            if (info.impalingProgress < 1.0f)
            {
                float t = Mathf.Clamp01(info.impalingProgress); // zamiast SmoothStep
                Vector3 endPos = transform.position - transform.up * (1.0f - slotsOffset - slotsSpacing * i);
                info.food.transform.position = Vector3.Lerp(transform.position, endPos, t);

                info.impalingProgress = Mathf.Min(info.impalingProgress +
                                                  info.currentImpalingSpeed * Time.deltaTime, 1.0f);
            }

            if (shouldSlideOff && info.impalingProgress >= 1.0f)
            {
                info.isSlidingOff = true;
                info.slideDuration = 0.5f + 0.3f * i;
                info.slideProgress = 0f;
            }

            foodSlots[i] = info;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        var foodItem = other.GetComponent<FoodItem>();
        var grab = other.GetComponent<XRGrabInteractable>();
        float angle = Vector3.Angle(transform.up, Vector3.up);
        if (foodItem == null ||
            grab.isSelected ||
            foodSlots.size >= maxSlotsCount ||
            (foodSlots.size > 0 && foodSlots[foodSlots.size - 1].impalingProgress <= 0.2f) ||
            angle > 90.0f)
        {
            return;
        }
        other.GetComponent<SphereCollider>().enabled = false;
        var rb = other.GetComponent<Rigidbody>();
        float initialSpeed = CalculateInitialImpalingSpeed(rb.linearVelocity);
        foodItem.PlayParticles();
        rb.isKinematic = true;
        foodItem.toDestroy = false;
        other.transform.SetParent(transform);
        foodSlots.Add(new FoodInfo(0.0f, initialSpeed, other.gameObject));
    }

    private float CalculateInitialImpalingSpeed(Vector3 velocity)
    {
        float speedAlongAxis = Vector3.Dot(velocity, -transform.up);
        speedAlongAxis = Mathf.Max(0f, speedAlongAxis);
        float normalizedSpeed = Mathf.Lerp(0.2f, 3.0f, Mathf.InverseLerp(0f, 5f, speedAlongAxis));

        return Mathf.Clamp(normalizedSpeed, 0.2f, 3.0f);
    }


    private void UpdateImpalingSpeed()
    {
        for (int i = 0; i < foodSlots.size; ++i)
        {
            if (foodSlots[i].impalingProgress >= 1.0f)
            {
                continue;
            }

            var currentFood = foodSlots[i];
            float slowdownMultiplier = CalculateSlowdownMultiplier(i);
            float angle = Vector3.Angle(transform.up, Vector3.up);
            currentFood.currentImpalingSpeed = currentFood.baseImpalingSpeed * slowdownMultiplier * (angle <= 45f ? 1.0f : 0.0f);

            foodSlots[i] = currentFood;
        }
    }

    private float CalculateSlowdownMultiplier(int currentIndex)
    {
        float multiplier = 1.0f;

        for (int i = currentIndex + 1; i < foodSlots.size; ++i)
        {
            if (foodSlots[i].impalingProgress < 1.0f)
            {
                float elementInfluence = 1.0f - foodSlots[i].impalingProgress;
                multiplier *= Mathf.Lerp(1.0f, 0.7f, elementInfluence);
            }
        }

        return Mathf.Clamp(multiplier, 0.1f, 1.0f);
    }
}
