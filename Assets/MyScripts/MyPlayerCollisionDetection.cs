using UnityEngine;

public class MyPlayerCollisionDetection : MonoBehaviour
{
    public MyPlayer myPlayer;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "death")
        {
            //Death Behavior
            myPlayer.Die();
        }
        if (collision.tag == "point")
        {
            //Point + beahvior
            myPlayer.Increment_Score();
        }
    }
}
