using UnityEngine;

public class BrokenyellowWall_AY : MonoBehaviour
{

    public GameObject yellowBrokenWall;
    public AudioClip switchSound;


    public void BreakeyellowWall()
    {
        if (AudioManager_TY.Instance != null && switchSound != null)
        {
            AudioManager_TY.Instance.PlaySFX(switchSound);
        }

        if (yellowBrokenWall.activeSelf)
        {
            yellowBrokenWall.SetActive(false);
        }
        else
        {
            //紫で通ったときにボタンは消えるが壁が残ってしまう不具合修正
            //yellowBrokenWall.SetActive(true);
        }
    }

}

