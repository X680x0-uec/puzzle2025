using UnityEngine;

public class BrokenkorituWall_AY : MonoBehaviour
{

    public GameObject korituWall;



    public void BreakekorituWall()
    {
        Debug.Log("korituWall");
        if (korituWall.activeSelf)
        {
            korituWall.SetActive(false);
        }
        else
        {
            korituWall.SetActive(true);
        }
    }


}

