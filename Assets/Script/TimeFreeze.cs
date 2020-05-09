using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeFreeze : MonoBehaviour
{
    private const float freezeDuration = 2f;
    
    public void StartFreeze(){
        GameManager.paused = true;
        StartCoroutine(FreezeCountDown());
    }

    public static IEnumerator FreezeCountDown(){
        float TimerRemain = freezeDuration;
        while(TimerRemain >= 0){
            TimerRemain -= Time.deltaTime;
            yield return null;
        }
        GameManager.paused = false;
        yield return null;
    }
}
