using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public float interactRadius = 1.5f;
    public PlayerManager player;

    public void Interact()
    {
        if (player.heldItem == HeldItem.None) return; // nothing to throw away

        float distance = Vector2.Distance(player.transform.position, transform.position);
        if (distance > interactRadius) return;

        if (player.currentHeldItem != null)
        {
            Destroy(player.currentHeldItem);
            player.currentHeldItem = null;
        }

        player.heldItem = HeldItem.None;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, interactRadius);
    }
}