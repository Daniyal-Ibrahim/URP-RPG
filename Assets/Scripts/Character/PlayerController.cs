using System.Collections;
using Cinemachine;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// 
/// </summary>
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    private Vector2 _inputVector = new Vector2(0, 0);
    private Vector3 _targetVector;
    private Quaternion _rotation;
    [SerializeField] private float rotationSpeed;
    private Rigidbody _rb;
    // Movement
    public float speed;
    public float jump;


    [SerializeField] private bool isMoving;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool isGrounded;
    [SerializeField] private bool isRunning;

    
    [Header("Animation")]
    private Animator _animator;
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int Running = Animator.StringToHash("isRunning");
    private static readonly int Dodging = Animator.StringToHash("Dodge");

    [SerializeField] private float maxVelocity;
    // Animations
    public Animator animator;
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");


    // Attack Animations 
    [Header("Attack Animation")]
    private static readonly int Weapon = Animator.StringToHash("Weapon ID");
    private static readonly int AttackReset = Animator.StringToHash("Attack Reset"); 
    private static readonly int[] Attack = new[]
    {
        Animator.StringToHash("Attack 1"),
        Animator.StringToHash("Attack 2"),
        Animator.StringToHash("Attack 3")
    };
    private float _delayTimer;
    private bool _pressed;
    private int _attack;
    private bool _attackResetDelayRunning;
    [SerializeField] private float resetDelay;
    
    [Header("Interaction")]
    public bool inInteracting;


    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    #region Input Callbacks

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isMoving = true;
        }

        if (context.canceled)
        {
            isMoving = false;
        }

        _inputVector = context.ReadValue<Vector2>();
        _targetVector = new Vector3(_inputVector.x, 0, _inputVector.y);

        if (Camera.main != null)
        {
            _targetVector = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * _targetVector;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isJumping = true;
            JumpLogic();
        }

        if (context.canceled)
        {
            isJumping = false;
            animator.SetBool(IsJumping, isJumping);
            _rb.AddForce(jump * Vector3.down,ForceMode.Impulse);
        }
    }
    
    public void OnInteract(InputAction.CallbackContext context)
    {

        if (context.performed)
        {
            inInteracting = true;
        }

        if (context.canceled)
        {
            inInteracting = false;
        }

    }

    [SerializeField] private _Test_Panels panels;
    [SerializeField] private CinemachineVirtualCamera virtualCamera;

    public void OnInventory(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            panels.ShowInventory();
        }
        
    }
    
    public void OnCharacter(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            panels.ShowCharacter();
        }
    }
    
    public void OnEquipment(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            panels.ShowEquipment();
        }

    }
    
    

    public void OnAttack(InputAction.CallbackContext context)
    {

        if (_attack >= 3)
        {
            _attack = 0;
            animator.SetTrigger(AttackReset);
        }

        resetDelay = 1;// Mathf.Abs(_attack - 4);

        if (context.performed && !_pressed)
        {
            _delayTimer = 0.5f;
            _pressed = true;
            animator.SetTrigger(Attack[_attack]);
            Debug.Log(_attack);
            _attack++;
        }


        if (!context.canceled) return;
        if (_attackResetDelayRunning)
        {
            StopCoroutine(nameof(AttackRestDelay));
            StartCoroutine(nameof(AttackRestDelay));
        }
        else
        {
            StartCoroutine(nameof(AttackRestDelay));
        }
    }


    IEnumerator AttackRestDelay()
    {
        _attackResetDelayRunning = true;
        if (_attack == 3)
        {
            _attack = 0;
            resetDelay = 0;
            animator.SetTrigger(AttackReset);
        }
        yield return new WaitForSecondsRealtime(resetDelay);
        _attack = 0;
        resetDelay = 0;
        animator.SetTrigger(AttackReset);
        _attackResetDelayRunning = false;
    }

    private void Reset()
    {
        if(_pressed && _delayTimer > 0)
        {
            _delayTimer -= Time.deltaTime;
        }
        if(_delayTimer < 0)
        {
            _delayTimer = 0;
            _pressed = false;
        }
    }
    

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.performed)
            isRunning = true;

        if (context.canceled)
            isRunning = false;
    }

    #endregion
    
    
    private void Update()
    {
        Reset();
        PlayerAnimation();
    }

    private void FixedUpdate()
    {
        PlayerPhysicsMovement();
        
    }

    public void Zoom(InputAction.CallbackContext context)
    {
        var z = context.ReadValue<float>();

        if (z > 0 && virtualCamera.m_Lens.OrthographicSize > 2)
        {
            virtualCamera.m_Lens.OrthographicSize -= 0.1f;
        }
        else if (z < 0 && virtualCamera.m_Lens.OrthographicSize < 10)
        {
            virtualCamera.m_Lens.OrthographicSize += 0.1f;
        }
    }
    
    /// <summary>
    /// Rotates the player during movement, applying rigid body velocity to move character forward
    /// </summary>
    private void PlayerPhysicsMovement()
    {
        if (isGrounded)
        {
            if (_inputVector.x > 0 || _inputVector.x < 0 || _inputVector.y > 0 || _inputVector.y < 0)
            {

                _rotation = Quaternion.LookRotation(_targetVector);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, _rotation, 180);
                
                if(_rb.velocity.magnitude >= maxVelocity)
                    return;
                _rb.AddForce( transform.forward * speed ,ForceMode.Impulse);
            }
            else
            {
                _rb.velocity = new Vector3(0,0,0);
            }
        }
    }

    private void JumpLogic()
    {
        //_rb.AddForce(jump * Vector3.up,ForceMode.Impulse);
        _rb.AddForce(Vector3.up * Mathf.Sqrt(jump * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        animator.SetBool(IsJumping, true);
    }
    

    /// <summary>
    /// Sets player animation
    /// </summary>
    private void PlayerAnimation()
    {
        if (isGrounded)
        {
            animator.SetFloat(Vertical, _inputVector.y);
            animator.SetFloat(Horizontal, _inputVector.x);
            if (isMoving)
            {
                animator.SetBool(IsRunning, isRunning);
            }
        }
        else
        {
            animator.SetFloat(Vertical, 0);
            animator.SetFloat(Horizontal, 0);
        }
    }

    private void OnApplicationQuit()
    {
        StopAllCoroutines();
    }
}
