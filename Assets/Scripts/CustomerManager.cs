using UnityEngine;
 
public class CustomerManager : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Vector3 target;
    public Animator animator;
    
    void Awake()
    {
        target = transform.position; 
        animator = GetComponent<Animator>();
    }
 
    public void SetTarget(Vector3 newTarget)
    {
        target = newTarget;
        bool isMoving = Vector2.Distance(transform.position, target) > 0.05f;
        if (animator != null)
        animator.SetBool("IsWalking", isMoving);
    }
 
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
    }
}