using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float consSpeed = 10.0f;
    public float lateralSpeed = 8.0f;
    public float rotationSpeed = 12.0f;

    private int health = 100;
    private int score = 0;
    private Vector3 movement;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        RunnerEvents.OnLifeChange?.Invoke(health);
        RunnerEvents.OnScoreChange?.Invoke(score);

        StartCoroutine(AddScoreByTime());
    }

    private void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        Quaternion playerRotation = Quaternion.LookRotation(Vector3.forward, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, playerRotation, rotationSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        float moveX = Input.GetAxisRaw("Horizontal");

        Vector3 forwardMovement = Vector3.forward * consSpeed;
        Vector3 horizontalMovement = Vector3.right * moveX * lateralSpeed;

        movement = (forwardMovement + horizontalMovement) * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + movement);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            score += 10;
            RunnerEvents.OnScoreChange?.Invoke(score);
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Obstacle"))
        {
            ChangeHealth(-20);
        }
        else if (other.CompareTag("HealItem"))
        {
            ChangeHealth(15);

            score += 15;
            RunnerEvents.OnScoreChange?.Invoke(score);
            Destroy(other.gameObject);
        }
    }

    private IEnumerator AddScoreByTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            score += 2;
            RunnerEvents.OnScoreChange?.Invoke(score);
        }
    }

    private void ChangeHealth(int amount)
    {
        health += amount;
        health = Mathf.Clamp(health, 0, 100);
        RunnerEvents.OnLifeChange?.Invoke(health);

        if (health <= 0)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
