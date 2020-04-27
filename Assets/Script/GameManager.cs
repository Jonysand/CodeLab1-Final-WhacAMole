using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject target;
    public GameObject ScoreObject;
    public GameObject TimerObject;

    public static int totalScore = 0;
    public static float timerRemain = 0;

    public static List<GameObject> targetsArray = new List<GameObject>();


    private void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start() {
        ScoreObject.GetComponent<TextMesh>().text = "Score: "+totalScore;
        TimerObject.GetComponent<TextMesh>().text = "Timer: "+timerRemain;
        // start timer
        StartCoroutine(Countdown());
    }

    private IEnumerator Countdown(){
        timerRemain = 11;
        float interval = 1;
        float cumulateInter = 0;
        while(timerRemain >= 0){
            ScoreObject.GetComponent<TextMesh>().text = "Score: "+totalScore;
            TimerObject.GetComponent<TextMesh>().text = "Timer: "+(int)timerRemain;
            timerRemain -= Time.deltaTime;
            cumulateInter += Time.deltaTime;
            if(cumulateInter >= interval){
                generateTargets();
                cumulateInter = 0;
            }
            yield return null;
        }
        SceneManager.LoadScene("EndScene", LoadSceneMode.Single);
    }

    void generateTargets(){
        // get an available position and R
        Vector2 newPos;
        float newR;
        bool conflicted = false;
        int TrialCount = 5; // after trying multiple times, give up generating
        while(true){
            newPos = new Vector2(Random.Range(-7f,7f), Random.Range(-4f,3.5f));
            newR = Random.Range(1f,2f);
            conflicted = false;
            foreach (GameObject oldTarget in targetsArray){
                // if conflict exists
                if(Vector2.Distance(newPos, oldTarget.transform.position) <= (newR + oldTarget.transform.localScale.x)/2){
                    conflicted = true;
                    break;
                }
            }
            if(!conflicted) break;
            if(--TrialCount <= 0) return;
        }
        // initiate new target
        target.transform.localScale = new Vector3(newR, newR);
        target.transform.position = new Vector3(newPos.x, newPos.y, 0);
        targetsArray.Add(target);
        Instantiate(target, target.transform.position, Quaternion.identity);
    }

    public void debugClick(){
        Debug.Log("Clicked");
    }
}
