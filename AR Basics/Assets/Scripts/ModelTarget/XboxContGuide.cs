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
        controls.Gamepad.A.performed += ctx => ShowButtonInfo("A",
            "Commonly used for jumping or confirming actions. The A button has been the primary confirm/jump button since the original Xbox controller (2001). Its green color symbolizes 'go' or 'accept', consistent across all Xbox generations.");

        controls.Gamepad.B.performed += ctx => ShowButtonInfo("B",
            "Used to cancel or go back. The red color represents 'stop' or 'exit', making it intuitive. Present since the first Xbox controller, the B button is positioned to the right for quick access during gameplay.");

        controls.Gamepad.X.performed += ctx => ShowButtonInfo("X",
            "Often used for reloading, interacting, or performing secondary actions. The blue X button, placed on the left of the diamond layout, follows the color scheme from the original Xbox controller, meant to be easily distinguishable by both color and position.");

        controls.Gamepad.Y.performed += ctx => ShowButtonInfo("Y",
            "Used for switching weapons or opening inventory. The yellow color symbolizes 'alert' or 'attention', fitting for contextual or top-view actions. Introduced in 2001, its top position mirrors its role for upward or menu-related inputs.");


        // Bumpers
        controls.Gamepad.LB.performed += ctx => ShowButtonInfo("LB",
            "Left Bumper: commonly used for secondary abilities or aiming. Bumpers were first introduced with the Xbox 360 controller (2005), replacing the 'black and white' buttons of the original Xbox.");

        controls.Gamepad.RB.performed += ctx => ShowButtonInfo("RB",
            "Right Bumper: used to switch items or perform quick actions. Along with LB, RB improved ergonomics, allowing faster access without lifting the index fingers from the triggers.");


        // Triggers
        controls.Gamepad.LT.performed += ctx => ShowButtonInfo("LT",
            "Left Trigger: frequently used for aiming or braking. The analog triggers first appeared on the original Xbox controller, offering variable pressure sensitivity for precise control.");

        controls.Gamepad.RT.performed += ctx => ShowButtonInfo("RT",
            "Right Trigger: generally used for shooting or accelerating. Designed like a real trigger for intuitive use in shooters and racing games, evolving in resistance and feel through Xbox generations.");


        // Mains
        controls.Gamepad.Start.performed += ctx => ShowButtonInfo("Start",
            "Opens the pause or settings menu. On modern Xbox controllers, it's represented by the 'Menu' icon (three lines). The Start button has existed since the original Xbox, maintaining its role for pausing and accessing options.");

        controls.Gamepad.Back.performed += ctx => ShowButtonInfo("Back",
            "Displays the map or system information. Now known as the 'View' button, it replaced the traditional 'Back' name starting with the Xbox One controller (2013), used to toggle views or secondary menus.");


        // Sticks
        controls.Gamepad.LS.performed += ctx => ShowButtonInfo("Left Stick Button",
            "Pressing the left stick, usually to sprint or crouch. The clickable sticks (L3/R3) first appeared with the Xbox 360 controller, enhancing functionality without extra buttons.");

        controls.Gamepad.RS.performed += ctx => ShowButtonInfo("Right Stick Button",
            "Pressing the right stick, often used to center the camera or zoom. Its placement makes it ideal for quick camera control, a standard since the Xbox 360 era.");


        // D-Pad:
        controls.Gamepad.Dpad_down.performed += ctx => ShowButtonInfo("D-Pad Down",
            "Used for menu navigation or switching items. The D-Pad’s design evolved from the bulky original to the hybrid circular shape on the Xbox Series X|S controller, improving precision.");

        controls.Gamepad.Dpad_right.performed += ctx => ShowButtonInfo("D-Pad Right",
            "Used for menu navigation or switching items. Often mapped to shortcuts or quick commands in modern games.");

        controls.Gamepad.DPad_left.performed += ctx => ShowButtonInfo("D-Pad Left",
            "Used for menu navigation or switching items. Frequently assigned to gadgets or weapon wheels.");

        controls.Gamepad.Dpad_up.performed += ctx => ShowButtonInfo("D-Pad Up",
            "Used for menu navigation or switching items. Commonly toggles perspectives or activates special tools, depending on the game.");

    }

    private void Start()
    {
        // Default (Only at the start)
        buttonNameTxt.text = "Xbox Controller Guide";
        buttonDescTxt.text = "Press any key of the Xbox Controller to see it's function";
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
