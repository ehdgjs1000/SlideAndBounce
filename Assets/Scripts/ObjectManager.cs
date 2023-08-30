using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    public GameObject itemDoublePrefab;
    public GameObject itemPPrefab;
    public GameObject itemRPrefab;
    public GameObject itemSPrefab;
    public GameObject itemTriplePrefab;
    public GameObject ballClonePrefab;

    GameObject[] itemDouble;
    GameObject[] itemP;
    GameObject[] itemR;
    GameObject[] itemS;
    GameObject[] itemTriple;
    GameObject[] ballClone;

    GameObject[] targetPool;


    void Awake()
    {
        itemDouble = new GameObject[50];
        itemP = new GameObject[50];
        itemR = new GameObject[50];
        itemS = new GameObject[50];
        itemTriple = new GameObject[50];
        ballClone = new GameObject[1000];

        Genarate();
    }
    void Genarate()
    {
        for (int index = 0; index < itemDouble.Length; index++)
        {
            itemDouble[index] = Instantiate(itemDoublePrefab);
            itemDouble[index].SetActive(false);
        }
        for (int index = 0; index < itemP.Length; index++)
        {
            itemP[index] = Instantiate(itemPPrefab);
            itemP[index].SetActive(false);
        }
        for (int index = 0; index < itemR.Length; index++)
        {
            itemR[index] = Instantiate(itemRPrefab);
            itemR[index].SetActive(false);
        }
        for (int index = 0; index < itemS.Length; index++)
        {
            itemS[index] = Instantiate(itemSPrefab);
            itemS[index].SetActive(false);
        }
        for (int index = 0; index < itemTriple.Length; index++)
        {
            itemTriple[index] = Instantiate(itemTriplePrefab);
            itemTriple[index].SetActive(false);
        }
        for (int index = 0; index < ballClone.Length; index++)
        {
            ballClone[index] = Instantiate(ballClonePrefab);
            ballClone[index].SetActive(false);
        }
    }
    public GameObject MakeObj(string type)
    {
        switch (type)
        {
            case "itemDouble":
                targetPool = itemDouble;
                break;
            case "itemP":
                targetPool = itemP;
                break;
            case "itemR":
                targetPool = itemR;
                break;
            case "itemS":
                targetPool = itemS;
                break;
            case "itemTriple":
                targetPool = itemTriple;
                break;
            case "ballClone":
                targetPool = ballClone;
                break;
        }

        for (int index = 0; index < targetPool.Length; index++)
        {
            if (!targetPool[index].activeSelf)
            {
                targetPool[index].SetActive(true);
                return targetPool[index];
            }
        }
        return null;
    }
    public GameObject DestroyObj()
    {
        for (int index = 0; index < targetPool.Length; index++)
        {
            targetPool[index].SetActive(false);
        }
        return null;
    }
}
