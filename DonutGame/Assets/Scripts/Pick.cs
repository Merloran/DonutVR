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
        public FoodInfo(float progress, float speed, GameObject food)
        {
            impalingProgress = progress;
            baseImpalingSpeed = speed;
            currentImpalingSpeed = speed;
            this.food = food;
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
        if (foodSlots.size < maxSlotsCount || foodSlots[maxSlotsCount - 1].impalingProgress < 1.0f)
        {
            for (int i = 0; i < foodSlots.size; ++i)
            {
                if (foodSlots[i].impalingProgress >= 1.0f)
                {
                    continue;
                }

                foodSlots[i].food.transform.position = 
                    Vector3.Lerp(transform.position,
                                 transform.position - transform.up * (1.0f - slotsOffset - slotsSpacing * i),
                                 Mathf.SmoothStep(0.0f, 1.0f, foodSlots[i].impalingProgress));

                foodSlots[i].impalingProgress = Math.Min(foodSlots[i].impalingProgress + 
                                                         foodSlots[i].currentImpalingSpeed * Time.deltaTime, 1.0f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        var foodItem = other.GetComponent<FoodItem>();
        var grab = other.GetComponent<XRGrabInteractable>();
        if (foodItem == null || 
            grab.isSelected || 
            foodSlots.size >= maxSlotsCount ||
            (foodSlots.size > 0 && foodSlots[foodSlots.size - 1].impalingProgress <= 0.0f))
        {
            return;
        }
        other.GetComponent<SphereCollider>().enabled = false;
        var rb = other.GetComponent<Rigidbody>();
        float initialSpeed = CalculateInitialImpalingSpeed(rb.linearVelocity);
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
