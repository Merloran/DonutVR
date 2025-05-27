using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public struct Order
{
    public List<Food> foods;
    public float time;

    public Order(float time, int itemsCount)
    {
        foods = new List<Food>();
        this.time = time;
        int foodCount = System.Enum.GetValues(typeof(Food)).Length;
        for (int i = 0; i < itemsCount; i++)
        {
            foods.Add((Food)Random.Range(0, foodCount - 1));
        }

    }
};
public class OrderManager : MonoBehaviour
{
    [SerializeField]
    private int requiredItems = 6;
    public GameObject pickSpawnPoint;
    public GameObject pickPrefab;
    public GameObject text;
    public int finalScore = 0;

    List<Order> currentOrders = new();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Order testOrder = new Order(10f, requiredItems);

        currentOrders.Add(testOrder);

        foreach (var order in testOrder.foods)
        {
            Debug.Log(order);
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < currentOrders.Count; i++)
        {
            var order = currentOrders[i];
            order.time -= Time.deltaTime;
            currentOrders[i] = order;
            //manage expiring orders
            if (currentOrders[0].time <= 0f)
            {
            }
        }


    }

    void OnTriggerEnter(Collider other)
    {
        var pick = other.GetComponentInChildren<Pick>();
        if (!pick)
        {
            return;
        }

        var foodsOnPick = other.GetComponentsInChildren<FoodItem>();
        if (foodsOnPick.Count() != pick.maxSlotsCount)
        {
            return;
        }

        foreach (var order in currentOrders)
        {
            bool correct = true;
            for (int i = 0; i < order.foods.Count; i++)
            {
                if (order.foods[i] != foodsOnPick[i].type)
                {
                    correct = false;
                }
            }
            if (correct == true)
            {
                int score = 0;
                foreach (var food in foodsOnPick)
                {
                    score += food.points;
                }
                Debug.Log(score);
                finalScore += score;
                text.GetComponent<TextMeshProUGUI>().SetText(finalScore.ToString());
                Destroy(pick.transform.parent.gameObject);
                Instantiate(pickPrefab, pickSpawnPoint.transform);
                return;
            }
        }
    }
}
