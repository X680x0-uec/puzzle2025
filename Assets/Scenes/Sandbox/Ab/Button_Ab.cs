using UnityEngine;

public class Button_Ab : MonoBehaviour
{
    public BrokenWall_Ab recever;
    void OnTggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { if (recever != null)
            {
                recever.BreakeWall();
            }
        }
        Destroy(gameObject);
    }
}
