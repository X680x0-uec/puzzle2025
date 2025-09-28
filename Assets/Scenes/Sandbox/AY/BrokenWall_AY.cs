using UnityEngine;

public class BrokenWall_AY : MonoBehaviour
{

    public GameObject BrokenWallBefore; // BrokenWallを追加


    public void BreakeWall()
    {

        if (BrokenWallBefore.activeSelf) // Check if the "before" wall is active
        {
            BrokenWallBefore.SetActive(false); // If it is, deactivate it
        }
        else
        {
            BrokenWallBefore.SetActive(true); // If it's not, activate it
        }
    }

}

