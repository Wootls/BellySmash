using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    //랜덤 좌표  
    private float moveX;
    private float moveY;

    //이동속도, 회전속도
    public float moveSpeed = 10f;
    public float rotationSpeed = 10f;

    //공격딜레이시간, 
    public float attackDelay = 2f;
    private float lastAttackTime = 0f;

    //배치기
    public GameObject pSmash;
    public Image smashImage;    

    //터치패드
    public VariableJoystick joystick;

    //애니메이션 
    private Animator anim;

    Vector3 moveVec;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (GameManager.instance.timer <= 0f)
        {
            anim.SetTrigger("isLose");
            return;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            Smash();
        }
        else
        {
            //움직임 
            ShowMove();
        }
        Movement();

        //버튼 쿨타임 표시
        if (Time.time - lastAttackTime < attackDelay)
        {
            smashImage.fillAmount += Time.deltaTime/attackDelay ;
        }
    }

    private void FixedUpdate()
    {
        //키보드 이동
        /*
        //이동 
        moveX = Input.GetAxis("Horizontal");
        moveY = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(moveX, 0f, moveY);

        if (!(moveX ==0 && moveY == 0))
        {
            transform.position += dir * moveSpeed * Time.deltaTime;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);
        }
        */
    }

    public void Movement()
    {
        //이동 
        moveX = joystick.Horizontal;
        moveY = joystick.Vertical;

        Vector3 dir = new Vector3(moveX, 0f, moveY);

        if (!(moveX == 0 && moveY == 0))
        {
            transform.position += dir * moveSpeed * Time.deltaTime;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir), Time.deltaTime * rotationSpeed);
        }
    }

    void ShowMove()
    {
        moveVec = new Vector3(moveX, 0, moveY).normalized;
        anim.SetBool("isWalk", moveVec != Vector3.zero);
    }

    public void Smash()
    {
        //공격 딜레이 적용
        if (Time.time - lastAttackTime > attackDelay)
        {
            StartCoroutine(SmashDelay());
            smashImage.fillAmount = 0f;
        }
    }

    IEnumerator SmashDelay()
    {
        lastAttackTime = Time.time;
        pSmash.SetActive(true);
        anim.SetTrigger("isAttack");
        yield return new WaitForSeconds(0.28f);
        pSmash.SetActive(false);
    }
}

/*
private void Smash()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if (hit.transform.tag == "Enemy")
            {
                if (hit.rigidbody != null)
                {
                    hit.rigidbody.AddForce(-hit.normal * attackForce);
                }
            }
        }

    }
*/
