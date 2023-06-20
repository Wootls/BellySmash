using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemySpawn : MonoBehaviour
{
    //생성된 enemy 갯수
    [HideInInspector]
    public static int enemyCount;

    //생성된 boss 갯수
    [HideInInspector]
    public static int bossCount;

    //파괴된 enemy 갯수
    [HideInInspector]
    public int destroyEnemyCount;

    //파괴된 boss 갯수
    [HideInInspector]
    public int destroyBossCount;

    //enemy 프리팹
    public GameObject enemyPrefab;
    public GameObject bossPrefab;

    //생성지점
    public Transform start;

    //스폰타이머 - 7초 간격
    public float spawnTimer = 7f;
    private float countdown = 2f;

    //spawn Timer text
    public TextMeshProUGUI countdownText;

    private void Start()
    {
        //생성된 enemy의 수 초기화
        enemyCount = 0;

        //
        destroyEnemyCount = 0;
        destroyBossCount = 0;
    }

    private void Update()
    {
        //Timer
        if (bossCount == 1)
            return;
        else
        {
            if (countdown <= 0)
            {
                //파괴된 enemy 수, 생성된 enemy 수 같으면
                if ((destroyEnemyCount%10) == 0 && enemyCount == 10)
                {
                    //보스 소환, 생성된 enemy 수 초기화
                    SpawnBoss(bossPrefab);
                    bossCount++;
                    enemyCount = 0;
                }
                // 생성된 enemy 수가 10 미만이면 enemy 소환
                else if (enemyCount < 10)
                {
                    SpawnEnemy(enemyPrefab);
                    enemyCount++;
                }
                countdown = spawnTimer;
            }
            else
            {
                countdown -= Time.deltaTime;
            }
        }

        countdownText.text = Mathf.Round(countdown).ToString();
    }

    //enemy 생성
    private void SpawnEnemy(GameObject prefab)
    {
        Instantiate(prefab, start.position, Quaternion.identity);
    }

    //boss 생성
    private void SpawnBoss(GameObject prefab)
    {
        Instantiate(prefab, start.position, Quaternion.identity);
    }
}
