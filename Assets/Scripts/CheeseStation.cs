using UnityEngine;

public class CheeseStation : MonoBehaviour
{
    public float pickupRadius = 1.5f;
    public PlayerManager player;
    public GameObject cheesePrefab;

    public void TryPickup()
    {
        if (player.heldItem != HeldItem.None) return;

        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance > pickupRadius) return;

        player.heldItem = HeldItem.Cheese;
        player.currentHeldItem = Instantiate(cheesePrefab, player.itemHoldPoint.position, Quaternion.identity, player.itemHoldPoint);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}