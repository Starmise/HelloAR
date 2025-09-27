using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private TMP_Text scoreTxt;
    [SerializeField] private int maxHealth = 100;

    private void OnEnable()
    {
        RunnerEvents.OnLifeChange += UpdateHealthUI;
        RunnerEvents.OnScoreChange += UpdateScoreUI;
    }

    private void UpdateHealthUI(int health)
    {
        healthBar.value = (float)health / maxHealth;
    }

    private void UpdateScoreUI(int score)
    {
        scoreTxt.text = $"Score: {score}";
    }

    private void OnDisable()
    {
        RunnerEvents.OnLifeChange -= UpdateHealthUI;
        RunnerEvents.OnScoreChange -= UpdateScoreUI;
    }
}
