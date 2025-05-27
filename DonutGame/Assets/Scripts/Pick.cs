using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit.Interactables;


public class Pick : MonoBehaviour
{
    struct FoodInfo
    {
        public float impalingProgress;
        public float currentImpalingSpeed;
        public GameObject food;
        public FoodInfo(float progress, float speed, GameObject food)
        {
            impalingProgress = progress;
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
        if (foodItem == null || grab.isSelected || foodSlots.size >= maxSlotsCount)
        {
            return;
        }
        other.GetComponent<SphereCollider>().enabled = false;
        var rb = other.GetComponent<Rigidbody>();
        // float speed = CalculateEffectiveSpeed(rb.linearVelocity);
        rb.isKinematic = true;
        foodItem.toDestroy = false;
        other.transform.SetParent(transform);
        foodSlots.Add(new FoodInfo(0.0f, 1.0f, other.gameObject));
    }

}
