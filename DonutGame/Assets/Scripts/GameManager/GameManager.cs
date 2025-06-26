
using System.Collections.Generic;
using System.Linq;
using Microsoft.MixedReality.Toolkit.Experimental.UI;
using TMPro;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

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
    public GameObject scoreeasyButton;
    public GameObject mediumButton;
    public GameObject scoremediumButton;
    public GameObject hardButton;
    public GameObject scorehardButton;
    public GameObject startButton;
    public GameObject scoreBoardbutton;


    public GameObject endMenu;
    public GameObject scoremenu;
    public GameObject scoreText;

    public GameObject menu;
    private Vector3 knifeStartPos;
    private Quaternion knifeStartRot;
    private Vector3 stick1StartPos;
    private Quaternion stick1StartRot;
    private Vector3 stick2StartPos;
    private Quaternion stick2StartRot;
    Difficulty currentDifficulty;
   
    public GameObject playerName;
    public GameObject backButton;
    public GameObject camera;
    public GameObject keyboardPrefab;
    float distance = 2.0f;

    private Dictionary<Difficulty, SortedDictionary<int, string>> highscores;

    private const int maxScores = 10;

    public GameObject hsmanager;
    void Start()
    {
        highscores = new Dictionary<Difficulty, SortedDictionary<int, string>>
        {
            { Difficulty.Easy, new SortedDictionary<int, string>(new DescendingComparer<int>()) },
            { Difficulty.Medium, new SortedDictionary<int, string>(new DescendingComparer<int>()) },
            { Difficulty.Hard, new SortedDictionary<int, string>(new DescendingComparer<int>()) }
        };
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
        easyButton.SetActive(false);
        mediumButton.SetActive(false);
        hardButton.SetActive(false);
        tutorialButton.SetActive(false);
        startButton.SetActive(true);
        scoreBoardbutton.SetActive(true);

        ScoreBoard points = score.GetComponent<ScoreBoard>();
        if (points)
        {
            points.currentTime = 180.0f;
            points.fullTime = 0.0f;
        }

        // Przywróæ pozycje
        knife.transform.position = knifeStartPos;
        knife.transform.rotation = knifeStartRot;
        stick1.transform.position = stick1StartPos;
        stick1.transform.rotation = stick1StartRot;
        stick2.transform.position = stick2StartPos;
        stick2.transform.rotation = stick2StartRot;
    }

    public void showEasyScoreBoard()
    {
        ShowScoreForDifficulty(Difficulty.Easy);
    }

    public void showMediumScoreBoard()
    {
        ShowScoreForDifficulty(Difficulty.Medium);
    }

    public void showHardScoreBoard()
    {
        ShowScoreForDifficulty(Difficulty.Hard);
    }

    private void ShowScoreForDifficulty(Difficulty difficulty)
    {
       
        scoreText.SetActive(true);
        var scores = GetScoresForDifficulty(difficulty);

        string result = $"<b>{difficulty} Highscores:</b>\n";
        foreach (var entry in scores)
        {
            result += $"{entry.Value} - {entry.Key}\n";
        }

        scoreText.GetComponent<TMP_Text>().text = result;
    }

    public IEnumerable<KeyValuePair<int, string>> GetScoresForDifficulty(Difficulty difficulty)
    {
        if (highscores.ContainsKey(difficulty))
            return highscores[difficulty];
        else
            return Enumerable.Empty<KeyValuePair<int, string>>();
    }


    public void backToMenu()
    {
        scoreeasyButton.SetActive(false);
        scoremediumButton.SetActive(false);
        scorehardButton.SetActive(false);
    }
    public void setTutorial()
    {
        // cannons.GetComponent<Launcher>().setDifficulty()
        StartGame();
    }
    public void setEasyMode()
    {
        cannons.SetActive(true);
        currentDifficulty = Difficulty.Easy;
        cannons.GetComponent<Launcher>().setDifficulty(Difficulty.Easy);
        StartGame();
    }
    public void setMediumMode()
    {
        cannons.SetActive(true);
        currentDifficulty = Difficulty.Medium;
        cannons.GetComponent<Launcher>().setDifficulty(Difficulty.Medium);
        StartGame();
    }
    public void setHardMode()
    {
        cannons.SetActive(true);
        currentDifficulty =Difficulty.Hard;
        cannons.GetComponent<Launcher>().setDifficulty(Difficulty.Hard);
        StartGame();
    }

    public void setEndLevel()
    {

    }
    public void chooseDifficulty()
    {

        tutorialButton.SetActive(false);
        easyButton.SetActive(true);
        mediumButton.SetActive(true);
        hardButton.SetActive(true);
        startButton.SetActive(false);
        scoreBoardbutton.SetActive(false);

    }

    public void chooseScoreDifficulty()
    {


        scoreeasyButton.SetActive(true);
        scoremediumButton.SetActive(true);
        scorehardButton.SetActive(true);
        startButton.SetActive(false);
        scoreBoardbutton.SetActive(false);

    }

    public void endGame()
    {
        keyboardPrefab.SetActive(true);
        endMenu.SetActive(true);
        //endMenu.GetComponentInChildren<TextMeshProUGUI>().text = $"Nice Work!\n\n\n\n{minutes:00}:{seconds:00}";
        endMenu.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z) + camera.GetComponent<Camera>().transform.forward.normalized * distance;
        endMenu.GetComponentInChildren<NonNativeKeyboard>().PresentKeyboard();
        cannons.SetActive(false);
        score.SetActive(false);
        order1.SetActive(false);
        order2.SetActive(false);
        menu.SetActive(true);
    }

    public void checkScore()
    {
        
        if (scoremenu == null)
        {
            Debug.LogError("scoremenu is null!");
        }
        else if (scoremenu.GetComponent<ScoreBoard>() == null)
        {
            Debug.LogError("ScoreBoard component on scoremenu is null!");
        }
        else
        {
            Debug.Log($"fullTime = {scoremenu.GetComponent<ScoreBoard>().fullTime}");
        }

        if (currentDifficulty == null)
        {
            Debug.LogError("aaaaa!");
        }

        if (playerName == null)
        {
            Debug.LogError("playerName is null!");
        }
        else if (playerName.GetComponent<TMPro.TMP_InputField>() == null)
        {
            Debug.LogError("TMP_InputField component on playerName is null!");
        }
        else
        {
            Debug.Log($"Entered player name: {playerName.GetComponent<TMPro.TMP_InputField>().text}");
        }
        AddScore(currentDifficulty, (int)scoremenu.GetComponent<ScoreBoard>().fullTime, playerName.GetComponent<TMP_InputField>().text);   
        endMenu.SetActive(false);  
    }

    public void backToMenufromScoreboard()
    {
        scoreText.SetActive(false);
        startButton.SetActive(true);
        scoreeasyButton.SetActive(true);
        backButton.SetActive(false);
    } 

    public void AddScore(Difficulty difficulty, int score, string playerName)
    {
        var table = highscores[difficulty];

        // Dodaj wynik, jeœli mniej ni¿ 10
        if (table.Count < maxScores)
        {
            table[score] = playerName;
        }
        else
        {
            // Jeœli nowy wynik lepszy ni¿ najgorszy, zast¹p
            int worstScore = table.Keys.Last(); // najmniejszy wynik w posortowanej tablicy
            if (score > worstScore)
            {
                table.Remove(worstScore);
                table[score] = playerName;
            }
        }
    }

    public void PrintHighscores(Difficulty difficulty)
    {
        Debug.Log($"Highscores - {difficulty}:");
        foreach (var entry in highscores[difficulty])
        {
            Debug.Log($"{entry.Value}: {entry.Key}");
        }
    }

    // W³asny komparator malej¹cy
    private class DescendingComparer<T> : IComparer<T> where T : System.IComparable<T>
    {
        public int Compare(T x, T y)
        {
            return y.CompareTo(x); // od najwiêkszego do najmniejszego
        }
    }
    void Update()
    {


    }
}



