using UnityEngine;

public class brokenfloor_IK : MonoBehaviour
{
    public enum Type { go, notgo }
    
    public Type type = Type.go; // 床の状態を設定する（進めるかどうか）
}