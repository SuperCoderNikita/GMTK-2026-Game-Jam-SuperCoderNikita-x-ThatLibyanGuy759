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

    public AudioSource audioSource;
    public AudioClip pickupSound;

    private HeldItem previousHeldItem = HeldItem.None;

    private static readonly int InputX = Animator.StringToHash("InputX");
    private static readonly int InputY = Animator.StringToHash("InputY");

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
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

       
        if (previousHeldItem == HeldItem.None && heldItem != HeldItem.None)
        {
            if (audioSource != null && pickupSound != null)
                audioSource.PlayOneShot(pickupSound);
        }
        previousHeldItem = heldItem;
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