
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject cannons;
    public GameObject score;

    public GameObject order1;
    public GameObject order2;
    public GameObject knife;
    public GameObject stick1;
    public GameObject stick2;

    public GameObject tutorialButton;
    public GameObject easyButton;
    public GameObject mediumButton;
    public GameObject hardButton;
    public GameObject startButton;
    public GameObject scoreBoardbutton;


    public GameObject endMenu;

    public GameObject menu;
    private Vector3 knifeStartPos;
    private Quaternion knifeStartRot;
    private Vector3 stick1StartPos;
    private Quaternion stick1StartRot;
    private Vector3 stick2StartPos;
    private Quaternion stick2StartRot;

    void Start()
    {
        // Zapisz pocz¹tkowe pozycje
        knifeStartPos = knife.transform.position;
        knifeStartRot = knife.transform.rotation;
        stick1StartPos = stick1.transform.position;
        stick1StartRot = stick1.transform.rotation;
        stick2StartPos = stick2.transform.position;
        stick2StartRot = stick2.transform.rotation;
    }

    public void StartGame()
    {
        // Aktywuj elementy
        cannons.GetComponent<Launcher>().setActiveLauncer(true);
        score.SetActive(true);
        order1.SetActive(true);
        order2.SetActive(true);
        menu.SetActive(false);

        // Przywróæ pozycje
        knife.transform.position = knifeStartPos;
        knife.transform.rotation = knifeStartRot;
        stick1.transform.position = stick1StartPos;
        stick1.transform.rotation = stick1StartRot;
        stick2.transform.position = stick2StartPos;
        stick2.transform.rotation = stick2StartRot;
    }


    public void setTutorial()
    {
        // cannons.GetComponent<Launcher>().setDifficulty()
        StartGame();
    }
    public void setEasyMode()
    {
        cannons.GetComponent<Launcher>().setDifficulty(Difficulty.Easy);
        StartGame();
    }
    public void setMediumMode()
    {
        cannons.GetComponent<Launcher>().setDifficulty(Difficulty.Medium);
        StartGame();
    }
    public void setHardMode()
    {
        cannons.GetComponent<Launcher>().setDifficulty(Difficulty.Hard);
        StartGame();
    }

    public void setEndLevel()
    {

    }
    public void chooseDifficulty()
    {

        tutorialButton.SetActive(true);
        easyButton.SetActive(true);
        mediumButton.SetActive(true);
        hardButton.SetActive(true);
        startButton.SetActive(false);
        scoreBoardbutton.SetActive(false);

    }

    void Update()
    {


    }
}

  

   
