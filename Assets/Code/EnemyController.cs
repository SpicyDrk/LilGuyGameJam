using UnityEngine;
using UnityEngine.Serialization;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float speed = 1f;
    [FormerlySerializedAs("hp")] [SerializeField] private float maxHp = 100f;
    public float currentHp; 
    [SerializeField] private float knockback = 5f;

    private PlayerController player;
    [SerializeField] private SpriteRenderer HpBar;
    
    //enter hitbox of weapon
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerAttack"))
        {
            currentHp -= 10;
            Vector3 direction = (transform.position - player.transform.position).normalized;
            direction.y = 0;
            transform.position += direction * knockback;
            if (currentHp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHp = maxHp;
        if (player == null)
        {
            player = FindObjectsByType<PlayerController>(FindObjectsSortMode.None)[0];
        }
    }
    
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position- new Vector3(0,1f,0), speed * Time.deltaTime);
    }
}
