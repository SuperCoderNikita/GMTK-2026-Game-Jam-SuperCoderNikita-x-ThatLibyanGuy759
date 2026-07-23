using UnityEngine;

public class Basket : MonoBehaviour
{
    public float pickupRadius = 1.5f;
    public PlayerManager player;
    public GameObject itemPrefab; 

    public void TryPickup()
    {
        if (player.heldItem != HeldItem.None) return; 

        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance > pickupRadius) return;

        player.heldItem = HeldItem.RawMeat;
        player.currentHeldItem = Instantiate(itemPrefab, player.itemHoldPoint.position, Quaternion.identity, player.itemHoldPoint);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}