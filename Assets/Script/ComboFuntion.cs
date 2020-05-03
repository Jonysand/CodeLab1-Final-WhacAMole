using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboFuntion : MonoBehaviour
{
    public static bool ComboStart = false;
    public static int ComboCount = 0;
    public static IEnumerator ComboTimer(){
        Debug.Log("Combo Start");
        float comboTimerRemain = 1f;
        ComboStart = true;
        while(comboTimerRemain >= 0 && ComboStart){
            comboTimerRemain -= Time.deltaTime;
            yield return null;
        }
        ComboEnd();
        yield return null;
    }

    public static void ComboEnd(){
        Debug.Log("Combo End");
        ComboStart = false;
        ComboCount = 1;
    }
}
