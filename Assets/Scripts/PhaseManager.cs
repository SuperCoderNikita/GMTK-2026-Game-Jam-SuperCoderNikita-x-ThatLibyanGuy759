using UnityEngine;

[System.Serializable]
public class PhaseSettings
{
    public int minNpcs;
    public int maxNpcs;
    public float minPatience;
    public float maxPatience;
}

public class PhaseManager : MonoBehaviour
{
    public PhaseSettings[] phases = new PhaseSettings[]
    {
        new PhaseSettings { minNpcs = 1, maxNpcs = 2, minPatience = 55f, maxPatience = 60f },
        new PhaseSettings { minNpcs = 2, maxNpcs = 3, minPatience = 45f, maxPatience = 50f },
        new PhaseSettings { minNpcs = 3, maxNpcs = 4, minPatience = 35f, maxPatience = 40f },
        new PhaseSettings { minNpcs = 4, maxNpcs = 5, minPatience = 25f, maxPatience = 30f },
        new PhaseSettings { minNpcs = 5, maxNpcs = 6, minPatience = 10f, maxPatience = 15f },
    };

    public int servedNeededPerPhase = 8;
    public CustomerQueue[] lanes;
    public float spawnInterval = 3f;

    private int currentPhaseIndex = 0;
    private int servedThisPhase = 0;
    private int currentMaxActiveTotal;

    void Start()
    {
        ApplyPhase(currentPhaseIndex);
        InvokeRepeating(nameof(TrySpawn), spawnInterval, spawnInterval);
    }

    // Call this whenever a customer is successfully served (OrderComplete)
    public void OnCustomerServed()
    {
        servedThisPhase++;

        if (servedThisPhase >= servedNeededPerPhase && currentPhaseIndex < phases.Length - 1)
        {
            servedThisPhase = 0;
            currentPhaseIndex++;
            ApplyPhase(currentPhaseIndex);
        }
    }

    void ApplyPhase(int index)
    {
        PhaseSettings phase = phases[index];

        // roll ONE total cap for the whole board, not per lane
        currentMaxActiveTotal = Random.Range(phase.minNpcs, phase.maxNpcs + 1);

        foreach (var lane in lanes)
        {
            lane.SetPatienceRange(phase.minPatience, phase.maxPatience);
        }
    }

    void TrySpawn()
    {
        int totalActive = 0;
        foreach (var lane in lanes)
            totalActive += lane.ActiveCount;

        if (totalActive >= currentMaxActiveTotal) return;

        // pick the lane with the fewest active customers that still has physical space
        CustomerQueue bestLane = null;
        foreach (var lane in lanes)
        {
            if (!lane.HasSpace) continue;
            if (bestLane == null || lane.ActiveCount < bestLane.ActiveCount)
                bestLane = lane;
        }

        if (bestLane != null)
            bestLane.SpawnCustomer();
    }
}