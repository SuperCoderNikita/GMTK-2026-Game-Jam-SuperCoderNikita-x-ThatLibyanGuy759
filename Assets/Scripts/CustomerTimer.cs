using UnityEngine;
using TMPro;

public class CustomerTimer : MonoBehaviour
{
    public float minBaseTime =120f;
    public float maxBaseTime = 165f;
    public float timeToCook;
    public TextMeshPro timerText; 
    public bool isDisapointed = false;
    public bool isServed = false;
    public float difficultyRampRate = 10f;
    public float minStartingTime = 10f;
    private float reduction;
    private float rolledBaseTime;
    private SpriteRenderer sr;

    public Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();
        isServed = false;
        isDisapointed = false;

        rolledBaseTime = Random.Range(minBaseTime, maxBaseTime);
        reduction = Time.timeSinceLevelLoad * difficultyRampRate;
        timeToCook = Mathf.Max(rolledBaseTime - reduction, minStartingTime);
    }

    public void MarkServed()
    {
        isServed = true;
        timerText.text = "";
        if (animator != null) animator.SetTrigger("GoHappy");
        sr.sortingOrder = -3;
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

    public void MarkDisappointed()
    {
        isDisapointed = true;
        timerText.text = "";
        if (animator != null) animator.SetTrigger("GoMad");
        sr.sortingOrder = -3;
    }
}