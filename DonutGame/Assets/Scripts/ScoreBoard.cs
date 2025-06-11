using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    private float fullTime = 0.0f;
    public float currentTime = 120.0f;
    private TextMeshProUGUI text;

    public GameObject endMenu;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            fullTime += Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            currentTime = 0.0f;
            endMenu.SetActive(true);
            int minutes = Mathf.FloorToInt(fullTime / 60f);
            int seconds = Mathf.FloorToInt(fullTime % 60f);
            endMenu.GetComponentInChildren<TextMeshProUGUI>().text = $"Nice Work!\n\n\n\n{minutes:00}:{seconds:00}";
        }
    }

    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        text.text = $"{minutes:00}:{seconds:00}";
    }
}
