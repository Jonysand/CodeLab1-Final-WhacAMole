using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPoison : TargetBase
{
    public override void clicked(){
        if(--GameManager.totalScore < 0){
            GameManager.totalScore = 0;
        }
        ComboFuntion.ComboEnd();
    }
}
