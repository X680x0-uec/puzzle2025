using UnityEngine;

/// <summary>
/// 壁の種類（共通壁、色付き壁）を管理
/// </summary>
public class Wall_TY : MonoBehaviour
{
    public PlayerColor_TY.PlayerType interactablePlayer = PlayerColor_TY.PlayerType.None; // Noneは共通壁
}