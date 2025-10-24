using UnityEngine;

public class BrokenyellowWall_AY : MonoBehaviour
{

    public GameObject yellowBrokenWall;


    public void BreakeyellowWall()
    {

        if (yellowBrokenWall.activeSelf)
        {
            yellowBrokenWall.SetActive(false);
        }
        else
        {
            yellowBrokenWall.SetActive(true);
        }
    }

}

