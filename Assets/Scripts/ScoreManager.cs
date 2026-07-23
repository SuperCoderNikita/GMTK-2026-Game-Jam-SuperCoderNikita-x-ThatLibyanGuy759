using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public float score = 2.5f;
    public TextMeshProUGUI scoreText;
    void Update()
    {
        score = Mathf.Clamp(score, 0f, 5f);
        scoreText.text = score.ToString();
    }
}
