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
    public float difficultyRampRate = 10f;
    public float minStartingTime = 10f;
    private float reduction;
    private float rolledBaseTime;

    void Awake()
    {
        isServed = false;
        isDisapointed = false;

        rolledBaseTime = Random.Range(minBaseTime, maxBaseTime);
        reduction = Time.timeSinceLevelLoad * difficultyRampRate;
        timeToCook = Mathf.Max(rolledBaseTime - reduction, minStartingTime);
    }

    void Update()
    {
        if (isDisapointed)
            return; 

        if (isServed == true)
        {
            timeToCook = 999;
            timerText.text = ":D";
            return;
        }

        timeToCook -= Time.deltaTime;

        if (timeToCook <= 0)
        {
            timeToCook = 0;
            isDisapointed = true;
            timerText.text = ">:-(";
            return;
        } 

        int minutes = Mathf.FloorToInt(timeToCook / 60);
        int seconds = Mathf.FloorToInt(timeToCook % 60);
        timerText.text = string.Format("{0}:{1:00}", minutes, seconds);
    }
}