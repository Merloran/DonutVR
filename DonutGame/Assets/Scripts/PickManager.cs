using UnityEngine;

public class PickManager : MonoBehaviour
{
    public GameObject pickSpawnPoint;
    public GameObject pickPrefab;
    public int finalScore = 0;
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
            var foods = other.GetComponentsInChildren<FoodItem>();
            int score = 0;
            foreach (var food in foods)
            {
                score += food.points;
            }
            Debug.Log(score);
            finalScore += score;
            Destroy(pick.transform.parent.gameObject);
            Instantiate(pickPrefab, pickSpawnPoint.transform);
        }

    }
}
