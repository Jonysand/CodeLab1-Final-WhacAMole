using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitInteraction : MonoBehaviour
{
    private void OnMouseDown() {
        GameManager.totalScore ++;
        GameManager.targetsArray.Remove(this.gameObject);
        Destroy(this.gameObject);
    }
}
