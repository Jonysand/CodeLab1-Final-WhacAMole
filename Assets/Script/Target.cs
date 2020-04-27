using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target:MonoBehaviour{
    public float R;
    public Vector2 position;

    private void Start() {
        StartCoroutine(Countdown());
    }

    // if not clicked in 1 second, destroy self
    private IEnumerator Countdown(){
        float timerRemain = 2;
        while(timerRemain >= 0){
            timerRemain -= Time.deltaTime;
            yield return null;
        }
        GameManager.targetsArray.Remove(gameObject);
        Destroy(gameObject);
    }
}
