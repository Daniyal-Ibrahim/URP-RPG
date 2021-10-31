using UnityEngine;

public class GridMovement : MonoBehaviour
{
    public static GridMovement instance;
    
    [SerializeField] private float moveSpeed = 0.25f;
    [SerializeField] private float rayLength = 1.4f;
    [SerializeField] private float rayOffsetX = 0.5f;
    [SerializeField] private float rayOffsetY = 0.5f;
    [SerializeField] private float rayOffsetZ = 0.5f;

    private Vector3 _targetPosition;
    private Vector3 _startPosition;
    private bool _moving;

    private Vector3 _xOffset;
    private Vector3 _yOffset;
    private Vector3 _zOffset;
    private Vector3 _zAxisOriginA;
    private Vector3 _zAxisOriginB;
    private Vector3 _xAxisOriginA;
    private Vector3 _xAxisOriginB;

    [SerializeField] private Transform cameraRotator = null;

    [SerializeField] private LayerMask walkableMask = 0;

    [SerializeField] private LayerMask collidableMask = 0;

    [SerializeField] private float maxFallCastDistance = 100f;
    [SerializeField] private float fallSpeed = 30f;
    private bool _falling;
    private float _targetFallHeight;

    void Update() {

        // Set the ray positions every frame

        
       
        /*if (_falling) {
            if (transform.position.y <= _targetFallHeight) 
            {
                float x = Mathf.Round(transform.position.x);
                float y = Mathf.Round(_targetFallHeight);
                float z = Mathf.Round(transform.position.z);

                transform.position = new Vector3(x, y, z);

                _falling = false;

                return;
            }

            transform.position += Vector3.down * fallSpeed * Time.deltaTime;
            return;
        } 
        else */if (_moving) 
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

            transform.position += (Time.deltaTime) * moveSpeed * (_targetPosition - _startPosition);
            return;
        } 
        /*else 
        {
            RaycastHit[] hits = Physics.RaycastAll(
                    transform.position + Vector3.up * 0.5f,
                    Vector3.down,
                    maxFallCastDistance,
                    walkableMask
            );

            if (hits.Length > 0) 
            {
                int topCollider = 0;
                for (int i = 0; i < hits.Length; i++) 
                {
                    if (hits[topCollider].collider.bounds.max.y < hits[i].collider.bounds.max.y)
                        topCollider = i;
                }
                if (hits[topCollider].distance > 1f) 
                {
                    _targetFallHeight = transform.position.y - hits[topCollider].distance + 0.5f;
                    _falling = true;
                }
            } 
            else 
            {
                _targetFallHeight = -Mathf.Infinity;
                _falling = true;
            }
        }*/

        // Handle player input
        // Also handle moving up 1 level

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)) 
        {
            if (CanMove(Vector3.forward))
            {
                _targetPosition = transform.position + cameraRotator.transform.forward;
                _startPosition = transform.position;
                _moving = true;
            } 
            /*else if (CanMoveUp(Vector3.forward)) 
            {
                _targetPosition = transform.position + cameraRotator.transform.forward + Vector3.up;
                _startPosition = transform.position;
                _moving = true;
            }*/
        }
        
    }

    // Check if the player can move

    bool CanMove(Vector3 direction) {
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

    // Check if the player can step-up

    /*bool CanMoveUp(Vector3 direction) {
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.up, 1f, collidableMask))
            return false;
        if (Physics.Raycast(transform.position + Vector3.up * 1.5f, direction, 1f, collidableMask))
            return false;
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, direction, 1f, walkableMask))
            return true;
        return false;
    }*/

    /*void OnCollisionEnter(Collision other) {
        if (_falling && (1 << other.gameObject.layer & walkableMask) == 0) {
            // Find a nearby vacant square to push us on to
            Vector3 direction = Vector3.zero;
            Vector3[] directions = { Vector3.forward, Vector3.right, Vector3.back, Vector3.left };
            for (int i = 0; i < 4; i++) {
                if (Physics.OverlapSphere(transform.position + directions[i], 0.1f).Length == 0) {
                    direction = directions[i];
                    break;
                }
            }
            transform.position += direction;
        }
    }*/
}
