using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

/// <summary>
/// アクター操作・制御クラス
/// </summary>
public class ActorController : MonoBehaviour
{
    // オブジェクト・コンポーネント参照
    private Rigidbody2D rigidbody2D;
    private SpriteRenderer spriteRenderer;
    private Collider2D collider2D;
    public CameraController cameraController; // カメラ制御クラス

    // 移動関連変数
    [SerializeField] private float xSpeed = 6.0f; // X方向移動速度
    private bool rightFacing = true; // 向いている方向(true.右向き false:左向き)
    private float remainJumpTime;   // 空中でのジャンプ入力残り受付時間

    // ジャンプ力をInspectorから設定可能にする
    [SerializeField] private float jumpPower = 10.0f;
    
    // ジャンプ中のジャンプ力加算量
    [SerializeField] private float jumpAddPower = 30.0f;

    // 摩擦の設定
    [SerializeField] private PhysicsMaterial2D noFriction; // 滑らない摩擦材質

    // スタート位置
    private Vector3 startPosition;

    // Start（オブジェクト有効化時に1度実行）
    void Start()
    {
        // コンポーネント参照取得
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider2D = GetComponent<Collider2D>();

        // カメラ初期位置
        cameraController.SetPosition(transform.position);

        // 変数初期化
        rightFacing = true; // 最初は右向き

        // Rigidbody2Dの回転を固定
        rigidbody2D.freezeRotation = true;

        // 摩擦材質の設定
        collider2D.sharedMaterial = noFriction;

        // スタート位置を保存
        startPosition = transform.position;
    }

    // Update（1フレームごとに1度ずつ実行）
    void Update()
    {
        // 入力処理
        HandleMovementInput();
        HandleJumpInput();

        // 画面外に出たらスタート位置に戻る
        if (transform.position.y < -10.0f)
        {
            ResetPosition();
        }
    }

    /// <summary>
    /// 移動入力処理
    /// </summary>
    private void HandleMovementInput()
    {
        // X方向移動入力
        if (Input.GetKey(KeyCode.RightArrow))
        {
            MoveRight();
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        else
        {
            StopMoving();
        }

        // カメラ位置更新
        cameraController.SetPosition(transform.position);
    }

    /// <summary>
    /// ジャンプ入力処理
    /// </summary>
    private void HandleJumpInput()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StartJump();
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            ContinueJump();
        }
        else if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            EndJump();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            ApplyDownwardForce();
        }

        // 空中でのジャンプ入力受付時間減少
        if (remainJumpTime > 0.0f)
            remainJumpTime -= Time.deltaTime;
    }

    private void MoveRight()
    {
        // X方向移動速度をプラスに設定
        xSpeed = 6.0f;

        // 右向きフラグon
        rightFacing = true;

        // スプライトを通常の向きで表示
        spriteRenderer.flipX = false;
    }

    private void MoveLeft()
    {
        // X方向移動速度をマイナスに設定
        xSpeed = -6.0f;

        // 右向きフラグoff
        rightFacing = false;

        // スプライトを左右反転した向きで表示
        spriteRenderer.flipX = true;
    }

    private void StopMoving()
    {
        // X方向の移動を停止
        xSpeed = 0.0f;
    }

    private void StartJump()
    {
        // ジャンプ力を適用
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpPower);
        // 空中でのジャンプ入力受け付け時間設定
        remainJumpTime = 0.25f;
    }

    private void ContinueJump()
    {
        if (remainJumpTime <= 0.0f)
            return;

        // ジャンプ力加算を適用
        rigidbody2D.velocity += new Vector2(0.0f, jumpAddPower * Time.deltaTime);
    }

    private void EndJump()
    {
        remainJumpTime = -1.0f;
    }

    private void ApplyDownwardForce()
    {
        // 下方向の力を計算
        float downwardForce = -5.0f;
        // 下方向の力を適用
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, downwardForce);
    }

    private void ResetPosition()
    {
        // 位置をスタート位置にリセット
        transform.position = startPosition;

        // 速度をゼロにリセット
        rigidbody2D.velocity = Vector2.zero;

        // 向きを右向きにリセット
        rightFacing = true;
        spriteRenderer.flipX = false;
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