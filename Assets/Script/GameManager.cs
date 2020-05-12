using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject[] targetList;
    public GameObject ScoreObject;
    public GameObject TimerObject;
    public GameObject ComboObject;

    public static int totalScore = 0;
    public static float timerRemain = 0;
    public static bool paused = false;

    public static List<GameObject> targetsArray = new List<GameObject>();
    public AnimationCurve targetsProbCurve;

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

        BGColor = Camera.main.backgroundColor;
        ScoreObject.GetComponent<TextMesh>().text = "Score: " + totalScore;
        TimerObject.GetComponent<TextMesh>().text = "Timer: " + timerRemain;
        ComboObject.GetComponent<TextMesh>().text = "Combo: " + ComboFuntion.ComboCount;
        // start timer
        StartCoroutine(Countdown());
    }
    private void Update()
    {
        if (ComboObject != null)
        {
            ComboObject.GetComponent<TextMesh>().text = "Combo: " + ComboFuntion.ComboCount;
        }
    }

    private IEnumerator Countdown()
    {
        timerRemain = 21;
        while (timerRemain >= 0)
        {
            if (paused || TimeFreeze.isFreeze)
            {
                yield return new WaitForFixedUpdate();
                continue;
            }
            ScoreObject.GetComponent<TextMesh>().text = "Score: " + totalScore;
            TimerObject.GetComponent<TextMesh>().text = "Timer: " + (int)timerRemain;
            timerRemain -= Time.deltaTime;
            yield return null;
        }
        ComboFuntion.ComboEnd();
        SceneManager.LoadScene("EndScene", LoadSceneMode.Single);
    }
}
