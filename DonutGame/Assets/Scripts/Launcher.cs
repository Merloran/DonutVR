using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum Difficulty
{
    Easy,
    Medium,
    Hard
}

[System.Serializable]
public class DifficultySettings
{
    public Difficulty level;
    public float cooldown = 2f;
    public float strength = 10f;

    [Range(0.0f, 180.0f), Tooltip("Vertical angle of random offset in degrees")]
    public float verticalConstraintAngle = 20f;

    [Range(0.0f, 180.0f), Tooltip("Horizontal angle of random offset in degrees")]
    public float horizontalConstraintAngle = 40f;
}

public class Launcher : MonoBehaviour
{
    [Header("Difficulty Settings")]
    [SerializeField]
    private List<DifficultySettings> difficultyPresets;

    [Header("Spawner and Items")]
    [SerializeField]
    private List<GameObject> foodItems;

    [SerializeField]
    private List<GameObject> spawners;

    private Difficulty currentDifficulty;
    private DifficultySettings currentSettings;

    private bool activeLauncher = false;

    private float verticalConstraint;
    private float horizontalConstraint;

    void Start()
    {
        // Ustaw domyślną trudność, np. Easy
        setDifficulty(Difficulty.Hard);
    }

    public void setActiveLauncer(bool set)
    {
        activeLauncher = set;

        if (activeLauncher)
            StartCoroutine(LaunchFruit());
        else
            StopAllCoroutines();
    }

    public void setDifficulty(Difficulty level)
    {
        currentDifficulty = level;
        currentSettings = difficultyPresets.Find(p => p.level == level);

        if (currentSettings == null)
        {
            Debug.LogWarning($"Brak ustawień dla poziomu trudności: {level}. Launcher nie będzie działał poprawnie.");
            return;
        }

        verticalConstraint = currentSettings.verticalConstraintAngle / 180.0f;
        horizontalConstraint = currentSettings.horizontalConstraintAngle / 180.0f;
    }

    private IEnumerator LaunchFruit()
    {
        while (activeLauncher && currentSettings != null)
        {
            var spawner = spawners[Random.Range(0, spawners.Count)];
            var fruitPrefab = foodItems[Random.Range(0, foodItems.Count)];
            var fruit = Instantiate(fruitPrefab, spawner.transform.position, spawner.transform.rotation);

            Vector3 randomOffset = spawner.transform.right * Random.Range(-verticalConstraint, verticalConstraint)
                                 + spawner.transform.up * Random.Range(-horizontalConstraint, horizontalConstraint);

            Vector3 direction = (spawner.transform.forward + Vector3.up + randomOffset).normalized;

            fruit.GetComponent<Rigidbody>().AddForce(direction * currentSettings.strength, ForceMode.Impulse);
            yield return new WaitForSeconds(currentSettings.cooldown);
        }
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        // Sprawdzenie duplikatów poziomów trudności w edytorze
        if (difficultyPresets != null)
        {
            var duplicates = difficultyPresets
                .GroupBy(d => d.level)
                .Where(g => g.Count() > 1)
                .ToList();

            if (duplicates.Count > 0)
            {
                Debug.LogWarning("Zduplikowane poziomy trudności w difficultyPresets!");
            }
        }
    }
#endif
}
