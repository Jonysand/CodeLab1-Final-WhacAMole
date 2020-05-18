using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // update UI elements
    public GameObject ScoreObject;
    public GameObject TimerObject;
    public GameObject ComboObject;
    public static int totalScore = 0;
    public static float timerRemain = 0;
    public static bool paused = false;
    public static Color BGColor;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        paused = false;
        BGColor = Camera.main.backgroundColor;
        ScoreObject.GetComponent<TextMeshProUGUI>().text = "Score: " + totalScore;
        TimerObject.GetComponent<TextMeshProUGUI>().text = "Timer: " + timerRemain;
        ComboObject.GetComponent<TextMeshProUGUI>().text = "Combo: " + ComboFuntion.ComboCount;
        // start timer
        StartCoroutine(Countdown());
    }
    private void Update()
    {
        if (ComboObject != null)
        {
            ComboObject.GetComponent<TextMeshProUGUI>().text = "Combo: " + ComboFuntion.ComboCount;
        }
    }

    private IEnumerator Countdown()
    {
        timerRemain = 30;
        while (timerRemain >= 0)
        {
            if (paused || TimeFreeze.isFreeze)
            {
                yield return new WaitForFixedUpdate();
                continue;
            }
            ScoreObject.GetComponent<TextMeshProUGUI>().text = "Score: " + totalScore;
            TimerObject.GetComponent<TextMeshProUGUI>().text = "Timer: " + Mathf.CeilToInt(timerRemain);
            timerRemain -= Time.deltaTime;
            yield return null;
        }
        ComboFuntion.ComboEnd();
        paused = true;
        SceneManager.LoadScene("RankScene", LoadSceneMode.Additive);
    }
}
