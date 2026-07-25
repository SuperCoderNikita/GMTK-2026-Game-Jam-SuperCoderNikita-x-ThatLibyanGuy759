using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public float score = 2.5f;
    public Image[] starImages; // drag all 5 stars in, left to right
    public float fillSpeed = 1f; // how fast the stars animate, in "stars per second"

    void Update()
    {
        score = Mathf.Clamp(score, 0f, 5f);
        UpdateStars();
    }

    void UpdateStars()
    {
        for (int i = 0; i < starImages.Length; i++)
        {
            float targetFill = Mathf.Clamp(score - i, 0f, 1f);
            float currentFill = starImages[i].fillAmount;

            starImages[i].fillAmount = Mathf.MoveTowards(currentFill, targetFill, fillSpeed * Time.deltaTime);
        }
    }
}