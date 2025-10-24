using UnityEngine;

public class korituButton : MonoBehaviour
{
    public BrokenkorituWall_AY recever;
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            if (recever != null)
            {
                recever.BreakekorituWall();
            }
        }
    }
}


