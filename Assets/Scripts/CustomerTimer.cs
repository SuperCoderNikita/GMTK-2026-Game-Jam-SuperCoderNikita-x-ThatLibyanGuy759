using UnityEngine;
using TMPro;

public class CustomerTimer : MonoBehaviour
{
    public float timeToCook = 60.0f;
    public TextMeshPro timerText; 
    public bool isDisapointed = false;
    public bool isServed = false;

    void Awake()
    {
        isServed = false;
        isDisapointed = false;
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