using System;
using System.Collections;
using TMPro;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class RankManager : MonoBehaviour
{
    const string FILE_SCORE_RANK = "/score_rank.txt";
    public GameObject ScoreRankPanel;

    private void Start()
    {
        ScoreRankPanel.GetComponent<Image>().CrossFadeColor(Color.white, 1.0f, false, true);
        if (GameManager.totalScore == 0)
        {
            loadData(false);
        }
        else
        {
            loadData(true, GameManager.totalScore);
        }
    }

    void loadData(bool isNewScore, int score = 0)
    {
        List<int> scoreRank = new List<int>();
        string hsString = ""; // high score list as a string
        if (File.Exists(Application.dataPath + FILE_SCORE_RANK))
        {
            hsString = File.ReadAllText(Application.dataPath + FILE_SCORE_RANK);
        }
        else
        {
            hsString = "0,0,0,0,0,0,0,0,0,";
        }
        string[] splitString = hsString.TrimEnd(',').Split(',');
        scoreRank = new List<int>(Array.ConvertAll(splitString, int.Parse));
        string scoretext = ""; // to render rank list
        bool highScoreInterted = false; // check if new score
        string allScoreString = ""; // to write score file
        for (int i = 0; i < scoreRank.Count; i++)
        {
            // update rank list
            if (score > scoreRank[i] && !highScoreInterted && isNewScore)
            {
                for (int j = scoreRank.Count - 1; j > i; j--)
                {
                    scoreRank[j] = scoreRank[j - 1];
                }
                scoreRank[i] = score;
                highScoreInterted = true;
                scoretext += "* ";
            }
            scoretext += ("(" + (i + 1) + ")" + ". " + '\t' + scoreRank[i] + '\n');
            allScoreString = allScoreString + scoreRank[i] + ",";
        }
        ScoreRankPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = scoretext;
        File.WriteAllText(Application.dataPath + FILE_SCORE_RANK, allScoreString);
    }

    public void loadNextScene()
    {
        if (GameManager.totalScore == 0)
        {
            SceneManager.UnloadSceneAsync("RankScene");
        }
        else
        {
            SceneManager.LoadScene("EndScene", LoadSceneMode.Single);
        }
    }
}
