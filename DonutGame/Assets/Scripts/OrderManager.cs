using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


public struct Order
{
    public List<Food> foods;
    public float time;

    public Order(float time)
    {
        foods = new List<Food>();
        this.time = time;
        int foodCount = System.Enum.GetValues(typeof(Food)).Length;
        for (int i = 0; i < 4; i++)
        {
            foods.Add((Food)Random.Range(0, foodCount - 1));
        }

    }
};
public class OrderManager : MonoBehaviour
{
    public GameObject pickSpawnPoint;
    public GameObject pickPrefab;
    public GameObject text;
    public int finalScore = 0;

    List<Order> currentOrders = new List<Order>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Order testOrder = new Order(10f);

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
        Debug.Log(other.name);
        var pick = other.GetComponent<Pick>();
        if (pick)
        {
            var foodsOnPick = other.GetComponentsInChildren<FoodItem>();
            //assuming all orders should have 4 elements
            if (foodsOnPick.Count() != 4)
                return;
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
}
