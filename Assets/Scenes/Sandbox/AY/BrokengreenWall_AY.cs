using UnityEngine;

public class BrokengreenWall_AY : MonoBehaviour
{

    public GameObject greenBrokenWall;
    public AudioClip switchSound;


    public void BreakegreenWall()
    {
        if (AudioManager_TY.Instance != null && switchSound != null)
        {
            AudioManager_TY.Instance.PlaySFX(switchSound);
        }

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

