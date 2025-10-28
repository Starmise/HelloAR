using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicBall : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Vector3 firstPos;

    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        firstPos = transform.position;
    }

    public void ResetPosition()
    {
        transform.position = firstPos;
    }

    public void EnableRigidbody(bool enable)
    {
        _rigidbody.constraints = enable ? RigidbodyConstraints.None : RigidbodyConstraints.FreezeAll;
    }
}