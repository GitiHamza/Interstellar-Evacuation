using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private static readonly int Upwards = Animator.StringToHash("Upwards");

    #region Inspector Variables
    
    [SerializeField] private float _walkingSpeed = 5f;
    [SerializeField] private float _runningSpeed = 8f;
    public PlayerMovementState playerMovementState;
    
    #endregion Inspector Variables

    #region Private Variables

    public InputSystem_Actions _inputActions;
    private InputAction _moveAction;
    private InputAction _attackAction;
    private InputAction _guardAction;
    
    public Vector2 _moveInput;
    private Rigidbody2D _rb;
    [SerializeField] private Animator _anim;
    
    private float _currentSpeed;
    
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
        _inputActions = new InputSystem_Actions();
        
        _currentSpeed = _walkingSpeed;
        _moveAction = _inputActions.Player.Move;
        _attackAction = _inputActions.Player.Attack;
        _guardAction = _inputActions.Player.Guard;
    }

    private void OnEnable()
    {
        _inputActions.Enable();
        _moveAction.performed += Move;
        _moveAction.canceled += Move;
        //_attackAction.performed += Attack;
        //_guardAction.performed += Guard;
        //_guardAction.canceled += Guard;
    }

    private void OnDisable()
    {
        _inputActions.Disable();
        _moveAction.performed -= Move;
        _moveAction.canceled -= Move;
        //_attackAction.performed -= Attack;
        //_guardAction.performed -= Guard;
       // _guardAction.canceled -= Guard;
    }

    private void UpdateAnimator()
    {
        float movementValue = _moveInput.x > 0 ? _moveInput.x : 0f;
        _anim.SetFloat("MovementValue", movementValue);

        _anim.SetBool("Upwards", _moveInput.y > 0);
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
    #endregion Input Methods
}

