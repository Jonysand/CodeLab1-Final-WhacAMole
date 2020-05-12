using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBase : MonoBehaviour
{
    public float R;
    public AnimationCurve appearingCurve;
    private float appearingDuration = 0.5f;
    public float lifeDuration = 2f;

    public void Begin()
    {
        StartCoroutine(Appearing());
        StartCoroutine(Countdown());
    }
    public IEnumerator Appearing() // Animation of gradually appearing
    {
        float time = 0;
        while (time <= 1f)
        {
            float scale = appearingCurve.Evaluate(time) * R;
            time = time + Time.deltaTime / appearingDuration;
            transform.localScale = new Vector3(scale, scale, 1);
            yield return new WaitForFixedUpdate();
        }
    }

    // if not clicked in 1 second, destroy self
    public IEnumerator Countdown()
    {
        float timerRemain = lifeDuration + appearingDuration;
        while (timerRemain >= 0)
        {
            if (GameManager.paused || TimeFreeze.isFreeze)
            {
                yield return new WaitForFixedUpdate();
                continue;
            }
            timerRemain -= Time.deltaTime;
            yield return null;
        }
        this.remove();
    }
    public void remove()
    {
        TargetsPool.instance.LiveTargetsArray.Remove(gameObject);
        TargetsPool.instance.Push(gameObject);
    }

    public virtual void clicked() { }

    private void OnMouseDown()
    {
        if (GameManager.paused) { return; }
        // score
        if (TimeFreeze.isFreeze)
        {
            TimeFreeze.clickedWhileFreezed(this.gameObject);
        }
        else
        {
            this.clicked();
            this.remove();
        }
    }
}
