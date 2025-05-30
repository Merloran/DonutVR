using UnityEngine;

public class Arrow : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        Vector3 start = transform.position;
        Vector3 end = start + transform.forward * 2f;

        Gizmos.DrawLine(start, end);
        DrawArrowHead(end, transform.forward);
    }

    private void DrawArrowHead(Vector3 position, Vector3 direction)
    {
        float headSize = 0.2f;
        Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 150, 0) * Vector3.forward;
        Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, -150, 0) * Vector3.forward;

        Gizmos.DrawLine(position, position + right * headSize);
        Gizmos.DrawLine(position, position + left * headSize);
    }
}
