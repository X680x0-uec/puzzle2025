using UnityEngine;

public class BrokenWall_AY : MonoBehaviour
{
    public GameObject BrokenWallAfter;
    public GameObject BrokenWallBefore; // BrokenWallを追加
    void Start()
    {
        if (BrokenWallAfter != null) // ②
        {


            BrokenWallAfter.SetActive(false);

        }
    }

    public void BreakeWall()
    {
        BrokenWallBefore.SetActive(false);
        if (BrokenWallAfter != null) // ⑥
        {
            BrokenWallAfter.SetActive(true); // ⑦
        }

    }

}
