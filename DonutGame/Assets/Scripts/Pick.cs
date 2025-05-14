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
            other.transform.position = new Vector3(this.transform.position.x, this.transform.position.y - 0.8f + (0.2f * itemNumber), this.transform.position.z);
            foodItem.toDestroy = false;
            other.transform.SetParent(this.transform);
            itemNumber++;

        }

    }
}
