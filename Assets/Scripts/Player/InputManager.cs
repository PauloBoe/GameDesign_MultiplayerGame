//Auteur: Paulo Boe
//User Story 1: Controle over karakter

using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] Movement movement;
    [SerializeField] MouseLook mouseLook;
    [SerializeField] WeaponController weaponController;

    //Link to Unity Input System
    PlayerControls controls;
    PlayerControls.GroundmovmentActions groundMovement;
    PlayerControls.PlayerActionsActions playerActions;

    private Vector2 horizontalInput;
    private Vector2 mouseInput;

    private void Awake()
    {
        controls = new PlayerControls();
        groundMovement = controls.Groundmovment;
        playerActions = controls.PlayerActions;

        //Here the methods are mapped to the controlls
        groundMovement.HorizontalMovement.performed += ctx => horizontalInput = ctx.ReadValue<Vector2>();
        groundMovement.MouseX.performed += ctx => mouseInput.x = ctx.ReadValue<float>();
        groundMovement.MouseY.performed += ctx => mouseInput.y = ctx.ReadValue<float>();
        groundMovement.Jump.performed += _ => movement.OnJumpPressed();
        playerActions.Reload.performed += _ => weaponController.OnReload();
        playerActions.WeaponSwitch.performed += _ => weaponController.OnSwitchGuns();
    }

    private void Update()
    {
        //Continuous checking for mouse movement input
        movement.ReceiveInput(horizontalInput);
        mouseLook.ReceiveInput(mouseInput);

        if (playerActions.Shoot.IsPressed())
        {
            weaponController.OnMouseShoot();
        }
    }

    private void OnEnable()
    {
        controls.Enable();
    }
    private void OnDisable()
    {
        controls.Disable();
    }
}