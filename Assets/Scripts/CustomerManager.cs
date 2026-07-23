using UnityEngine;
 
public class CustomerManager : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector3 target;
    
    void Awake()
    {
        target = transform.position; 
    }
 
    public void SetTarget(Vector3 newTarget)
    {
        target = newTarget;
    }
 
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }
}