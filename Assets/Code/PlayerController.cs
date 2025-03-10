using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float runSpeed = 8f;
    
    [SerializeField] private float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    
    [SerializeField] private float attackCooldown = 0.5f;
    private float timeSinceAttack = 0f;
    private float resetTime;
    
    [SerializeField] private GameObject attackOne;
    [SerializeField] private GameObject attackTwo;
    [SerializeField] private GameObject attackThree;
    
    private AttackType currentAttackType = AttackType.SlashOne;
    
    private CharacterController _controller;
    private Vector3 _direction;
    private Camera playerCamera;
    
    private Vector3 _forward;
    private Vector3 _right;

    private float startHeight;
    
    private void Awake()
    {
        playerCamera = Camera.main;
        startHeight = transform.position.y;
    }
    void Start()
    {
        resetTime = attackCooldown * 3;
        transform.rotation = Quaternion.Euler(0, 45, 0);
        _controller = GetComponent<CharacterController>();
        _forward = Vector3.forward;
        _right = Vector3.right;
    }
    
    void Update()
    {
        Move();
        FaceMouse();
        ProcessClick();
        timeSinceAttack += Time.deltaTime;
        if(timeSinceAttack > resetTime)
        {
            currentAttackType = AttackType.SlashOne;
        }
        transform.position = new Vector3(transform.position.x, startHeight, transform.position.z);
    }
    
    private void ProcessClick()
    {
        if (Input.GetMouseButtonDown(0) && timeSinceAttack > attackCooldown)
        {
            timeSinceAttack = 0;
            switch (currentAttackType)
            {
                case AttackType.SlashOne:
                    attackOne.SetActive(true);
                    currentAttackType = AttackType.SlashTwo;
                    break;
                case AttackType.SlashTwo:
                    attackTwo.SetActive(true);
                    currentAttackType = AttackType.Stab;
                    break;
                case AttackType.Stab:
                    attackThree.SetActive(true);
                    currentAttackType = AttackType.SlashOne;
                    break;
            } 
        }
    }
    

    private void Move()
    {
        var isRunning =  Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift);
        float curSpeedX = Input.GetAxis("Vertical");
        float curSpeedY = Input.GetAxis("Horizontal");
        
        _direction = (_forward * curSpeedX) + (_right * curSpeedY);
        
        if (_direction.magnitude > 1)
        {
            _direction.Normalize();    
        }
        
        _direction = Quaternion.Euler(0, 45, 0) * _direction;
        
        _controller.Move(_direction * ((isRunning ? runSpeed : speed) * Time.deltaTime));
    }
    
    private void FaceMouse()
    {
        Ray cameraRay = playerCamera.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;
        
        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }
}

public enum AttackType
{
    SlashOne=1,
    SlashTwo=2,
    Stab=3
}
