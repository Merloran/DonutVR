using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public Transform playerHead; // np. Camera.main.transform
    public float distance = 2f; // odleg³oœæ od twarzy
    public Vector3 offset = Vector3.zero; // odleg³oœæ od twarzy
    bool isFirstFrame = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ShowUI();
    }
    public void ShowUI()
    {
        Vector3 forward = playerHead.forward;
        Vector3 targetPos = playerHead.position + forward * distance;

        transform.position = targetPos + offset;

        transform.LookAt(playerHead);
        if (isFirstFrame)
        {
            transform.Rotate(0, 180f, 0);
            isFirstFrame = false;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
