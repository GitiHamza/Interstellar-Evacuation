using UnityEngine;
using UnityEngine.InputSystem;
public class ChildCage : MonoBehaviour
{
    private InputSystem_Actions _inputActions;
    private InputAction _rescueAction;

    public float holdTimeRequired;
    private float rescueHoldTimer = 0f;
    private bool isHoldingRescue = false;

    private void Awake()
    {
        HardwareInputs();
    }
    private void HardwareInputs()
    {
        _inputActions = new InputSystem_Actions();
        _rescueAction = _inputActions.Player.Rescue;
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        _rescueAction.performed += Rescue;
        _rescueAction.canceled += Rescue;
    }

    private void OnDisable()
    {
        _inputActions.Disable();
        _rescueAction.performed -= Rescue;
        _rescueAction.canceled -= Rescue;
    }

    private void Rescue(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rescueHoldTimer += Time.deltaTime;
            isHoldingRescue = true;
            if (rescueHoldTimer >= holdTimeRequired)
            {
                Debug.Log("Rescue successful");
            }
        }
        else
        { 
            rescueHoldTimer = 0f;
            isHoldingRescue = false;
        }
    }
}

