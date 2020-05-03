using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetNormal : TargetBase
{
    public override void clicked(){
        StopCoroutine(ComboFuntion.ComboTimer());
        StartCoroutine(ComboFuntion.ComboTimer());
        ComboFuntion.ComboCount ++;
        GameManager.totalScore += ComboFuntion.ComboCount;
    }
}
