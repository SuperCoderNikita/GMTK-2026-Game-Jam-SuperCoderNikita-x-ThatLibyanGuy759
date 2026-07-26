using UnityEngine;
using System.Collections;
using TMPro;

public class PizzaOven : MonoBehaviour
{
    public float interactRadius = 1.5f;
    public PlayerManager player;
    public Transform hoverPoint;     
    public GameObject checkPrefab;
    public GameObject pizzaPrefab;
    private float cookTime = 4f;

    private bool hasTomato = false;
    private bool hasCheese = false;
    private bool hasBread = false;
    private bool isProcessing = false;
    private GameObject hoveringItem;  
    public TextMeshPro cooldownText;
    public float baseCookTime = 4f;
    public AudioSource audioSource;
    public AudioClip ding;

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


        if (player.heldItem == HeldItem.Tomato && !hasTomato && hoveringItem == null && !isProcessing)
        {
            DepositItem();
            hasTomato = true;
            TryStartProcessing();
            return;
        }


        if (player.heldItem == HeldItem.Cheese && !hasCheese && hoveringItem == null && !isProcessing)
        {
            DepositItem();
            hasCheese = true;
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
        cookTime = baseCookTime;

        yield return new WaitForSeconds(baseCookTime); 

        hoveringItem = Instantiate(checkPrefab, hoverPoint.position, Quaternion.identity, hoverPoint);
        isProcessing = false;
        audioSource.PlayOneShot(ding);
        
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