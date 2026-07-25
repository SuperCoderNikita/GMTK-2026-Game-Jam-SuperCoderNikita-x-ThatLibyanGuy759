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

    // Hash string IDs for better performance
    private static readonly int InputX = Animator.StringToHash("InputX");
    private static readonly int InputY = Animator.StringToHash("InputY");

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        // Fix: Use GetComponent ONLY if animator wasn't assigned in the Inspector
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
            // Resume playing the animation walk cycle
            animator.speed = 1f;

            // Pass input values to drive the Blend Tree direction
            animator.SetFloat(InputX, moveInput.x);
            animator.SetFloat(InputY, moveInput.y);
        }
        else
        {
            // Freeze/pause the animator on the current frame
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