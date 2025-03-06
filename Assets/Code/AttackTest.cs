using UnityEngine;

public class AttackTest : MonoBehaviour
{
    private float timeSinceEnabled = 0f;
    
    [SerializeField]
    float lastingTime = 0.5f;
        
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceEnabled += Time.deltaTime;
        if (timeSinceEnabled > lastingTime)
        {
            gameObject.SetActive(false);
            timeSinceEnabled = 0;
        }
    }
}
