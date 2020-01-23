using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionChecker : MonoBehaviour
{
    private PlayerController pc;

    [Header("环境检测设置")]
    public float rayCastLength = 0.2f;
    public LayerMask environment;

    [Header("环境状态检测")]
    public bool onGround;
    public bool canClimb;

    [Header("检测设置")]
    public Vector2 offsetGround;
    public Vector2 offsetClimb_Bottom;
    public Vector2 offsetClimb_Top;
    public Vector2 offsetClimb_Down;

    void Start()
    {
        pc = GetComponent<PlayerController>();
    }

    void Update()
    {
        CheckOnGround();
        CheckCanClimb();
    }

    void CheckOnGround() {
        RaycastHit2D hit = Raycast(offsetGround, Vector2.down, rayCastLength, environment);
        if (hit) {
            onGround = true;
        }
    }

    void CheckCanClimb() {
        RaycastHit2D hit_Bottom = Raycast(offsetClimb_Bottom, Vector2.right * pc.direction, rayCastLength * 3, environment);
        RaycastHit2D hit_Top = Raycast(offsetClimb_Top, Vector2.right *pc.direction, rayCastLength * 3, environment);
        RaycastHit2D hit_Down = Raycast(offsetClimb_Down, Vector2.down, rayCastLength * 3, environment);

        if (hit_Bottom && hit_Down && !hit_Top)
        {
            canClimb = true;
            // 修正人物位置
            pc.FixPlayerClimbPosition(new Vector2(hit_Bottom.distance, hit_Down.distance));
        }
    }
    
    /// <summary>
    /// 重载2D射线 将射线起始点定位于物体中心
    /// </summary>
    /// <param name="_offset"></param>
    /// <param name="_direction"></param>
    /// <param name="_length"></param>
    /// <param name="_layer"></param>
    /// <returns></returns>
    RaycastHit2D Raycast(Vector2 _offset, Vector2 _direction, float _length, LayerMask _layer) {
        // 左右方向修正
        Vector2 pos = transform.position;
        Vector2 offset = _offset;
        offset.x *= pc.direction;

        RaycastHit2D hit = Physics2D.Raycast(pos + offset, _direction, _length, _layer);

        Color color = hit ? Color.red : Color.green;
        Debug.DrawRay(pos + offset,_direction * _length, color);
        return hit;
    }
}
