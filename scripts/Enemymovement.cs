using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemymovement : Enemy
{
    public float movespeed;
    public float startWaitTime;
    private float waitTime;
  
    public float chasespeed;
    public float radius;
    private Transform playerTransform;

    // 新增障碍物检测参数
    public float obstacleCheckDistance = 1.5f;  // 射线检测距离
    public LayerMask obstacleLayer;             // 障碍物层级
    public float avoidAngle = 30f;               // 避开障碍物的角度
    public float raycastOffset = 0.5f;          // 射线起点偏移量

    void Start()
    {
        base.Start();
        playerTransform = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    public void Update()
    {
        base.Update();

        

        if (playerTransform != null)
        {
            float distance = (transform.position - playerTransform.position).sqrMagnitude;

            if (distance < radius)
            {
                // 获取指向玩家的方向
                Vector2 directionToPlayer = (playerTransform.position - transform.position).normalized;
                Vector2 adjustedDirection = directionToPlayer;

                // 检测前方是否有障碍物
                if (IsObstacleInPath(directionToPlayer))
                {
                    // 尝试向左避让
                    Vector2 leftDirection = Quaternion.Euler(0, 0, avoidAngle) * directionToPlayer;
                    // 尝试向右避让
                    Vector2 rightDirection = Quaternion.Euler(0, 0, -avoidAngle) * directionToPlayer;

                    // 选择一个可行的避让方向
                    if (!IsObstacleInPath(leftDirection))
                    {
                        adjustedDirection = leftDirection;
                    }
                    else if (!IsObstacleInPath(rightDirection))
                    {
                        adjustedDirection = rightDirection;
                    }
                    // 如果两侧都有障碍物，可以添加向上/向下避让逻辑
                    else
                    {
                        Vector2 upDirection = Quaternion.Euler(0, 0, avoidAngle * 2) * directionToPlayer;
                        if (!IsObstacleInPath(upDirection))
                        {
                            adjustedDirection = upDirection;
                        }
                        else
                        {
                            adjustedDirection = Quaternion.Euler(0, 0, -avoidAngle * 2) * directionToPlayer;
                        }
                    }
                }

                // 向调整后的方向移动
                Vector2 targetPosition = (Vector2)transform.position + adjustedDirection;
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, chasespeed * Time.deltaTime);
            }
        }
    }

    // 检测路径上是否有障碍物
    private bool IsObstacleInPath(Vector2 direction)
    {
        // 设置射线起点（稍微偏离自身，避免检测到自己）
        Vector2 rayOrigin = (Vector2)transform.position + direction * raycastOffset;
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, obstacleCheckDistance, obstacleLayer);

        // 绘制射线（Scene视图中可见）
        Debug.DrawRay(rayOrigin, direction * obstacleCheckDistance, Color.red);

        return hit.collider != null;
    }

    
}