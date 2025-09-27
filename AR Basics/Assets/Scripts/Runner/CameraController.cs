using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float speedSmooth = 4f;
    public Vector3 offset = new Vector3(0f, 11f, -6f);
    public float rotateSpeed = 100f;

    private float currentAngle = 0f;

    private void FixedUpdate()
    {
        if (target == null)
        {
            return;
        }

        Quaternion rotation = Quaternion.Euler(0f, currentAngle, 0f);
        Vector3 rotatedOffset = rotation * offset;

        Vector3 newPos = target.position + rotatedOffset;
        transform.position = Vector3.Lerp(transform.position, newPos, speedSmooth * Time.deltaTime);
        transform.LookAt(target);
    }
}
