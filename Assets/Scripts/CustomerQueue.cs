using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerQueue : MonoBehaviour
{
    public GameObject customerPrefab;
    public Transform spawnPoint;

    public Transform[] queueSlots;

    public Transform exitPoint;

    public float spawnInterval = 3f;
    public float serveRadius = 1.5f;

    public PlayerManager player;
    public CustomerTimer timer;

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
        if (!player.hasFood) return;

        if (queue.Count == 0) return;

        GameObject front = queue[0];

        float distance = Vector2.Distance(player.transform.position, queueSlots[0].position);
        if (distance > serveRadius) return; 
        CustomerTimer frontTimer = front.GetComponent<CustomerTimer>();

        queue.RemoveAt(0);
        StartCoroutine(LeaveAndDestroy(front));
        RefreshQueuePositions();

        player.hasFood = false;
        frontTimer.isServed = true;
    }

    void RefreshQueuePositions()
    {
        for (int i = queue.Count - 1; i >= 0; i--)
        {
            CustomerManager customerMovement = queue[i].GetComponent<CustomerManager>();
            CustomerTimer timer = queue[i].GetComponent<CustomerTimer>();

            if (timer.isDisapointed == true)
            {
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
        CustomerManager customerMovement = customer.GetComponent<CustomerManager>();
        customerMovement.SetTarget(exitPoint.position);

        while (Vector2.Distance(customer.transform.position, exitPoint.position) > 0.05f)
        {
            yield return null;
        }

        Destroy(customer);
    }
}