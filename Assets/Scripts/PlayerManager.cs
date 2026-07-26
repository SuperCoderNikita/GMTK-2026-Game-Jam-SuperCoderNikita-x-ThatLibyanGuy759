using UnityEngine;
using UnityEngine.InputSystem; 

public enum HeldItem { None, RawMeat, CookedMeat, Bread, Burger, Soda, Tomato, Cheese, Pizza }

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


    private static readonly int InputX = Animator.StringToHash("InputX");
    private static readonly int InputY = Animator.StringToHash("InputY");

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
    }

    void Update()
    {
        bool moving = moveInput.sqrMagnitude > 0.01f;

        if (moving)
        {
            animator.speed = 1f;

            animator.SetFloat(InputX, moveInput.x);
            animator.SetFloat(InputY, moveInput.y);
        }
        else
        {
            animator.speed = 0f;
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