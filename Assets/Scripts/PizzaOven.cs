using UnityEngine;
using System.Collections;
using TMPro;

public class PizzaOven : MonoBehaviour
{
    public float interactRadius = 1.5f;
    public PlayerManager player;
    public Transform hoverPoint;      // where the finished pizza waits, above the oven
    public GameObject pizzaPrefab;
    public float cookTime = 4f;

    private bool hasTomato = false;
    private bool hasCheese = false;
    private bool hasBread = false;
    private bool isProcessing = false;
    private GameObject hoveringItem;  
    public TextMeshPro cooldownText;

    void Update()
    {
        if (isProcessing)
        {
            cookTime -= Time.deltaTime;
            if (cookTime < 0) cookTime = 0;
            cooldownText.text = cookTime.ToString("F1");
        }
        else
        {
            cooldownText.text = "";
        }
    }
    
    public void Interact()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance > interactRadius) return;

        // Drop off tomato
        if (player.heldItem == HeldItem.Tomato && !hasTomato && hoveringItem == null && !isProcessing)
        {
            DepositItem();
            hasTomato = true;
            TryStartProcessing();
            return;
        }

        // Drop off cheese
        if (player.heldItem == HeldItem.Cheese && !hasCheese && hoveringItem == null && !isProcessing)
        {
            DepositItem();
            hasCheese = true;
            TryStartProcessing();
            return;
        }

        // Drop off bread
        if (player.heldItem == HeldItem.Bread && !hasBread && hoveringItem == null && !isProcessing)
        {
            DepositItem();
            hasBread = true;
            TryStartProcessing();
            return;
        }

        // Pick up the finished pizza
        if (hoveringItem != null && player.heldItem == HeldItem.None)
        {
            PickupPizza();
        }
    }

    void DepositItem()
    {
        if (player.currentHeldItem != null)
        {
            Destroy(player.currentHeldItem);
            player.currentHeldItem = null;
        }
        player.heldItem = HeldItem.None;
    }

    void TryStartProcessing()
    {
        if (hasTomato && hasCheese && hasBread && !isProcessing)
        {
            StartCoroutine(CookPizza());
        }
    }

    IEnumerator CookPizza()
    {
        isProcessing = true;

        yield return new WaitForSeconds(cookTime);

        hoveringItem = Instantiate(pizzaPrefab, hoverPoint.position, Quaternion.identity, hoverPoint);
        isProcessing = false;

        // reset for the next pizza
        hasTomato = false;
        hasCheese = false;
        hasBread = false;
    }

    void PickupPizza()
    {
        player.currentHeldItem = Instantiate(pizzaPrefab, player.itemHoldPoint.position, Quaternion.identity, player.itemHoldPoint);
        player.heldItem = HeldItem.Pizza;

        Destroy(hoveringItem);
        hoveringItem = null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}