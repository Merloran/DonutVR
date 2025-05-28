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
    [SerializeField]
    private float minTime = 20.0f;
    [SerializeField]
    private float maxTime = 60.0f;

    [SerializeField]
    public GameObject pickSpawnPoint;
    [SerializeField]
    public GameObject pickPrefab;
    [SerializeField]
    public GameObject orderText;
    [SerializeField]
    public GameObject timerText;
    [SerializeField]
    public GameObject scoreText;
    public int finalScore = 0;
    private Order currentOrder;

    void Start()
    {
        currentOrder = new Order(maxTime, requiredItems);
        UpdateOrderText();
    }

    void Update()
    {
        currentOrder.time -= Time.deltaTime;
        UpdateTimeText();
        if (currentOrder.time <= 0f)
        {
            currentOrder = new Order(Random.Range(minTime, maxTime), requiredItems);
            UpdateOrderText();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        var pick = other.GetComponentInChildren<Pick>();
        if (!pick)
        {
            return;
        }

        FoodItem[] foodsOnPick = other.GetComponentsInChildren<FoodItem>();
        if (foodsOnPick.Count() != pick.maxSlotsCount)
        {
            return;
        }

        int score = currentOrder.foods.Count;
        for (int i = 0; i < currentOrder.foods.Count; ++i)
        {
            if (currentOrder.foods[i] != foodsOnPick[i].type)
            {
                score--;
            }

            if (foodsOnPick[i].type == Food.junk)
            {
                score--;
            }
        }

        currentOrder = new Order(Random.Range(minTime, maxTime), requiredItems);
        UpdateOrderText();
        finalScore += score;
        Destroy(pick.transform.parent.gameObject);
        UpdateScoreText();
        Instantiate(pickPrefab, pickSpawnPoint.transform);
    }

    void UpdateOrderText()
    {
        var text = orderText.GetComponent<TextMeshProUGUI>();

        string content = "Order\n";
        currentOrder.foods.Reverse();
        foreach (var food in currentOrder.foods)
        {
            content += "<sprite=" + (int)food + ">\n";
        }
        currentOrder.foods.Reverse();
        text.SetText(content);
    }
    void UpdateTimeText()
    {
        var text = timerText.GetComponent<TextMeshProUGUI>();
        text.SetText(((int)currentOrder.time).ToString());
    }

    void UpdateScoreText()
    {
        var text = scoreText.GetComponent<TextMeshProUGUI>();
        text.SetText(finalScore.ToString());
    }
}
