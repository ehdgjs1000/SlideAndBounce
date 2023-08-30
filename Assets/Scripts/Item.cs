using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public GameObject bar;
    public GameManager gameManager;
    public ObjectManager objectManager;

    private void Awake()
    {

    }
    void OnCollisionEnter2D(Collision2D co)
    {
        {
            DeActive();
        }
    }

    void DeActive()
    {
        gameObject.SetActive(false);
    }

}
