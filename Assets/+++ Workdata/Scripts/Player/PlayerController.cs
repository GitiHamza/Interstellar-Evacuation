using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private static readonly int Upwards = Animator.StringToHash("Upwards");

    #region Inspector Variables
    
    [SerializeField] private int _walkingSpeed = 5;
    [SerializeField] private int _runningSpeed = 8;
    [Header("Rescue Manager")]
    public float holdTimeRequired;
    
    public Interactable interactable;
    
    public PlayerMovementState playerMovementState;
    
    #endregion Inspector Variables

    #region Private Variables

    public InputSystem_Actions _inputActions;
    private InputAction _moveAction;
    private InputAction _attackAction;
    private InputAction _guardAction;
    private InputAction _rescueAction;
    
    public Vector2 _moveInput;
    private Rigidbody2D _rb;
    [SerializeField] private Animator _anim;
    
    private float _currentSpeed;
    
    [Header("ShieldManager")]
    public float shieldDuration;
    private bool _shieldActive = false;
    private float _shieldTimer;
    public GameObject shieldVisual;
    private bool _hasShieldItem;
    
    [Header("Rescue Manager")]
    private float rescueHoldTimer = 0f;
    private bool isHoldingRescue = false;
    #endregion Private Variables
    
    #region Enums
    public enum PlayerMovementState
    {
        Idle,
        Move
    }
    #endregion Enums

    #region Event Functions
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        interactable = GetComponent<Interactable>();
        
        HardwareInputs();
    }

    private void HardwareInputs()
    {
        _inputActions = new InputSystem_Actions();
        _currentSpeed = _walkingSpeed;
        _moveAction = _inputActions.Player.Move;
        _attackAction = _inputActions.Player.Attack;
        _guardAction = _inputActions.Player.Guard;
        _rescueAction = _inputActions.Player.Rescue;
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        _moveAction.performed += Move;
        _moveAction.canceled += Move;
        //_attackAction.performed += Attack;
        _guardAction.performed += Guard;
        _rescueAction.performed += Rescue;
        _rescueAction.canceled += Rescue;
    }

    private void OnDisable()
    {
        _inputActions.Disable();
        _moveAction.performed -= Move;
        _moveAction.canceled -= Move;
        //_attackAction.performed -= Attack;
        _guardAction.performed -= Guard;
        _rescueAction.performed -= Rescue;
        _rescueAction.canceled -= Rescue;
    }
    
    private void UpdateAnimator()
    {
        float movementValue = _moveInput.x > 0 ? _moveInput.x : 0f;
        _anim.SetFloat("MovementValue", movementValue);

        _anim.SetBool("Upwards", _moveInput.y > 0);
    }

    private void Update()
    {
        if (_shieldActive && shieldDuration > 0f)
        {
            _shieldTimer -= Time.deltaTime;
            if (_shieldTimer <= 0f)
            {
                DeactivateShield();
            }
        }

        if (!isHoldingRescue) return;
        
        rescueHoldTimer += Time.deltaTime;
        
        if (rescueHoldTimer >= holdTimeRequired)
        {
            isHoldingRescue = false;
            Debug.Log("Rescue successful");
        }
    }
    private void FixedUpdate()
    {
        _rb.linearVelocity = _moveInput * _currentSpeed;
        UpdateAnimator();
    }
    #endregion Event Functions
    #region Input Methods

    private void Move(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
        if (_moveInput.x == 0 && _moveInput.y == 0)
        {
            playerMovementState = PlayerMovementState.Idle;
        }
        else if (_moveInput.x != 0 || _moveInput.y != 0)
        {
            playerMovementState = PlayerMovementState.Move;
        }

        if (_moveInput.y > 0)
        {
            _anim.SetBool("Upwards", true);
        }
        else
        {
            _anim.SetBool("Upwards", false);
        }
    }

    private void Guard(InputAction.CallbackContext ctx)
    {
        if (!_hasShieldItem) return;
        if (ctx.performed && !_shieldActive)
        {
            ActivateShield();
        }
        
    }

    private void Rescue(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isHoldingRescue = true;
        }

        if (context.canceled)
        {
            isHoldingRescue = false;
            rescueHoldTimer = 0f;
        }
    }

    public void ActivateShield()
    {
        _shieldActive = true;
        _shieldTimer = shieldDuration;
        if(shieldVisual != null) shieldVisual.SetActive(true);
        Debug.Log("Shield activated");
    }

    public void DeactivateShield()
    {
        _shieldActive = false;
        if (shieldVisual != null) shieldVisual.SetActive(false);
        Debug.Log("Shield deactivated");
    }
    #endregion Input Methods

    #region DamageRegulation

    public float ModifyDamage(float damage)
    {
        if (!_shieldActive) return damage;
        
        Debug.Log("Damage blocked");
        if (shieldDuration == 0f) DeactivateShield();
        return 0f;
    }

    #endregion

    #region ItemManager

    public void PickupShieldItem()
    {
        _hasShieldItem = true;
        if(shieldVisual != null) shieldVisual.SetActive(false);
        Debug.Log("Shield picked up");
    }

    #endregion
}

