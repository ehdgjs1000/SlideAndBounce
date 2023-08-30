using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{
    public GameObject ball;
    public Rigidbody2D rigid;
    public SpriteRenderer spriteRenderer;
    public GameManager gameManager;

    private float groundY = -65;
    public float ballSpeed = 500;
    public int ballDmg = 1;

    bool isStart = false;
    private float ranStartX,ranStartY;
    private Vector2 startSpeed;

    private void FixedUpdate()
    {
        if(this.rigid.velocity.y <= 10)
        {
            //rigid.AddForce(new Vector2(0f,1f).normalized * ballSpeed);
        }
    }

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        ball = GetComponent<GameObject>();

        ranStartX = Random.Range(0.1f, 0.5f);
        ranStartY = Random.Range(0.1f, 0.9f);
        int startDir = Random.Range(1, 3);
        if(startDir == 2)
        {
            ranStartX = -ranStartX;
        }
        startSpeed = new Vector2(ranStartX, ranStartY).normalized * ballSpeed;
        rigid.AddForce(startSpeed);

    }
    private void OnCollisionEnter2D(Collision2D co)
    {
        if (co.gameObject.tag == "Block")
        {
            Text BlockText = co.transform.GetChild(0).GetComponentInChildren<Text>();
            int blockValue = int.Parse(BlockText.text) - ballDmg;
            gameManager.PlayAudio();

            if (blockValue > 0)
            {
                BlockText.text = blockValue.ToString();
                
            }
            else
            {
                if(co.transform.position != null)
                {
                    gameManager.SpawnItem(co.transform.position);
                }
                Destroy(co.gameObject);
            } 
        }else if(co.gameObject.tag == "Ground"){
            if(this.tag == "Ball")
            {
                gameManager.GameOver();
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }

        Vector2 pos = rigid.velocity.normalized;
        if(pos.magnitude != 0 && pos.y < 0.2f && pos.y > -0.2f)
        {
            rigid.velocity = Vector2.zero;
            rigid.AddForce(new Vector2(pos.x > 0 ? 1 : -1, -0.2f).normalized *ballSpeed);
        }
    }
    public void BallDmgUp()
    {
        ballDmg++;
    }
    public float GetBallSpeed()
    {
        return ballSpeed;
    }
}
