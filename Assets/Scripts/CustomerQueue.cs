using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerQueue : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform spawnPoint;

    public Transform[] queueSlots;

    public Transform exitPointLeft;
    public Transform exitPointRight;
    public float spawnInterval = 3f;
    public float serveRadius = 1.5f;

    public PlayerManager player;
    public ScoreManager score;

    private List<GameObject> queue = new List<GameObject>();

    

    public bool IsPlayerInRange(Vector2 playerPosition)
    {
        if (queueSlots.Length == 0) return false;
        return Vector2.Distance(playerPosition, queueSlots[0].position) <= serveRadius;
    }

    void OnDrawGizmos()
    {
        if (queueSlots == null || queueSlots.Length == 0) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(queueSlots[0].position, serveRadius);
    }

    void Start()
    {
        InvokeRepeating(nameof(SpawnCustomer), spawnInterval, spawnInterval);
    }

    void Update()
    {
        RefreshQueuePositions();
    }

    void SpawnCustomer()
    {
        if (queue.Count >= queueSlots.Length)
            return;

        GameObject obj = Instantiate(customerPrefab, spawnPoint.position, Quaternion.identity);

        CustomerManager customerMovement = obj.GetComponent<CustomerManager>();
        if (customerMovement == null) customerMovement = obj.AddComponent<CustomerManager>();

        queue.Add(obj);
        RefreshQueuePositions();
    }

    public void ServeFrontCustomer()
    {
        if (player.heldItem == HeldItem.None) return;
        if (queue.Count == 0) return;

        float distance = Vector2.Distance(player.transform.position, queueSlots[0].position);
        if (distance > serveRadius) return;

        GameObject front = queue[0];
        CustomerOrder order = front.GetComponent<CustomerOrder>();
        CustomerTimer frontTimer = front.GetComponent<CustomerTimer>();

        DeliveryResult result = order.DeliverItem(player.heldItem);

       if (result == DeliveryResult.WrongItem)
        {
            score.score -= 0.5f;

            frontTimer.isDisapointed = true;
            if (frontTimer.timerText != null)
                frontTimer.timerText.text = ">:-(";

           
            if (player.currentHeldItem != null)
            {
                Destroy(player.currentHeldItem);
                player.currentHeldItem = null;
            }
            player.heldItem = HeldItem.None;

            queue.RemoveAt(0);
            StartCoroutine(LeaveAndDestroy(front));
            RefreshQueuePositions();
            return;
        }

        if (player.currentHeldItem != null)
        {
            Destroy(player.currentHeldItem);
            player.currentHeldItem = null;
        }
        player.heldItem = HeldItem.None;

        if (result == DeliveryResult.OrderComplete)
        {
            queue.RemoveAt(0);
            StartCoroutine(LeaveAndDestroy(front));
            RefreshQueuePositions();

            frontTimer.isServed = true;
            score.score += 0.5f;
        }
        
    }

    void RefreshQueuePositions()
    {
        for (int i = queue.Count - 1; i >= 0; i--)
        {
            CustomerManager customerMovement = queue[i].GetComponent<CustomerManager>();
            CustomerTimer timer = queue[i].GetComponent<CustomerTimer>();

            if (timer.isDisapointed == true)
            {
                score.score -= 0.5f;
                GameObject leavingCustomer = queue[i];
                queue.RemoveAt(i);
                StartCoroutine(LeaveAndDestroy(leavingCustomer));
                continue;
            }

            customerMovement.SetTarget(queueSlots[i].position);
        }
    }

    IEnumerator LeaveAndDestroy(GameObject customer)
    {
        Transform chosenExit = (Random.value < 0.5f) ? exitPointLeft : exitPointRight;

        CustomerManager customerMovement = customer.GetComponent<CustomerManager>();
        customerMovement.SetTarget(chosenExit.position);

        while (Vector2.Distance(customer.transform.position, chosenExit.position) > 0.05f)
        {
            yield return null;
        }

        Destroy(customer);
    }
}