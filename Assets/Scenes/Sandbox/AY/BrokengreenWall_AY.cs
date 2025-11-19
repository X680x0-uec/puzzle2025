using UnityEngine;

public class BrokengreenWall_AY : MonoBehaviour
{

    public GameObject greenBrokenWall;
    public AudioClip switchSound;
    [Range(0f, 1f)] public float switchSoundScale = 1.0f;


    public void BreakegreenWall()
    {
        if (AudioManager_TY.Instance != null && switchSound != null)
        {
            AudioManager_TY.Instance.PlaySFX(switchSound, switchSoundScale);
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

