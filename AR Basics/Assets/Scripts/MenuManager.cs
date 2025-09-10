using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject instrPanel;

    [SerializeField] private string startScene;

    private void Awake()
    {
        CloseIntructions();
    }

    public void StartApp()
    {
        SceneManager.LoadScene(startScene);
    }

    public void OpenIntructions()
    {
        menuPanel.SetActive(false);
        instrPanel.SetActive(true);
    }

    public void CloseIntructions()
    {
        menuPanel.SetActive(true);
        instrPanel.SetActive(false);
    }

    public void ExitApp()
    {
        Application.Quit();
    }
}
