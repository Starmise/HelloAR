using UnityEngine;
using System.Collections;
using Vuforia;

public class PopUpURL : MonoBehaviour
{
    public GameObject popupPanel;
    public float delay = 30f;
    public string url;

    [Header("Vuforia Target")]
    public ObserverBehaviour modelTarget;

    private bool popupActive = false;
    private bool timerStarted = false;

    private void Start()
    {
        if (popupPanel != null)
        {
            popupPanel.SetActive(false);
        }

        if (modelTarget != null)
        { modelTarget.OnTargetStatusChanged += OnTargetStatusChanged; }

    }

    private void OnTargetStatusChanged(ObserverBehaviour behaviour, TargetStatus status)
    {
        if (!timerStarted && status.Status == Status.TRACKED || status.Status == Status.EXTENDED_TRACKED)
        {
            timerStarted = true;
            StartCoroutine(ShowPopupAfterDelay());
        }
    }

    private IEnumerator ShowPopupAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        popupActive = true;

        if (popupPanel != null)
        {
            popupPanel.SetActive(true);
        }

        StartCoroutine(BlinkPanel());
    }

    public void AbrirEnlace()
    {
        if (!string.IsNullOrEmpty(url))
            Application.OpenURL(url);
    }

    // Annoying Blink (what have i become??)
    private System.Collections.IEnumerator BlinkPanel()
    {
        CanvasGroup cg = popupPanel.GetComponent<CanvasGroup>();
        if (cg == null)
        {
            cg = popupPanel.AddComponent<CanvasGroup>();
        }

        while (popupActive)
        {
            cg.alpha = 0.3f;
            yield return new WaitForSeconds(0.3f);
            cg.alpha = 1f;
            yield return new WaitForSeconds(0.3f);
        }
    }

    private void OnDestroy()
    {
        if (modelTarget != null)
        {
            modelTarget.OnTargetStatusChanged -= OnTargetStatusChanged;
        }
    }
}
