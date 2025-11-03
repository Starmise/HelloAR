using UnityEngine;
using TMPro;

public class XboxControllerGuide_NewInput : MonoBehaviour
{
    [Header("UI References")]
    public TMP_Text buttonNameTxt;
    public TMP_Text buttonDescTxt;

    private XboxControl controls;

    private void Awake()
    {
        controls = new XboxControl();

        // Face Buttons
        controls.Gamepad.A.performed += ctx => ShowButtonInfo("A", "Usado comúnmente para saltar o confirmar acciones.");
        controls.Gamepad.B.performed += ctx => ShowButtonInfo("B", "Botón B: usado para cancelar o retroceder.");
        controls.Gamepad.X.performed += ctx => ShowButtonInfo("X", "Usado para recargar, interactuar o acciones secundarias.");
        controls.Gamepad.Y.performed += ctx => ShowButtonInfo("Y", "Usado para cambiar de arma o abrir inventario.");

        // Bumpers
        controls.Gamepad.LB.performed += ctx => ShowButtonInfo("LB", "Left Bumper: usado para habilidades secundarias o apuntar.");
        controls.Gamepad.RB.performed += ctx => ShowButtonInfo("RB", "Right Bumper: usado para cambiar objetos o realizar acciones rápidas.");

        // Triggers
        controls.Gamepad.LT.performed += ctx => ShowButtonInfo("LT", "Left Trigger: frecuentemente usado para apuntar o frenar.");
        controls.Gamepad.RT.performed += ctx => ShowButtonInfo("RT", "Right Trigger: generalmente usado para disparar o acelerar.");

        // Mains
        controls.Gamepad.Start.performed += ctx => ShowButtonInfo("Start", "Abre el menú de pausa o configuración.");
        controls.Gamepad.Back.performed += ctx => ShowButtonInfo("Back", "Muestra el mapa o información del sistema.");

        // Sticks
        controls.Gamepad.LS.performed += ctx => ShowButtonInfo("Left Stick Button", "Presiona el stick izquierdo, normalmente para correr o agacharse.");
        controls.Gamepad.RS.performed += ctx => ShowButtonInfo("Right Stick Button", "Presiona el stick derecho, usado para centrar cámara o apuntar.");

        // D-Pad:
        controls.Gamepad.Dpad_down.performed += ctx => ShowButtonInfo("D-Pad Down", "Usado para navegar menús o cambiar ítems.");
        controls.Gamepad.Dpad_right.performed += ctx => ShowButtonInfo("D-Pad Right", "Usado para navegar menús o cambiar ítems.");
        controls.Gamepad.DPad_left.performed += ctx => ShowButtonInfo("D-Pad Left", "Usado para navegar menús o cambiar ítems.");
        controls.Gamepad.Dpad_up.performed += ctx => ShowButtonInfo("D-Pad Up", "Usado para navegar menús o cambiar ítems.");
    }

    private void Start()
    {
        // Default (Only at the start)
        buttonNameTxt.text = "Guía del mando Xbox";
        buttonDescTxt.text = "Presiona cualquier botón del mando para ver su función.";
    }

    private void OnEnable()
    {
        controls.Gamepad.Enable();
    }

    private void OnDisable()
    {
        controls.Gamepad.Disable();
    }

    void ShowButtonInfo(string name, string description)
    {
        buttonNameTxt.text = name;
        buttonDescTxt.text = description;
        Debug.Log($"{name}: {description}");
    }
}
