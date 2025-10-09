using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerControllerAR : MonoBehaviour
{
    [Header("Movement")]
    public float lateralSpeed = 0.08f;

    private int health = 100;
    private int score = 0;
    private Rigidbody rb;
    private RunnerControls controls;
    private Vector2 moveInput;

    private void Awake()
    {
        controls = new RunnerControls();
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        RunnerEvents.OnLifeChange?.Invoke(health);
        RunnerEvents.OnScoreChange?.Invoke(score);

        StartCoroutine(AddScoreByTime());
    }

    private void OnEnable()
    {
        controls.Enable();
        controls.Runner.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Runner.Move.canceled += ctx => moveInput = Vector2.zero;
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void FixedUpdate()
    {
        float moveX = moveInput.x;

        Vector3 horizontalMovement = Vector3.right * moveX * lateralSpeed * Time.fixedDeltaTime / 10f;
        rb.MovePosition(rb.position + horizontalMovement);
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
