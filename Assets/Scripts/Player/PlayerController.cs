using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private SpriteRenderer sr;
    private Rigidbody2D rb;
    private PlayerCollisionChecker pcc;

    [Header("人物属性")]
    public float speed;
    public float jumpSpeed;

    [Header("人物状态")]
    public int direction;

    [Header("输入检测")]
    public float axisX;
    public float axisY;
    public float axisRawX;
    public float axisRawY;

    [Header("行为状态")]
    public bool jumpPressed;
    public bool isJumping;
    public bool isClimbing;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        pcc = GetComponent<PlayerCollisionChecker>();
    }

    void Update()
    {
        CheckInput();
    }

    void FixedUpdate()
    {
        Move();
        Jump();
        Climb();
    }

    private void CheckInput() {
        axisX = Input.GetAxis("Horizontal");
        axisRawX = Input.GetAxisRaw("Horizontal");
        axisY = Input.GetAxis("Vertical");
        axisRawY = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.W) && pcc.onGround) {
            jumpPressed = true;
        }
    }

    /// <summary>
    /// 角色移动
    /// </summary>
    private void Move()
    {
        // 仅地面上可移动
        if (pcc.onGround)
        {
            rb.velocity = new Vector2(axisX * speed, rb.velocity.y);
            switchDirection(axisRawX);
        }
    }

    /// <summary>
    /// 转换人物朝向
    /// </summary>
    /// <param name="_dir">0->左 1->右</param>
    void switchDirection(float _dir) {
        if (_dir == 1)
        {
            direction = 1;
            sr.flipX = false;
        }
        else if(_dir == -1){
            direction = -1;
            sr.flipX = true;
        }
    }

    /// <summary>
    /// 角色跳跃
    /// </summary>
    private void Jump() {
        // 起跳
        if (jumpPressed && pcc.onGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);

            isJumping = true;
            jumpPressed = false;
            pcc.onGround = false;
        }
        // 跳跃结束 重置跳跃状态
        else
        {
            isJumping = false;
        }
    }

    /// <summary>
    /// 角色攀爬
    /// </summary>
    private void Climb() {
        if (pcc.canClimb) {
            rb.bodyType = RigidbodyType2D.Static;

            isClimbing = true;

            // 此处应衔接动画 匹配动画演出点
        }
    }

    /// <summary>
    /// 修正人物攀爬动作起始位置
    /// </summary>
    /// <param name="offset">二维向量偏移量 X作为底部射线到碰撞体距离 Y为下侧射线到碰撞体距离</param>
    public void FixPlayerClimbPosition(Vector2 offset) {
        Vector2 pos = transform.position;
        pos.x += offset.x * direction;
        pos.y -= offset.y;

        transform.position = pos;
    }
}
