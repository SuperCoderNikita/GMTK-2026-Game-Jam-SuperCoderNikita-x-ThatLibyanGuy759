using UnityEngine;
using System.Collections;

public class Oven : MonoBehaviour
{
    public float interactRadius = 1.5f;
    public PlayerManager player;
    public Transform hoverPoint;
    public GameObject cookedMeatPrefab;
    public float cookTime = 3f;

    private bool isCooking = false;
    private GameObject hoveringItem;

    public void Interact()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance > interactRadius) return;

        if (player.heldItem == HeldItem.RawMeat && !isCooking && hoveringItem == null)
        {
            StartCoroutine(CookMeat());
        }

        else if (hoveringItem != null && player.heldItem == HeldItem.None)
        {
            PickupCookedMeat();
        }
    }

    IEnumerator CookMeat()
    {
        isCooking = true;

        Destroy(player.currentHeldItem);
        player.currentHeldItem = null;
        player.heldItem = HeldItem.None;

        yield return new WaitForSeconds(cookTime);

        hoveringItem = Instantiate(cookedMeatPrefab, hoverPoint.position, Quaternion.identity, hoverPoint);
        isCooking = false;
    }

    void PickupCookedMeat()
    {
        player.currentHeldItem = Instantiate(cookedMeatPrefab, player.itemHoldPoint.position, Quaternion.identity, player.itemHoldPoint);
        player.heldItem = HeldItem.CookedMeat;

        Destroy(hoveringItem);
        hoveringItem = null;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}