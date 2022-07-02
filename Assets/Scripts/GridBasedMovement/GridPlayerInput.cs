using UnityEngine;
using UnityEngine.InputSystem;

public class GridPlayerInput : MonoBehaviour
{
    public static GridPlayerInput instance;
    
    [SerializeField] private float moveSpeed;
    private Vector2 _inputVector = new Vector2(0, 0);
    private Vector3 _targetVector;
    private Quaternion _rotation;
   
    
    // Movement
    [SerializeField] private Transform rotationReference = null;

    private Vector3 _targetPosition;
    private Vector3 _startPosition;
    private bool _moving;
    
    // Rays
    [SerializeField] private float rayLength = 1.4f;
    [SerializeField] private float rayOffsetX = 0.5f;
    [SerializeField] private float rayOffsetY = 0.5f;
    [SerializeField] private float rayOffsetZ = 0.5f;
    private Vector3 _xOffset;
    private Vector3 _yOffset;
    private Vector3 _zOffset;
    private Vector3 _zAxisOriginA;
    private Vector3 _zAxisOriginB;
    private Vector3 _xAxisOriginA;
    private Vector3 _xAxisOriginB;
    
    
    // Animations
    public Animator animator;
    private static readonly int Vertical = Animator.StringToHash("Vertical");
    private static readonly int Horizontal = Animator.StringToHash("Horizontal");
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private static readonly int IsJumping = Animator.StringToHash("isJumping");

    private int _move;
    
    public void OnMovement(InputAction.CallbackContext context)
    {

        _inputVector = context.ReadValue<Vector2>();
        _targetVector = new Vector3(_inputVector.x, 0, _inputVector.y);

        if (Camera.main != null)
        {
            _targetVector = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * _targetVector;
        }

        if(context.canceled)
            CalculateMovement();
    }

    private void Update()
    {
        PlayerAnimation();
        PlayerRotation();
        DrayRays();

        if (_moving)
        {
            if (Vector3.Distance(_startPosition, transform.position) > 1f)
            {
                float x = Mathf.Round(_targetPosition.x);
                float y = Mathf.Round(_targetPosition.y);
                float z = Mathf.Round(_targetPosition.z);

                transform.position = new Vector3(x, y, z);

                _moving = false;

                return;
            }

            _move = 1;
            transform.position += (Time.deltaTime) * moveSpeed * (_targetPosition - _startPosition);
        }
        else
        {
            _move = 0;
        }
    }
    
    private void PlayerRotation()
    {
        if (_inputVector.x > 0 || _inputVector.x < 0 || _inputVector.y > 0 || _inputVector.y < 0)
        {
            _rotation = Quaternion.LookRotation(_targetVector);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, _rotation, 180);
        }
        
    }
    
    private void CalculateMovement()
    {
        if (!CanMove(Vector3.forward)) return;
        var position = transform.position;
        _targetPosition = position + rotationReference.transform.forward;
        _startPosition =  position;
        _moving = true;
    }


    private bool CanMove(Vector3 direction) {
        if (direction.z != 0) {
            if (Physics.Raycast(_zAxisOriginA, direction, rayLength)) return false;
            if (Physics.Raycast(_zAxisOriginB, direction, rayLength)) return false;
        }
        else if (direction.x != 0) {
            if (Physics.Raycast(_xAxisOriginA, direction, rayLength)) return false;
            if (Physics.Raycast(_xAxisOriginB, direction, rayLength)) return false;
        }
        return true;
    }

    private void DrayRays()
    {
        _yOffset = transform.position + Vector3.up * rayOffsetY;
        _zOffset = Vector3.forward * rayOffsetZ;
        _xOffset = Vector3.right * rayOffsetX;

        _zAxisOriginA = _yOffset + _xOffset;
        _zAxisOriginB = _yOffset - _xOffset;

        _xAxisOriginA = _yOffset + _zOffset;
        _xAxisOriginB = _yOffset - _zOffset;

        // Draw Debug Rays

        #region Debug Rays

        Debug.DrawLine(
            _zAxisOriginA,
            _zAxisOriginA + Vector3.forward * rayLength,
            Color.red,
            Time.deltaTime);
        Debug.DrawLine(
            _zAxisOriginB,
            _zAxisOriginB + Vector3.forward * rayLength,
            Color.red,
            Time.deltaTime);

        Debug.DrawLine(
            _zAxisOriginA,
            _zAxisOriginA + Vector3.back * rayLength,
            Color.red,
            Time.deltaTime);
        Debug.DrawLine(
            _zAxisOriginB,
            _zAxisOriginB + Vector3.back * rayLength,
            Color.red,
            Time.deltaTime);

        Debug.DrawLine(
            _xAxisOriginA,
            _xAxisOriginA + Vector3.left * rayLength,
            Color.red,
            Time.deltaTime);
        Debug.DrawLine(
            _xAxisOriginB,
            _xAxisOriginB + Vector3.left * rayLength,
            Color.red,
            Time.deltaTime);

        Debug.DrawLine(
            _xAxisOriginA,
            _xAxisOriginA + Vector3.right * rayLength,
            Color.red,
            Time.deltaTime);
        Debug.DrawLine(
            _xAxisOriginB,
            _xAxisOriginB + Vector3.right * rayLength,
            Color.red,
            Time.deltaTime);


        #endregion
    }

    void PlayerAnimation()
    {
        animator.SetFloat(Vertical, _move);
        animator.SetFloat(Horizontal, _move);
    }
}
