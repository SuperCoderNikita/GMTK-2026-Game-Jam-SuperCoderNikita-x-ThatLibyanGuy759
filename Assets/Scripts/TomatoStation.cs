using UnityEngine;

public class TomatoStation : MonoBehaviour
{
    public float pickupRadius = 1.5f;
    public PlayerManager player;
    public GameObject tomatoPrefab;

    public void TryPickup()
    {
        if (player.heldItem != HeldItem.None) return;

        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance > pickupRadius) return;

        player.heldItem = HeldItem.Tomato;
        player.currentHeldItem = Instantiate(tomatoPrefab, player.itemHoldPoint.position, Quaternion.identity, player.itemHoldPoint);
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}