using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAi7 : MonoBehaviour
{
    //타겟 설정
    private Transform target;

    //공격오브젝트
    public GameObject eSmash;
    private Animator anim;

    //rigidbody 
    private Rigidbody rb;

    public float gravityForce;

    public sEnemyState eState = sEnemyState.E_Idle;

    //무빙 속도
    public float moveSpeed = 6f;
    //공격 범위 
    private float attackRange = 2f;

    //위치값 받아오는 시간
    private float timer;
    public float startTime;

    //공격 딜레이
    public float attackDelay = 2f;
    private float lastAttackTime = 0f;

    //enemy공격 시간
    private float enemyAttTimer;
    public float enemyAttTime;

    //랜덤위치 값들
    private float dirX;
    private float dirZ;
    //랜덤시간 받아오기 
    private float randTime;

    public Vector3 randDir;
    public Vector3 targetPos;

    private Vector3 resetPos;

    //보스확인
    public bool isBoss = false;

    void Start()
    {
        //시작위치 시간 초기화
        timer = startTime;
        anim = GetComponent<Animator>();
        resetPos = new Vector3(0, 0, 0);
        target = GameObject.Find("P_Bebelly").transform;

        //rigidbody 받아오기
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //랜덤위치 좌표 찍음
        if (timer <= 0f)
        {
            //x, z좌표 랜덤 값 
            dirX = transform.position.x + Random.Range(-7.5f, 7.5f);
            dirZ = transform.position.y + Random.Range(-7.5f, 7.5f);
            //시간 랜덤값 
            randTime = Random.Range(0.3f, 1.3f);
            timer = randTime;
        }
        timer -= Time.deltaTime;

        EnemyUpdate();
    }

    //enemy의 상태 관리 
    private void EnemyUpdate()
    {
        Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, target.position.z);
        float distance = Vector3.Distance(targetPosition, transform.position);

        switch (eState)
        {
            case sEnemyState.E_Idle:
                if(distance < attackRange)
                {
                    SetState(sEnemyState.E_Attack);
                }
                else
                {
                    StartCoroutine(WalkState());
                }
                break;

            case sEnemyState.E_Walk:
                if (distance <= attackRange * 1.2f && distance > attackRange)
                {
                    EnemyWalk(targetPosition);

                }
                else if(distance <= attackRange)
                {
                    SetState(sEnemyState.E_Attack);
                }
                else
                {
                    EnemyRandomWalk();
                }
                
                break;

            case sEnemyState.E_Attack:

                if ((Time.time - lastAttackTime) > attackDelay)
                {
                    StartCoroutine(EnemyAttack());
                }
                
                if(distance > attackRange)
                {
                    SetState(sEnemyState.E_Walk);
                }
                break;

            case sEnemyState.E_Win:
                break;

            case sEnemyState.E_Lose:
                break;
        }
    }

    IEnumerator WalkState()
    {
        yield return new WaitForSeconds(1.5f);
        SetState(sEnemyState.E_Walk);
    }

    private void Attack()
    {
        if(enemyAttTimer <= 0f)
        {
            StartCoroutine(EnemyAttack());
            enemyAttTimer = enemyAttTime;
        }
        enemyAttTimer -= Time.deltaTime;
    }

    IEnumerator EnemyAttack()
    {
        lastAttackTime = Time.time;
        eSmash.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        eSmash.SetActive(false);
        //yield return new WaitForSeconds(2f);
    }

    public void SetState(sEnemyState state)
    {
        if (eState == state)
            return;
        eState = state;
        anim.SetInteger("eState", (int)eState);
    }

    private void EnemyWalk(Vector3 targetPos)
    {
        Vector3 dir = targetPos - transform.position;
        transform.Translate(dir.normalized * Time.deltaTime * moveSpeed);
        transform.LookAt(targetPos);
    }

    private void EnemyRandomWalk()
    {
        //enemy의 시선 처리
        Vector3 targetLook = new Vector3(dirX, transform.position.y, dirZ);

        //랜덤 이동 좌표
        targetPos = new Vector3(dirX, 0, dirZ);
        //transform.Translate(randDir.normalized * Time.deltaTime * moveSpeed);

        randDir = targetPos - transform.position;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, randDir.normalized, out hit))
        {
            Debug.Log("tag" + hit.transform.tag);

            if (hit.transform.tag == "PlayGround")
            {
                //Lerp사용해서 부드럽게 이동
                transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime);
                transform.LookAt(targetLook);
            }            
        }
    }
}

public enum sEnemyState
{
    E_Idle,
    E_Walk,
    E_Attack,
    E_Win,
    E_Lose,
}