using UnityEngine;

public class PickManager : MonoBehaviour
{
    public GameObject pickSpawnPoint;
    public GameObject pickPrefab;
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
        var pick = other.GetComponent<Pick>();
        if (pick)
        {
            var foods = GetComponentsInChildren<FoodItem>();
            int score = 0;
            foreach (var food in foods)
            {
                score += food.points;
            }
            Debug.Log(score);
            Destroy(pick.transform.parent.gameObject);
            Instantiate(pickPrefab, pickSpawnPoint.transform);
        }

    }
}
