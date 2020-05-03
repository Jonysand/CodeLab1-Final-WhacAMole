using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFunction : MonoBehaviour
{
    public void gamePause(){
        GameManager.paused = !GameManager.paused;
    }
}
