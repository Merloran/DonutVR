using UnityEngine;

public class FloorChecker : MonoBehaviour
{
    [Tooltip("Destroy prefab")]
    public GameObject vfxEffectPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // SprawdŸ, czy warstwa obiektu to "Floor"
        if (collision.gameObject.layer == LayerMask.NameToLayer("floor"))
        {
            // Pobierz punkt kontaktu
            Vector3 hitPoint = collision.contacts[0].point;

            // Utwórz efekt w miejscu kontaktu
            if (vfxEffectPrefab != null)
            {
                Instantiate(vfxEffectPrefab, hitPoint, Quaternion.identity);
            }

            // (opcjonalnie) zniszcz prefab po zderzeniu
            Destroy(gameObject);
        }
    }
}
