using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public float cooldown = 2f;
    [SerializeField]
    public List<GameObject> foodItems;

    [SerializeField]
    public List<GameObject> spawners;

    [SerializeField]
    public float Strength = 10.0f;

    [SerializeField, UnityEngine.Range(0.0f, 180.0f), Tooltip("Vertical angle of random offset in degrees")]
    public float verticalConstraintAngle = 20.0f;
    private float verticalConstraint;

    [SerializeField, UnityEngine.Range(0.0f, 180.0f), Tooltip("Horizontal angle of random offset in degrees")]
    public float horizontalConstraintAngle = 40.0f;
    private float horizontalConstraint;

    void Start()
    {
        verticalConstraint = verticalConstraintAngle / 180.0f;
        horizontalConstraint = horizontalConstraintAngle / 180.0f;
        StartCoroutine("LaunchFruit");
    }

    void Update()
    {

    }

    private IEnumerator LaunchFruit()
    {
        while (true)
        {
            var spawner = spawners[Random.Range(0, spawners.Count)];
            var fruitPrefab = foodItems[Random.Range(0, foodItems.Count)];
            var fruit = Instantiate(fruitPrefab, spawner.transform.position, spawner.transform.rotation);

            Vector3 randomOffset = spawner.transform.right * Random.Range(-verticalConstraint, verticalConstraint)
                                 + spawner.transform.up * Random.Range(-horizontalConstraint, horizontalConstraint);

            Vector3 direction = (spawner.transform.forward + Vector3.up + randomOffset).normalized;

            fruit.GetComponent<Rigidbody>().AddForce(direction * Strength, ForceMode.Impulse);
            yield return new WaitForSeconds(cooldown);
        }
    }
}
