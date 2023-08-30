using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarController : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    public GameObject bar;
    public GameObject motherBall;
    public ObjectManager objectManager;
    public GameManager gameManager;
    BallController BC;
    public Text txtDmg;

    public float barSize = 6f;
    private float maxBarSize = 10f;
    public GameObject safeZone;

    Vector3 firstPos;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    private void FixedUpdate()
    {
        BC = motherBall.GetComponent<BallController>();
        bar.transform.localScale = new Vector3(barSize, 0.8f, 1);

        if (Input.GetMouseButton(0))
        {
            firstPos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + new Vector3(0, 0, 10);
            if(firstPos.x <= -44)
            {
                firstPos.x = -44;
            }else if (firstPos.x >= 45)
            {
                firstPos.x = 45;
            }
            bar.transform.position = new Vector3(firstPos.x,-70,0);
        }
    }
    private void OnCollisionEnter2D(Collision2D co)
    {
        if(co.gameObject.tag == "Items" && co.gameObject.name == "ItemDouble(Clone)")
        {
            GameObject ballClone1 = objectManager.MakeObj("ballClone");
            GameObject ballClone2= objectManager.MakeObj("ballClone");
            Rigidbody2D rigidBall1 = ballClone1.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidBall2 = ballClone2.GetComponent<Rigidbody2D>();
            SpriteRenderer spriteRenderer1 = ballClone1.GetComponent<SpriteRenderer>();
            SpriteRenderer spriteRenderer2= ballClone2.GetComponent<SpriteRenderer>();
            BallController BC1 = ballClone1.GetComponent<BallController>();
            BallController BC2 = ballClone2.GetComponent<BallController>();
            BC1.ballDmg = BC.ballDmg;
            BC2.ballDmg = BC.ballDmg;

            BC1.transform.position = motherBall.transform.position;
            BC2.transform.position = motherBall.transform.position;
            float ranDgree = Random.Range(-1f, 1f);
            rigidBall1.AddForce(new Vector3(-ranDgree, 1, 0) * BC.ballSpeed);
            rigidBall2.AddForce(new Vector3(ranDgree, 1, 0) * BC.ballSpeed);

            BC1.gameManager = this.gameManager;
            BC2.gameManager = this.gameManager;
            co.gameObject.SetActive(false);
        }else if(co.gameObject.tag == "Items" && co.gameObject.name == "ItemTriple(Clone)")
        {
            GameObject ballClone1 = objectManager.MakeObj("ballClone");
            GameObject ballClone2 = objectManager.MakeObj("ballClone");
            GameObject ballClone3 = objectManager.MakeObj("ballClone");
            Rigidbody2D rigidBall1 = ballClone1.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidBall2 = ballClone2.GetComponent<Rigidbody2D>();
            Rigidbody2D rigidBall3 = ballClone3.GetComponent<Rigidbody2D>();
            SpriteRenderer spriteRenderer1 = ballClone1.GetComponent<SpriteRenderer>();
            SpriteRenderer spriteRenderer2 = ballClone2.GetComponent<SpriteRenderer>();
            SpriteRenderer spriteRenderer3 = ballClone3.GetComponent<SpriteRenderer>();
            BallController BC1 = ballClone1.GetComponent<BallController>();
            BallController BC2 = ballClone2.GetComponent<BallController>();
            BallController BC3 = ballClone3.GetComponent<BallController>();
            BC1.ballDmg = BC.ballDmg;
            BC2.ballDmg = BC.ballDmg;
            BC3.ballDmg = BC.ballDmg;

            BC1.transform.position = motherBall.transform.position;
            BC2.transform.position = motherBall.transform.position;
            BC3.transform.position = motherBall.transform.position;
            float ranDgree = Random.Range(-1f, 1f);
            float ranDgree2 = Random.Range(-1f, 1f);
            rigidBall1.AddForce(new Vector3(-ranDgree, 1, 0) * BC.ballSpeed);
            rigidBall2.AddForce(new Vector3(ranDgree, 1, 0) * BC.ballSpeed);
            rigidBall3.AddForce(new Vector3(ranDgree2, 1, 0) * BC.ballSpeed);

            BC1.gameManager = this.gameManager;
            BC2.gameManager = this.gameManager;
            BC3.gameManager = this.gameManager;
            co.gameObject.SetActive(false);
        }
        else if (co.gameObject.tag == "Items" && co.gameObject.name == "ItemS(Clone)")
        {
            safeZone.SetActive(true);
            Invoke("DeactiveSZ",3f);
            co.gameObject.SetActive(false);
        }
        else if (co.gameObject.tag == "Items" && co.gameObject.name == "ItemP(Clone)")
        {
            BC.BallDmgUp();
            txtDmg.text = BC.ballDmg.ToString();
            co.gameObject.SetActive(false);
        }
        else if (co.gameObject.tag == "Items" && co.gameObject.name == "ItemR(Clone)")
        {
            barSize = barSize + 0.2f;
            if (barSize >= maxBarSize)
            {
                barSize = maxBarSize;
            }
            co.gameObject.SetActive(false);
        }else if (co.gameObject.tag == "Ball" || co.gameObject.tag == "BallClone")
        {
            co.rigidbody.velocity = Vector2.zero;
            float ballSpeed = BC.GetBallSpeed();
            co.rigidbody.AddForce((co.gameObject.transform.position - transform.position).normalized * ballSpeed);
        }
    }

    public void DeactiveSZ()
    {
        safeZone.SetActive(false);
    }
}
