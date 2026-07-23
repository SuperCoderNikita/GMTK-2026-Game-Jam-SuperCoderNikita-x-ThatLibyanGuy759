using UnityEngine;

public class FoodTestFile : MonoBehaviour
{
    public PlayerManager player;

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            player.hasFood = true;
        }
    }
}
