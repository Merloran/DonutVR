using UnityEngine;

public class PlayerCollider : MonoBehaviour
{
    [SerializeField]
    private Transform head;
    [SerializeField]
    private Transform floor;

    private CapsuleCollider collider;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        float height = head.position.y - floor.position.y;
        collider.height = height;
        transform.position = head.position - Vector3.up * height * 0.5f;
    }
}
