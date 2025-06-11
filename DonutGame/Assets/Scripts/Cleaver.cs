using UnityEngine;

public class Cleaver : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Cuttable cuttable = other.gameObject.GetComponent<Cuttable>();
        if (!cuttable)
        {
            return;
        }

        Transform itemTransform = other.gameObject.transform;

        Vector3 bottom = itemTransform.position - itemTransform.up * cuttable.itemHeight * 0.5f;
        Vector3 offset = itemTransform.up * (cuttable.cutSpacing + cuttable.itemHeight / cuttable.cutCount);
        Vector3 velocity = other.gameObject.GetComponent<Rigidbody>().linearVelocity;
        Vector3 angularVelocity = other.gameObject.GetComponent<Rigidbody>().angularVelocity;
        for (int i = 0; i < cuttable.cutCount; ++i)
        {
            GameObject spawned = Instantiate(cuttable.cutItem, bottom + offset * i, itemTransform.rotation);
            Rigidbody spawnedBody = spawned.GetComponent<Rigidbody>();
            spawnedBody.linearVelocity = velocity;
            spawnedBody.angularVelocity = angularVelocity;
        }
        Destroy(other.gameObject);
    }
}
