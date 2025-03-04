using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float runSpeed = 8f;
    
    private CharacterController _controller;
    private Vector3 _direction;
    private Camera playerCamera;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        
        var isRunning =  Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        float curSpeedX = Input.GetAxis("Vertical");
        float curSpeedY = Input.GetAxis("Horizontal");
        
        _direction = (forward * curSpeedX) + (right * curSpeedY);
        
        if (_direction.magnitude > 1)
        {
            _direction.Normalize();    
        }
        
        _controller.Move(_direction * ((isRunning ? runSpeed : speed) * Time.deltaTime));
    }
}
