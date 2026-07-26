using UnityEngine;
using TMPro;

public class CustomerTimer : MonoBehaviour
{
    public float minBaseTime = 60f;
    public float maxBaseTime = 120f;
    public float timeToCook;
    public TextMeshPro timerText; 
    public bool isDisapointed = false;
    public bool isServed = false;
    public float difficultyRampRate = 0f; // set to 0 since PhaseManager now controls difficulty directly
    public float minStartingTime = 10f;
    private float reduction;
    private float rolledBaseTime;

    public Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();

        isServed = false;
        isDisapointed = false;

        RollTime(); // fallback roll using default min/max, in case Initialize isn't called
    }

    // Called by CustomerQueue right after spawning, using the current phase's patience range
    public void Initialize(float minTime, float maxTime)
    {
        minBaseTime = minTime;
        maxBaseTime = maxTime;
        RollTime();
    }

    void RollTime()
    {
        rolledBaseTime = Random.Range(minBaseTime, maxBaseTime);
        reduction = Time.timeSinceLevelLoad * difficultyRampRate;
        timeToCook = Mathf.Max(rolledBaseTime - reduction, minStartingTime);
    }

    public void MarkServed()
    {
        isServed = true;
        timerText.text = ":D";
        if (animator != null) animator.SetTrigger("GoHappy");
    }

    public void MarkDisappointed()
    {
        isDisapointed = true;
        timerText.text = ">:-(";
        if (animator != null) animator.SetTrigger("GoMad");
    }

    void Update()
    {
        if (isDisapointed)
            return; 

        if (isServed)
        {
            timeToCook = 999;
            return;
        }

        timeToCook -= Time.deltaTime;

        if (timeToCook <= 0)
        {
            timeToCook = 0;
            MarkDisappointed();
            return;
        } 

        int minutes = Mathf.FloorToInt(timeToCook / 60);
        int seconds = Mathf.FloorToInt(timeToCook % 60);
        timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }
}