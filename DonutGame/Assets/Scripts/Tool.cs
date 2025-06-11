using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;

public class Tool : MonoBehaviour
{
    [SerializeField] 
    public Vector3 offsetPoint = Vector3.zero;
    public Quaternion rotation = Quaternion.identity;
    private XRGrabInteractable grab;
    private Rigidbody rb;

    public ToolHandle handle = null;

    void Awake()
    {
        grab = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        grab.selectEntered.AddListener(OnGrab);
        grab.selectExited.AddListener(OnRelease);
    }

    void OnGrab(SelectEnterEventArgs args)
    {
        rb.isKinematic = false;
        if (!handle) return;
        handle.isOccupied = false;
        handle = null;
    }

    void OnRelease(SelectExitEventArgs args)
    {
        rb.isKinematic = false;
        rb.useGravity = true;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
