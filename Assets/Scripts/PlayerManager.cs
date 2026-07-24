using System.Reflection;
using UnityEngine;
using UnityEngine.InputSystem; 
public enum HeldItem { None, RawMeat, CookedMeat, Bread, Burger, Soda }
public class PlayerManager : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;
    private Vector2 moveInput;
    public bool hasFood;
    public HeldItem heldItem = HeldItem.None;
    public GameObject currentHeldItem;
    public Animator animator;
    public Transform itemHoldPoint;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator.GetComponent<Animator>();
    }

    void Update()
    {
        bool moving = moveInput.sqrMagnitude > 0.01f;   
        animator.SetBool("IsMoving", moving);

        if (moving)
        {
            animator.SetFloat("InputX", moveInput.x);
            animator.SetFloat("InputY", moveInput.y);           
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}
