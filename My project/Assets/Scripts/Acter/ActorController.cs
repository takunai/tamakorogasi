
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アクター操作・制御クラス
/// </summary>
public class ActorController : MonoBehaviour
{
    // オブジェクト・コンポーネント参照
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;

    // 移動関連変数
    [HideInInspector] public float xSpeed; // X方向移動速度
    [HideInInspector] public bool rightFacing; // 向いている方向(true.右向き false:左向き)

    // **ジャンプ力をInspectorから設定可能にする**
    [SerializeField] private float jumpPower = 10.0f;

    // Start（オブジェクト有効化時に1度実行）
    void Start()
    {
        // コンポーネント参照取得
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 変数初期化
        rightFacing = true; // 最初は右向き
    }

    // Update（1フレームごとに1度ずつ実行）
    void Update()
    {
        // 左右移動処理
        MoveUpdate();
        // ジャンプ入力処理
        JumpUpdate();
    }

    /// <summary>
    /// Updateから呼び出される左右移動入力処理
    /// </summary>
    private void MoveUpdate()
    {
        // X方向移動入力
        if (Input.GetKey(KeyCode.RightArrow))
        {// 右方向の移動入力
            // X方向移動速度をプラスに設定
            xSpeed = 6.0f;

            // 右向きフラグon
            rightFacing = true;

            // スプライトを通常の向きで表示
            spriteRenderer.flipX = false;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {// 左方向の移動入力
            // X方向移動速度をマイナスに設定
            xSpeed = -6.0f;

            // 右向きフラグoff
            rightFacing = false;

            // スプライトを左右反転した向きで表示
            spriteRenderer.flipX = true;
        }
        else
        {// 入力なし
            // X方向の移動を停止
            xSpeed = 0.0f;
        }
    }

    /// <summary>
    /// Updateから呼び出されるジャンプ入力処理
    /// </summary>
    private void JumpUpdate()
    {
        // ジャンプ操作
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {// ジャンプ開始
            // ジャンプ力を適用
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpPower);
        }

        // DownArrowキーが押されたときの処理
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            // 下方向の力を計算（例として、-5.0fの値を使用）
            float downwardForce = -5.0f;
            // 下方向の力を適用
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, downwardForce);
        }
    }

    // FixedUpdate（一定時間ごとに1度ずつ実行・物理演算用）
    private void FixedUpdate()
    {
        // 移動速度ベクトルを現在値から取得
        Vector2 velocity = rigidbody2D.velocity;
        // X方向の速度を入力から決定
        velocity.x = xSpeed;

        // 計算した移動速度ベクトルをRigidbody2Dに反映
        rigidbody2D.velocity = velocity;
    }
}