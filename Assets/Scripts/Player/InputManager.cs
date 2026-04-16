using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("Player 1")]
    [SerializeField] Movement movement1;
    [SerializeField] MouseLook mouseLook1;
    [SerializeField] WeaponController weaponController1;

    [Header("Player 2")]
    [SerializeField] Movement movement2;
    [SerializeField] MouseLook mouseLook2;
    [SerializeField] WeaponController weaponController2;

    private Gamepad pad1;
    private Gamepad pad2;

    private void Update()
    {
        // Auto-assign connected gamepads
        pad1 = Gamepad.all.Count > 0 ? Gamepad.all[0] : null;
        pad2 = Gamepad.all.Count > 1 ? Gamepad.all[1] : null;

        if (pad1 != null) HandlePlayer(pad1, movement1, mouseLook1, weaponController1);
        if (pad2 != null) HandlePlayer(pad2, movement2, mouseLook2, weaponController2);
    }

    private void HandlePlayer(Gamepad pad, Movement movement, MouseLook mouseLook, WeaponController weaponController)
    {
        Vector2 move = pad.leftStick.ReadValue();
        Vector2 look = pad.rightStick.ReadValue();

        movement.ReceiveInput(move);
        mouseLook.ReceiveInput(look);

        if (pad.buttonSouth.wasPressedThisFrame)
            movement.OnJumpPressed();

        if (pad.rightTrigger.isPressed)
            weaponController.OnMouseShoot();

        if (pad.buttonWest.wasPressedThisFrame)
            weaponController.OnReload();

        if (pad.rightShoulder.wasPressedThisFrame)
            weaponController.OnSwitchGuns();
    }
}