using UnityEngine;
using UnityEngine.Serialization;

public class AttackTest : MonoBehaviour
{
    private float timeSinceEnabled = 0f;
    
    [FormerlySerializedAs("lastingTime")] [SerializeField]
    float spriteLastingTime = 0.5f;
    
    [SerializeField] float colliderTime = 0.1f;
    
    private SpriteRenderer _spriteRenderer;
    private BoxCollider _boxCollider;
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {

        timeSinceEnabled += Time.deltaTime;
        if (timeSinceEnabled > spriteLastingTime)
        {
            gameObject.SetActive(false);
            _boxCollider.enabled = true;
            timeSinceEnabled = 0;
        }
        if(_boxCollider.enabled && timeSinceEnabled > colliderTime)
        {
            _boxCollider.enabled = false;
        }
    }
}
