using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBase : MonoBehaviour
{
    public float R;
    public Vector2 position;
    public AnimationCurve appearingCurve;
    private float appearingDuration = 0.5f;
    public float lifeDuration = 2f;

    private void Start()
    {
        // animation of appearing
        StartCoroutine(Appearing());
        StartCoroutine(Countdown());
    }

    // Animation of gradually appearing
    public IEnumerator Appearing()
    {
        float time = 0;
        while (time <= 1f)
        {
            float scale = appearingCurve.Evaluate(time);
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
            if (GameManager.paused)
            {
                yield return new WaitForFixedUpdate();
                continue;
            }
            timerRemain -= Time.deltaTime;
            yield return null;
        }
        GameManager.targetsArray.Remove(gameObject);
        Destroy(gameObject);
    }

    public virtual void clicked() { }

    private void OnMouseDown()
    {
        if (GameManager.paused) { return;}
        // score
        this.clicked();
        GameManager.targetsArray.Remove(this.gameObject);
        Destroy(this.gameObject);
    }
}
