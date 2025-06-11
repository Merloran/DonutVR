using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class ToolHandle : MonoBehaviour
{
    public bool isOccupied = false;
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
        Tool tool = other.gameObject.GetComponent<Tool>();
        Rigidbody body = other.gameObject.GetComponent<Rigidbody>();
        XRGrabInteractable grab = other.gameObject.GetComponent<XRGrabInteractable>();
        if (isOccupied || !tool || !body || !grab || grab.isSelected)
        {
            return;
        }

        isOccupied = true;
        Transform otherTransform = other.gameObject.transform;

        otherTransform.position = gameObject.transform.position + tool.offsetPoint;
        otherTransform.rotation = tool.rotation;
        body.isKinematic = true;
        body.useGravity = false;
        tool.handle = this;
    }
}
