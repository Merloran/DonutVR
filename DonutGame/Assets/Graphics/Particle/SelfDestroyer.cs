using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
    public float lifeTime =1.0f;

    float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = lifeTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer < 0)
        {
            Destroy(gameObject);
        }
       
    }
}
