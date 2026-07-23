using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public float score = 2.5f;
    public TextMeshProUGUI scoreText;
    void Update()
    {
        scoreText.text = score.ToString();
    }
}
