using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> FoodItems;

    [SerializeField]
    public List<GameObject> Spawners;

    [SerializeField]
    public float Strength = 10.0f;
    void Start()
    {
        StartCoroutine("LaunchFruit");
    }

    void Update()
    {

    }

    private IEnumerator LaunchFruit()
    {
        for (; ; )
        {
            var spawner = Spawners[Random.Range(0, Spawners.Count)];

            var fruit = Instantiate(FoodItems[0], spawner.transform);
            fruit.GetComponent<Rigidbody>().AddForce((spawner.transform.forward + spawner.transform.up).normalized * Strength, ForceMode.Impulse);
            yield return new WaitForSeconds(5.0f);
            //Destroy(fruit);
        }
    }
}
