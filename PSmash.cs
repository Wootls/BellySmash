using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PSmash : MonoBehaviour
{
    private float attackForce = 150f;

    public static float attackCount;

    /*
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.tag == "Enemy")
        {            
            Rigidbody rb = collision.transform.GetComponent<Rigidbody>();
            if(rb != null)
            {
                rb.AddForce(collision.contacts[0].normal * 10000f);
                Debug.Log(collision.contacts[0].normal);
            }
        }
    }*/
    private void Start()
    {
        attackCount = 0;
    }

    private void Update()
    {
        if (InfinitePlayer.isPowerUp)
        {
            attackForce = 300;
        }
        else
        {
            attackForce = 150f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")
        {
            Vector3 target = other.transform.position;
            Vector3 dir = target - transform.position;
            if (GameManager.instance.isVibration == true)
                Handheld.Vibrate();
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddForce(dir.normalized * attackForce);
        }
        Debug.Log("attackCount =" + attackCount);
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.tag);
        if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Boss")
        {
            Vector3 target = other.transform.position;
            Vector3 dir = target - transform.position;
            if (GameManager.instance.isVibration == true)
                Handheld.Vibrate();
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddForce(dir.normalized * attackForce);

        }
    }
}
