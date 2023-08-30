using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public Text highScoreTxt;
    private void Awake()
    {
        int saveScore = PlayerPrefs.GetInt("SaveScore");
        highScoreTxt.text = saveScore.ToString();
    }
    public void StartGame()
    {
        Debug.Log("Btn");
        SceneManager.LoadScene(1);
    }
} 
