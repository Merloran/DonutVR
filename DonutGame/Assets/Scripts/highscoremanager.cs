using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HighscoreManager : MonoBehaviour
{


    // Mapy dla ka¿dego poziomu trudnoœci
    private Dictionary<Difficulty, SortedDictionary<int, string>> highscores;

    private const int maxScores = 10;
    private void Start()
    {
        highscores = new Dictionary<Difficulty, SortedDictionary<int, string>>
        {
            { Difficulty.Easy, new SortedDictionary<int, string>(new DescendingComparer<int>()) },
            { Difficulty.Medium, new SortedDictionary<int, string>(new DescendingComparer<int>()) },
            { Difficulty.Hard, new SortedDictionary<int, string>(new DescendingComparer<int>()) }
        };

    }
    void Awake()
    {
   

       
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
}
