using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(PlayerController_TY))]
public class TileDirectionChanger_Trigger : MonoBehaviour
{
    public Tilemap directionTilemap; // 方向タイルが置いてあるTilemap
    private PlayerController_TY playerController;

    void Start()
    {
        playerController = GetComponent<PlayerController_TY>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 触れた位置からタイル座標を計算
        Vector3Int cellPos = directionTilemap.WorldToCell(collision.transform.position);

        if (directionTilemap.HasTile(cellPos))
        {
            Matrix4x4 tileMatrix = directionTilemap.GetTransformMatrix(cellPos);
            float rotationZ = Mathf.Atan2(tileMatrix.m01, tileMatrix.m00) * Mathf.Rad2Deg;

            Vector2 dir = new Vector2(Mathf.Cos(rotationZ * Mathf.Deg2Rad), Mathf.Sin(rotationZ * Mathf.Deg2Rad));

            if (dir.magnitude < 0.1f) return;

            playerController.MoveDirection(dir.normalized);
        }
    }
}
