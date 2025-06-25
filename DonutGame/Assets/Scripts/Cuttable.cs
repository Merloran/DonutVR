using UnityEngine;

public class Cuttable : MonoBehaviour
{
    [SerializeField]
    public GameObject cutItem = null;
    [SerializeField]
    public int cutCount = 0;
    [SerializeField]
    public float itemHeight = 0;
    [SerializeField]
    public float cutSpacing = 0;
    [SerializeField]
    public Vector3 cutAxis = Vector3.up;
    public int timeToAdd = 3;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
