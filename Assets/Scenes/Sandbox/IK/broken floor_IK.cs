using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UIは不要な場合もあります

public class brokenfloor_IK : MonoBehaviour
{
    public enum Type { go, notgo }

    public Type type = Type.go; // 床の状態を設定する（進めるかどうか）

    //playerが触れたらフラグが立つように？
    // デフォルトの画像
    public Sprite DefaultImage;
    // デフォルトの画像(崩壊後の画像)
    public Sprite BrokenImage;
    // 画像描画用のコンポーネント
    SpriteRenderer sr;

    private Animator animator;

    void Start()
    {
        // SpriteのSpriteRendererコンポーネントを取得
        sr = gameObject.GetComponent<SpriteRenderer>();
        // Animatorコンポーネントを取得
        animator = GetComponent<Animator>();
    }

    // プレイヤーと壁がぶつかったときに一度だけ呼ばれる2D用の関数
    void OnTriggerEnter2D(Collider2D other)
    {
        // プレイヤーが触れたことを検知
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("プレイヤーと消える床が衝突しました！");
            type = Type.notgo;
            // 崩壊後の画像に切り替える
            sr.sprite = BrokenImage;
            // "Break"という名前のトリガーを発動
            animator.SetTrigger("Break");
        }
    }
}