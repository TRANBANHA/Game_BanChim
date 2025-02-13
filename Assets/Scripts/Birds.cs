using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Birds : MonoBehaviour
{
    public float xSpeed;
    public float minYspeed;
    public float maxYspeed;

    Rigidbody2D m_rb;
    bool m_moveLeftOnStart;

    bool m_isDie;

    public GameObject deathVft;


    private void Awake()
    {
        m_rb = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
       RandomMovingDirection();
    }
    private void Update()
    {

        m_rb.velocity = m_moveLeftOnStart?
             new Vector2(-xSpeed, Random.Range(minYspeed, maxYspeed)) : new Vector2(xSpeed, Random.Range(minYspeed, maxYspeed));
        Flip();
    }
    public void RandomMovingDirection()
    {
        m_moveLeftOnStart=transform.position.x >0 ?true:false;
    }
    void Flip()
    {
        if (m_moveLeftOnStart)
        {
            if (transform.localScale.x < 0) return;
            transform.localScale = new Vector3(transform.localScale.x *-1 , transform.localScale.y,transform.localScale.z);
        }
        else
        {
            if (transform.localScale.x > 0) return;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
    }

    public void Die()
    {
        m_isDie = true;
        Destroy(gameObject);
        GameManager.Ins.BirdKill++;

        if (deathVft)
        {
            Instantiate(deathVft, transform.position, Quaternion.identity);
        }
        GameGUIManager.Ins.UpdateKilledCounting(GameManager.Ins.BirdKill);
    }
}
