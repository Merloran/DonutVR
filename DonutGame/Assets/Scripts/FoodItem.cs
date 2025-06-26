using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public enum Food
{
    pepper,
    cucumber,
    tomato,
    steak,
    cheese,
    onion,
    pineapple,
    junk
};

public class FoodItem : MonoBehaviour
{
    public GameObject particles = null;
    public int points = 0;
    public float lifeTime = 5f;
    public bool toDestroy = true;

    public Food type;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (toDestroy == false || gameObject.GetComponent<XRGrabInteractable>().isSelected)
        {
            return;
        }

        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void PlayParticles()
    {
        if (!particles) return;
        Instantiate(particles, gameObject.transform.position, gameObject.transform.rotation);
    }
}
