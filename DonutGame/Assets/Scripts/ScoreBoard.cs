using Microsoft.MixedReality.Toolkit.Experimental.UI;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    public float fullTime = 0.0f;
    public float currentTime = 120.0f;
    private TextMeshProUGUI text;

    public bool isPlaying;
    public GameObject endMenu;
    public GameObject gameManager;
    public float distance = 2.0f;
    
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
            
            int minutes = Mathf.FloorToInt(fullTime / 60f);
            int seconds = Mathf.FloorToInt(fullTime % 60f);
            gameManager.GetComponent<GameManager>().endGame();
        }
    }

    public float getScore()
    {
        return fullTime;
    }
    private void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(currentTime / 60f);
        int seconds = Mathf.FloorToInt(currentTime % 60f);
        text.text = $"{minutes:00}:{seconds:00}";
    }
}
