using UnityEngine;

public class BrokengreenWall_AY : MonoBehaviour
{

    public GameObject greenBrokenWall;


    public void BreakegreenWall()
    {

        if (greenBrokenWall.activeSelf)
        {
            greenBrokenWall.SetActive(false);
        }
        else
        {
            greenBrokenWall.SetActive(true);
        }
    }

}

