using UnityEngine;

public class Button_AY : MonoBehaviour
{
    public BrokenWall_AY recever;
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            if (recever != null)
            {
                recever.BreakeWall();
            }
        }
        Destroy(gameObject);
    }
}
