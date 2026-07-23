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
    public Transform itemHoldPoint;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        rb.linearVelocity = moveInput * moveSpeed;
    }
}
