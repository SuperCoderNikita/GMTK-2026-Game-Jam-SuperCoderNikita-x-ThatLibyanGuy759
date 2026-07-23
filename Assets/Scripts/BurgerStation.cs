using UnityEngine;
using System.Collections;
using TMPro;

public class BurgerStation : MonoBehaviour
{
    public float interactRadius = 1.5f;
    public PlayerManager player;
    public Transform hoverPoint;      
    public GameObject burgerPrefab;
    public float cookAndAssembleTime = 4f;
    private float countdownTimer = 0f;

    private bool hasMeat = false;
    private bool hasBread = false;
    private bool isProcessing = false;
    public TextMeshPro cooldownText;
    private GameObject hoveringItem;  

    void Update()
    {
        if (isProcessing)
        {
            countdownTimer -= Time.deltaTime;
            if (countdownTimer < 0) countdownTimer = 0;
            cooldownText.text = countdownTimer.ToString("F1");
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


        if (player.heldItem == HeldItem.RawMeat && !hasMeat && hoveringItem == null && !isProcessing)
        {
            DepositItem();
            hasMeat = true;
            TryStartProcessing();
            return;
        }


        if (player.heldItem == HeldItem.Bread && !hasBread && hoveringItem == null && !isProcessing)
        {
            DepositItem();
            hasBread = true;
            TryStartProcessing();
            return;
        }


        if (hoveringItem != null && player.heldItem == HeldItem.None)
        {
            PickupBurger();
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
        if (hasMeat && hasBread && !isProcessing)
        {
            StartCoroutine(CookAndAssemble());
        }
    }

    IEnumerator CookAndAssemble()
    {
        isProcessing = true;
        countdownTimer = cookAndAssembleTime;

        yield return new WaitForSeconds(cookAndAssembleTime);

        hoveringItem = Instantiate(burgerPrefab, hoverPoint.position, Quaternion.identity, hoverPoint);
        isProcessing = false;

        hasMeat = false;
        hasBread = false;
    }

    void PickupBurger()
    {
        player.currentHeldItem = Instantiate(burgerPrefab, player.itemHoldPoint.position, Quaternion.identity, player.itemHoldPoint);
        player.heldItem = HeldItem.Burger;

        Destroy(hoveringItem);
        hoveringItem = null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}