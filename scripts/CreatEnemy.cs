using UnityEngine;

public class CreatEnemy : MonoBehaviour
{
    // 要生成的敌人预制体 
    public GameObject enemyPrefab;

    // 刷怪间隔时间（秒） 
    public float spawnInterval = 2f;

    // 敌人生成半径（怪物生成范围）
    public float spawnRadius = 2f;

    // 玩家检测半径（单独控制玩家进入的检测范围）
    public float playerDetectionRadius = 10f;

    // 扇形检测的角度（度） - 范围为0-360 
    [Range(0, 360)]
    public float detectionAngle = 90f;

    // 计时器 
    private float spawnTimer = 0f;

    // 玩家Transform引用 
    private Transform playerTransform;
    

    // 新增：跟踪玩家是否已在范围内 
    private bool isPlayerInRange = false;

    void Start()
    {
        // 尝试找到玩家对象（确保玩家有"Player"标签） 
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        else
        {
            Debug.LogWarning("Player not found! Make sure your player has the 'Player' tag.");
        }
    }

    void Update()
    {
        // 检查玩家是否在2D扇形检测范围内（使用玩家检测半径） 
        bool currentInRange = IsPlayerIn2DSector();

        if (currentInRange)
        {
            // 玩家刚进入范围，立即生成第一个敌人
            if (!isPlayerInRange)
            {
                SpawnEnemy();
                spawnTimer = 0f; // 重置计时器开始计算下一次生成时间
                isPlayerInRange = true;
            }
            // 玩家已在范围内，按间隔生成
            else
            {
                spawnTimer += Time.deltaTime;
                if (spawnTimer >= spawnInterval)
                {
                    SpawnEnemy();
                    spawnTimer = 0f;
                }
            }
        }
        else
        {
            // 玩家离开范围，重置状态
            isPlayerInRange = false;
            spawnTimer = 0f;
        }
    
        if (IsPlayerIn2DSector())
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                SpawnEnemy();
                spawnTimer = 0f;
            }
        }
    }


    void SpawnEnemy()
    {
        if (enemyPrefab == null) return;

        // 在生成半径范围内随机生成位置 
        Vector2 randomOffset = Random.insideUnitCircle * spawnRadius;
        Vector3 spawnPosition = new Vector3(transform.position.x + randomOffset.x, transform.position.y + randomOffset.y, transform.position.z);
        Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
    }

    // 2D扇形检测核心逻辑（使用玩家检测半径） 
    bool IsPlayerIn2DSector()
    {
        if (playerTransform == null) return false;

        // 仅使用X和Y轴计算距离（2D平面） 
        Vector2 toPlayer = new Vector2(
            playerTransform.position.x - transform.position.x,
            playerTransform.position.y - transform.position.y
        );

        float distance = toPlayer.magnitude;
        if (distance > playerDetectionRadius) return false;  // 使用玩家检测半径

        // 归一化方向 
        toPlayer.Normalize();

        // 获取2D方向（使用transform.right作为前方向） 
        Vector2 spawnerRight = transform.right;

        // 计算2D角度 
        float angle = Vector2.Angle(toPlayer, spawnerRight);

        return angle <= detectionAngle * 0.5f;
    }

    // 2D扇形Gizmos绘制 
    void OnDrawGizmosSelected()
    {
        // 绘制刷怪范围（黄色）- 使用生成半径 
        Gizmos.color = Color.yellow;
        DrawCircle2D(transform.position, spawnRadius);

        // 绘制检测扇形（红色）- 使用玩家检测半径 
        Gizmos.color = Color.red;
        DrawSector2D(transform.position, transform.right, playerDetectionRadius, detectionAngle);
    }

    // 绘制2D圆形辅助函数 
    void DrawCircle2D(Vector3 center, float radius)
    {
        int segments = 32;
        Vector3 prevPoint = new Vector3(center.x + radius, center.y, center.z);

        for (int i = 1; i <= segments; i++)
        {
            float angle = Mathf.Deg2Rad * (i * 360f / segments);
            Vector3 currentPoint = new Vector3(
                center.x + Mathf.Cos(angle) * radius,
                center.y + Mathf.Sin(angle) * radius,
                center.z
            );
            Gizmos.DrawLine(prevPoint, currentPoint);
            prevPoint = currentPoint;
        }
    }

    // 绘制2D扇形辅助函数 
    void DrawSector2D(Vector3 center, Vector2 forward, float radius, float angleInDegrees)
    {
        if (angleInDegrees >= 360)
        {
            DrawCircle2D(center, radius);
            return;
        }

        int segments = Mathf.Max(5, Mathf.RoundToInt(angleInDegrees / 5));
        float angleStep = angleInDegrees / segments;

        // 计算扇形边界 
        Quaternion startRotation = Quaternion.Euler(0, 0, -angleInDegrees * 0.5f);
        Quaternion endRotation = Quaternion.Euler(0, 0, angleInDegrees * 0.5f);

        Vector2 startDir = startRotation * forward;
        Vector2 endDir = endRotation * forward;

        // 绘制边界线 
        Gizmos.DrawLine(center, center + new Vector3(startDir.x, startDir.y, 0) * radius);
        Gizmos.DrawLine(center, center + new Vector3(endDir.x, endDir.y, 0) * radius);

        // 绘制扇形弧线 
        Vector2 prevDir = startDir;
        for (int i = 1; i <= segments; i++)
        {
            Quaternion rot = Quaternion.Euler(0, 0, -angleInDegrees * 0.5f + angleStep * i);
            Vector2 currentDir = rot * forward;
            Gizmos.DrawLine(
                center + new Vector3(prevDir.x, prevDir.y, 0) * radius,
                center + new Vector3(currentDir.x, currentDir.y, 0) * radius
            );
            prevDir = currentDir;
        }
    }
}