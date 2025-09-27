using UnityEngine;

public class ChunkMover : MonoBehaviour
{
    public float speed = 1f;
    private RunnerGeneratorAr runnerGeneratorAr;

    private void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        if (transform.position.z < -100f)
        {
            Destroy(gameObject);
        }
    }
}
