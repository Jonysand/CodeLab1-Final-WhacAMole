using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class EndGame : MonoBehaviour
{
    public GameObject ScoreObject;
    // Update is called once per frame
    void Start()
    {
        ScoreObject.GetComponent<TextMeshProUGUI>().text = "Your Score is " + GameManager.totalScore;
    }
}
