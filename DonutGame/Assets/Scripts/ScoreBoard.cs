using Microsoft.MixedReality.Toolkit.Experimental.UI;
using TMPro;
using UnityEngine;

public class ScoreBoard : MonoBehaviour
{
    private float fullTime = 0.0f;
    public float currentTime = 120.0f;
    private TextMeshProUGUI text;

    public bool isPlaying;
    public GameObject endMenu;
    public GameObject gameManager;
    public float distance = 2.0f;
    public GameObject camera;
    public GameObject keyboardPrefab;
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
            keyboardPrefab.SetActive(true);
            //endMenu.GetComponentInChildren<TextMeshProUGUI>().text = $"Nice Work!\n\n\n\n{minutes:00}:{seconds:00}";
            endMenu.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z) + camera.GetComponent<Camera>().transform.forward.normalized * distance;
            endMenu.GetComponentInChildren<NonNativeKeyboard>().PresentKeyboard();
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
