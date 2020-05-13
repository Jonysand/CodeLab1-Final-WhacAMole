using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFunction : MonoBehaviour
{
    public void StartGame(){
        GameManager.totalScore = 0;
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void ReturnHome(){
        GameManager.totalScore = 0;
        SceneManager.LoadScene("StartScene", LoadSceneMode.Single);
    }

    public void showRank(){
        SceneManager.LoadScene("RankScene", LoadSceneMode.Additive);
    }
}
