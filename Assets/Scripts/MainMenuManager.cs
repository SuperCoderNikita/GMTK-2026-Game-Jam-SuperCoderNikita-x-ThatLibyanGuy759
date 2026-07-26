using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public Transform[] titles; // Drag 4 objects here

    public float rotationAngle = 5f;
    public float rotationSpeed = 2f;
    public float floatAmount = 5f;
    public float floatSpeed = 2f;

    private Vector3[] startPositions;

    void Start()
    {
        startPositions = new Vector3[titles.Length];

        for (int i = 0; i < titles.Length; i++)
        {
            startPositions[i] = titles[i].localPosition;
        }
    }

    void Update()
    {
        for (int i = 0; i < titles.Length; i++)
        {
            float offset = i * 1.5f; // Different starting point for each object

            // Rotate left and right
            titles[i].localRotation = Quaternion.Euler(
                0f,
                0f,
                Mathf.Sin(Time.time * rotationSpeed + offset) * rotationAngle
            );

            // Bob up and down differently
            titles[i].localPosition = startPositions[i] + Vector3.up *
                Mathf.Sin(Time.time * floatSpeed + offset) * floatAmount;
        }
    }

    public void start()
    {
        SceneManager.LoadScene("Main Game");
    }

    public void quit()
    {
        Application.Quit();
    }
}