using UnityEngine;

public class FoodItem : MonoBehaviour
{
    public int points = 0;
    public float lifeTime = 5f;
    public bool toDestroy = true;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (toDestroy)
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime <= 0)
            {
                Destroy(this);
            }
        }

    }
}
