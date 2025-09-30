using UnityEngine;

public class ChunkMover : MonoBehaviour
{
    public float speed = 0.01f;
    private RunnerGeneratorAr runnerGeneratorAr;

    private void Update()
    {
        transform.Translate(Vector3.back * speed * Time.deltaTime / 20f);

        if (transform.position.z < -100f)
        {
            Destroy(gameObject);
        }
    }
}
