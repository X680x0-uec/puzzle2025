using UnityEngine;

public class DeleteButton_AY : MonoBehaviour
{
    public BrokenyellowWall_AY recever;
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Player"))
        {
            if (recever != null)
            {
                recever.BreakeyellowWall();
            }
        }
        Destroy(gameObject);
    }
}
