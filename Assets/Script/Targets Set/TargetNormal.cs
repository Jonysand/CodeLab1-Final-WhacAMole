using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetNormal : TargetBase
{
    public override void clicked(){
        EventManager.TriggerEvent("Combo");
        ComboFuntion.ComboCount ++;
        GameManager.totalScore += ComboFuntion.ComboCount;
    }
}
