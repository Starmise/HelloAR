using UnityEngine;

public class TogglePanel : MonoBehaviour
{
    public GameObject content;
    public RectTransform panel;

    [Header("Panel Height")]
    public float heightVisible = 265f;
    public float heightHidden = 60f;
    public float moveOffsetY = 102f;

    private Vector2 originalPosition;
    private bool isVisible = true;

    void Start()
    {
        if (panel != null)
            originalPosition = panel.anchoredPosition;
    }

    public void Toggle()
    {
        isVisible = !isVisible;
        content.SetActive(isVisible);

        if (panel != null)
        {
            Vector2 size = panel.sizeDelta;
            size.y = isVisible ? heightVisible : heightHidden;
            panel.sizeDelta = size;

            Vector2 pos = originalPosition;
            if (!isVisible)
            {
                pos.y += moveOffsetY;
            }
            panel.anchoredPosition = pos;
        }
    }
}