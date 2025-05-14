using UnityEngine;

public class Pick : MonoBehaviour
{
    int itemNumber = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
        var foodItem = other.GetComponent<FoodItem>();
        if (foodItem)
        {
            if (itemNumber >= 4)
            {
                return;
            }
            other.GetComponent<SphereCollider>().enabled = false;
            other.GetComponent<Rigidbody>().isKinematic = true;
            other.transform.position = this.transform.position - this.transform.up * (0.8f - 0.2f * itemNumber);
            foodItem.toDestroy = false;
            other.transform.SetParent(this.transform);
            itemNumber++;

        }

    }
}
