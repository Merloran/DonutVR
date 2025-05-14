using UnityEngine;

public class PickManager : MonoBehaviour
{
    public GameObject pickSpawnPoint;
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
        var FoodItem = other.GetComponent<FoodItem>();
        if (FoodItem)
        {


        }

    }
}
