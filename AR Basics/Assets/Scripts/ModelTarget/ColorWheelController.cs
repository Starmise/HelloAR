using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ColorWheelController : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public Material targetMaterial;
    public string colorProperty = "_Outline_Color";

    private Texture2D colorWheelTexture;

    void Start()
    {
        Image image = GetComponent<Image>();
        colorWheelTexture = image.sprite.texture;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        PickColor(eventData);
    }

    public void OnDrag(PointerEventData eventData)
    {
        PickColor(eventData);
    }

    void PickColor(PointerEventData eventData)
    {
        RectTransform rect = GetComponent<RectTransform>();
        Vector2 localPoint;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(rect, eventData.position, eventData.pressEventCamera, out localPoint))
        {
            Vector2 normalized = Rect.PointToNormalized(rect.rect, localPoint);
            int x = Mathf.Clamp((int)(normalized.x * colorWheelTexture.width), 0, colorWheelTexture.width - 1);
            int y = Mathf.Clamp((int)(normalized.y * colorWheelTexture.height), 0, colorWheelTexture.height - 1);

            Color pickedColor = colorWheelTexture.GetPixel(x, y);
            targetMaterial.SetColor(colorProperty, pickedColor);
        }
    }
}

