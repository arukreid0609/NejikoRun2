using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NejikoController : MonoBehaviour
{
    const int MinLane = -2;
    const int MaxLane = 2;
    const float LaneWidth = 1.0f;
    const int DefaultLife = 3;
    const float StunDuration = 0.5f;

    CharacterController controller;
    Rigidbody rd;
    bool isGrounded = true;
    CapsuleCollider col;
    Animator animator;

    Vector3 moveDirection = Vector3.zero;
    int targetLane;
    int life = DefaultLife;
    float recoverTime = 0.0f;

    public float gravity;
    public float speedZ;
    public float speedX;
    public float speedJump;
    public float accelerationZ;

    public int Life()
    {
        return life;
    }

    bool IsStun()
    {
        return recoverTime > 0.0f || life <= 0;
    }

    void Start()
    {
        // controller = GetComponent<CharacterController>();
        rd = GetComponent<Rigidbody>();
        col = GetComponent<CapsuleCollider>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // デバッグ用
        if (Input.GetKeyDown("left")) MoveToLeft();
        if (Input.GetKeyDown("right")) MoveToRight();
        if (Input.GetKeyDown("space")) Jump();

        if (IsStun())
        {
            // 動きを止め気絶状態からの復帰カウントを進める
            moveDirection.x = 0.0f;
            moveDirection.z = 0.0f;
            recoverTime -= Time.deltaTime;
        }
        else
        {
            // 徐々に加速しZ方向に常に前進させる
            float acceleratedZ = moveDirection.z + (accelerationZ * Time.deltaTime);
            moveDirection.z = Mathf.Clamp(acceleratedZ, 0, speedZ);

            // X方向は目標のポジションまでの差分の割合で速度を計算
            float ratioX = (targetLane * LaneWidth - transform.position.x) / LaneWidth;
            moveDirection.x = ratioX * speedX;
        }
        // 重力分の力を毎フレーム追加
        moveDirection.y -= gravity * Time.deltaTime;

        // 移動実行
        // Vector3 globalDirection = transform.TransformDirection(moveDirection);
        // controller.Move(globalDirection * Time.deltaTime);
        // rd.velocity = moveDirection;


        // 移動後接地してたらY方向の速度はリセットする
        // if (controller.isGrounded) moveDirection.y = 0;
        // if (isGrounded) moveDirection.y = 0;

        // 速度が0以上なら走っているフラグをtrueにする
        animator.SetBool("run", moveDirection.z > 0.0f);
    }
    private void FixedUpdate()
    {
        // rd.velocity = moveDirection;
        rd.AddForce(moveDirection);
    }

    // 左のレーンに移動を開始
    public void MoveToLeft()
    {
        if (IsStun()) return;
        // if (controller.isGrounded && targetLane > MinLane) targetLane--;
        if (isGrounded && targetLane > MinLane) targetLane--;
    }
    public void MoveToRight()
    {
        if (IsStun()) return;
        // if (controller.isGrounded && targetLane < MaxLane) targetLane++;
        if (isGrounded && targetLane < MaxLane) targetLane++;

    }

    public void Jump()
    {
        if (IsStun()) return;
        // if (controller.isGrounded)
        if (isGrounded)
        {
            moveDirection.y = speedJump;

            // ジャンプトリガーを設定
            animator.SetTrigger("jump");
        }
    }

    // CharcterControllerに衝突判定が生じたときの処理
    // void OnControllerColliderHit(ControllerColliderHit hit)
    // {
    //     if (IsStun()) return;

    //     if (hit.gameObject.tag == "Robo")
    //     {
    //         // ライフを減らして気絶状態に移行
    //         life--;
    //         recoverTime = StunDuration;

    //         // ダメージトリガーを設定
    //         animator.SetTrigger("damage");

    //         // ヒットしたオブジェクトは削除
    //         Destroy(hit.gameObject);
    //         //
    //     }
    // }
    void OnCollisionEnter(Collision other)
    {
        if (IsStun()) return;

        // if (other.gameObject.tag == "Ground") isGrounded = true;
        if (other.gameObject.tag == "Robo")
        {
            // ライフを減らして気絶状態に移行
            life--;
            recoverTime = StunDuration;

            // ダメージトリガーを設定
            animator.SetTrigger("damage");

            // ヒットしたオブジェクトは削除
            Destroy(other.gameObject);
            //
        }
    }
    void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "Ground") isGrounded = false;
    }
    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.tag == "Ground")
        {
            isGrounded = true;
            // moveDirection.y = 0;
        }
    }
}
