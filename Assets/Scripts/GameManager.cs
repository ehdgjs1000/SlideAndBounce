using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int score = 1;
    private float spawnTerm = 3f;
    bool isBlockMoving = false;

    public GameObject bar;
    public ObjectManager objectManager;
    public Text finalScore;
    public Text HighScore;
    public Text txtScore;

    public GameObject ItemDouble;
    public GameObject ItemP;
    public GameObject ItemR;
    public GameObject ItemS;
    public GameObject ItemTriple;
    public GameObject Block;

    public Transform BlockGroup;
    public GameObject GameOverSet;
    public GameObject newScore;
    public GameObject PauseScene;
    public Button pauseBtn;

    public AudioClip audioClip;
    AudioSource audioSource;


    private void Awake()
    {
        SpawnBlocks();
        HighScore.text = "HighScore : "+PlayerPrefs.GetInt("SaveScore").ToString();
        audioSource = gameObject.GetComponent<AudioSource>();

    }
    public void SpawnBlocks()
    {
        int blockSpawnCount;
        int blockRndValue = Random.Range(1,101);
        if(blockRndValue <= 15)
        {
            blockSpawnCount = 1;
        }
        else if (blockRndValue >15 && blockRndValue<=35)
        {
            blockSpawnCount = 2;
        }
        else if (blockRndValue > 35 && blockRndValue <= 65)
        {
            blockSpawnCount = 3;
        }
        else if (blockRndValue > 65 && blockRndValue <= 85)
        {
            blockSpawnCount = 4;
        }
        else
        {
            blockSpawnCount = 5;
        }

        List<Vector3> spawnList = new List<Vector3>();
        for (int i = 0; i < 6; i++) spawnList.Add(new Vector3(-45f + i * 18f, 62f, 0));
        for(int i = 0; i < blockSpawnCount; i++)
        {
            int rand = Random.Range(0, spawnList.Count);

            Transform TR = Instantiate(Block, spawnList[rand], Quaternion.identity).transform;
            TR.SetParent(BlockGroup);
            TR.GetChild(0).GetComponentInChildren<Text>().text = score.ToString();

            spawnList.RemoveAt(rand);
        }
        isBlockMoving = true;
        for (int i = 0; i < BlockGroup.childCount; i++) StartCoroutine(BlockMoveDown(BlockGroup.GetChild(i)));
        txtScore.text = "Score : " + score.ToString();
        score++;
        spawnTerm = spawnTerm + score/20;
        Invoke("SpawnBlocks", spawnTerm);
    }

    IEnumerator BlockMoveDown(Transform TR)
    {
        yield return new WaitForSeconds(0.2f);
        Vector3 targetPos = TR.position + new Vector3(0, -13.5f, 0);

        float TT = 1.5f;
        while (true)
        {
            yield return null; TT -= Time.deltaTime * 1.5f;
            if(TR.position != null)
            {
                TR.position = Vector3.MoveTowards(TR.position, targetPos + new Vector3(0, -6, 0), TT);
                if (TR.position == targetPos + new Vector3(0, -6, 0)) break;
            }
        }
        TT = 0.9f;
        while (true)
        {
            yield return null; TT -= Time.deltaTime;
            TR.position = Vector3.MoveTowards(TR.position,targetPos, TT);
            if (TR.position == targetPos) break;
        }
        isBlockMoving = false;
        if(targetPos.y < -62)
        {
            GameOver();
        }
    }

    public void SpawnItem(Vector3 pos)
    {
        int ranItemValue = Random.Range(1,101);
        if(ranItemValue <= 30)
        {
            
        }
        else if (ranItemValue >30 && ranItemValue <= 50)
        {
            
            SpawnItem(pos, "itemDouble");
        }
        else if (ranItemValue > 50 && ranItemValue <= 65)
        {
            SpawnItem(pos, "itemTriple");
        }
        else if (ranItemValue > 65 && ranItemValue <= 76)
        {
            SpawnItem(pos, "itemP");
        }
        else if (ranItemValue > 76 && ranItemValue <= 85)
        {
            SpawnItem(pos, "itemS");
        }
        else
        {
            SpawnItem(pos, "itemR");
        }
    }

    private void SpawnItem(Vector3 pos, string item)
    {
        GameObject spawnItem = objectManager.MakeObj(item);
        spawnItem.transform.position = pos;

        Rigidbody2D rigidItem = spawnItem.GetComponent<Rigidbody2D>();
        Item itemLogic = spawnItem.GetComponent<Item>();
        itemLogic.bar = this.bar;
        itemLogic.gameManager = this;
        itemLogic.objectManager = objectManager;
        rigidItem.AddForce(new Vector3(0, -1, 0) * 3000);
    }
    public void GameOver()
    {
        score--;
        int saveScore = PlayerPrefs.GetInt("SaveScore");
        if (saveScore < score)
        {
            PlayerPrefs.SetInt("SaveScore", score);
            HighScore.text = "HighScore : " + score.ToString();
            newScore.SetActive(true);
        }
        finalScore.text = score.ToString();

        //objectManager.DestroyObj();
        pauseBtn.interactable = false;
        score = 0;
        Time.timeScale = 0;
        GameOverSet.SetActive(true);
    }
    public void PlayAudio()
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }
    public void RetryGame()
    {
        Time.timeScale = 1;
        pauseBtn.interactable = true;
        SceneManager.LoadScene(1);
    }
    public void PauseGame()
    {
        Time.timeScale = 0;
        PauseScene.SetActive(true);
    }
    public void RestartGame()
    {
        Time.timeScale = 1;
        PauseScene.SetActive(false);
    }
    
}
