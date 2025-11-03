using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class ColorPickerDebug : MonoBehaviour
{
    [Header("Referencias")]
    public Image colorWheelImage;
    public Material targetMaterial;

    private Texture2D colorWheelTexture;
    private Camera uiCamera;

    void Start()
    {
        if (colorWheelImage != null && colorWheelImage.sprite != null)
        {
            colorWheelTexture = colorWheelImage.sprite.texture;

            // Si el Canvas está en World Space, usar su cámara
            var canvas = colorWheelImage.canvas;
            if (canvas.renderMode == RenderMode.ScreenSpaceCamera)
                uiCamera = canvas.worldCamera;
            else
                uiCamera = null;
        }
        else
        {
            Debug.LogError("No se asignó la Image o su Sprite en el inspector.");
        }
    }

    void Update()
    {
#if UNITY_EDITOR
        // Permitir clic con mouse en el editor de la lap
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            HandleTouchOrClick(Mouse.current.position.ReadValue());
        }
#else
        // En Android
        if (Touchscreen.current != null && Touchscreen.current.primaryTouch.press.wasPressedThisFrame)
        {
            HandleTouchOrClick(Touchscreen.current.primaryTouch.position.ReadValue());
        }
#endif
    }

    private void HandleTouchOrClick(Vector2 screenPosition)
    {
        if (colorWheelImage == null || colorWheelTexture == null)
            return;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            colorWheelImage.rectTransform,
            screenPosition,
            uiCamera,
            out Vector2 localPos))
        {
            Rect rect = colorWheelImage.rectTransform.rect;

            float x = (localPos.x - rect.x) / rect.width;
            float y = (localPos.y - rect.y) / rect.height;

            if (x >= 0f && x <= 1f && y >= 0f && y <= 1f)
            {
                Sprite sprite = colorWheelImage.sprite;
                Rect spriteRect = sprite.textureRect;

                float texX = spriteRect.x + spriteRect.width * x;
                float texY = spriteRect.y + spriteRect.height * y;

                float u = texX / colorWheelTexture.width;
                float v = texY / colorWheelTexture.height;

                Color pickedColor = colorWheelTexture.GetPixelBilinear(u, v);

                Debug.Log($"Color detectado: {pickedColor}");

                if (targetMaterial != null)
                {
                    targetMaterial.SetColor("_OutlineColor", pickedColor);
                    Debug.Log("Color aplicado al material");
                }
                else
                {
                    Debug.LogWarning("No se asignó un material objetivo.");
                }
            }
        }
    }
}
