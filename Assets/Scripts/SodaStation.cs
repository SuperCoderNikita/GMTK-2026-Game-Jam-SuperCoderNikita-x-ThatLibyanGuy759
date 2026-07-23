using UnityEngine;
 
public class SodaStation : MonoBehaviour
{
    public float pickupRadius = 1.5f;
    public PlayerManager player;
    public GameObject sodaPrefab;
 
    public void TryPickup()
    {
        if (player.heldItem != HeldItem.None) return; 
 
        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance > pickupRadius) return;
 
        player.heldItem = HeldItem.Soda;
        player.currentHeldItem = Instantiate(sodaPrefab, player.itemHoldPoint.position, Quaternion.identity, player.itemHoldPoint);
    }
 
    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, pickupRadius);
    }
}