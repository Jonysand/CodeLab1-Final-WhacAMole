using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimeFreeze : MonoBehaviour
{
    private const float freezeDuration = 2f;
    public static bool isFreeze = false;
    static float TimerRemain = 0;

    private static List<GameObject> shotsArray = new List<GameObject>();
    private UnityAction someListener;

    private void Awake()
    {
        someListener = new UnityAction(StartFreeze);
    }

    private void OnEnable()
    {
        EventManager.StartListening("Freeze", someListener);
    }

    private void OnDisable()
    {
        EventManager.StopListening("Freeze", someListener);
    }
    
    public void StartFreeze()
    {
        StartCoroutine(FreezeCountDown());
    }

    public static void clickedWhileFreezed(GameObject thisObj)
    {
        isHighNoon(thisObj);
    }

    public static IEnumerator FreezeCountDown()
    {
        TimerRemain = freezeDuration;

        // change background color when freezing
        Camera.main.backgroundColor = Color.grey;
        
        // init storing data
        shotsArray.Clear();

        isFreeze = true;
        while (TimerRemain > 0)
        {
            TimerRemain -= Time.deltaTime;
            yield return null;
        }
        isFreeze = false;
        isHighNoonEnd();
        yield return null;
    }

    static void isHighNoon(GameObject thisObj)
    {
        if (!shotsArray.Contains(thisObj)) shotsArray.Add(thisObj);
    }

    static void isHighNoonEnd()
    {
        // restore background color
        Camera.main.backgroundColor = GameManager.BGColor;

        foreach (GameObject shotTarget in shotsArray)
        {
            shotTarget.GetComponent<TargetBase>().clicked();
            GameManager.targetsArray.Remove(shotTarget);
            Destroy(shotTarget);
        }
    }
}
