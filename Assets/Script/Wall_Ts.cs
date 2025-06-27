using UnityEngine;

/// <summary>
/// 壁の種類（共通壁、色付き壁）を管理
/// </summary>
public class Wall_Ts : MonoBehaviour
{
    public PlayerColor_Ts.PlayerType interactablePlayer = PlayerColor_Ts.PlayerType.None; // Noneは共通壁
}