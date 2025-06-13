using UnityEngine;

public class Wall : MonoBehaviour
{
    public Color wallColor;

    private void Start()
    {
        GetComponent<SpriteRenderer>().color = wallColor;
    }
}
