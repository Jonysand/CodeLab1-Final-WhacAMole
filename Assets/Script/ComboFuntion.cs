using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ComboFuntion : MonoBehaviour
{
    public static int ComboCount = 0;
    public static float comboTimerRemain = 1f;

    private UnityAction someListener;

    private void Awake()
    {
        someListener = new UnityAction(ComboStart);
    }

    private void OnEnable()
    {
        EventManager.StartListening("Combo", someListener);
    }

    private void OnDisable()
    {
        EventManager.StopListening("Combo", someListener);
    }

    public void ComboStart()
    {
        // StopCoroutine(ComboTimer());
        StartCoroutine(ComboTimer());
    }

    public static IEnumerator ComboTimer()
    {
        comboTimerRemain = 5;
        while (comboTimerRemain >= 0)
        {
            comboTimerRemain -= Time.deltaTime;
            yield return null;
        }
        ComboEnd();
        yield return null;
    }

    public static void ComboEnd()
    {
        comboTimerRemain = 0;
        ComboCount = 0;
    }
}
