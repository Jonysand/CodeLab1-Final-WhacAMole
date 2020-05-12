using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetsPool : ObjectPool
{
    public static TargetsPool instance; //holds singleton reference
    public GameObject target; // holder for the target prefab, normal as default
    ShuffleBag<GameObject> TargetsBag; // shuffle bag for randomly generating targets
    public List<GameObject> TargetsArray; // storing all the prefabs
    public List<GameObject> LiveTargetsArray = new List<GameObject>(); // storing all the active targets, for avoiding position conflict
    public List<int> TargetsCount; // storing the number of each prefabs
    public float intervalMin = 0.5f; //min time to span new target
    public float intervalMax = 1f; //max time to span new target

    private void Start()
    {
        //set up the singleton
        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        // add prefabs to the TargetsBag
        TargetsBag = new ShuffleBag<GameObject>();
        for (int i = 0; i < TargetsArray.Count; ++i)
        {
            for (int j = 0; j < TargetsCount[i]; ++j)
            {
                TargetsBag.Add(TargetsArray[i]);
            }
        }

        Invoke("SpawnTarget", intervalMin);
    }

    protected override GameObject GetNewObject()
    {
        return Instantiate<GameObject>(target);
    }

    void SpawnTarget()
    {
        if (!GameManager.paused && !TimeFreeze.isFreeze) generateTargets();
        Invoke("SpawnTarget", Random.Range(intervalMin, intervalMax));
    }

    void generateTargets()
    {
        // get an available position and R
        Vector2 newPos;
        float newR;
        bool conflicted = false;
        int TrialCount = 5; // after trying multiple times, give up generating
        while (true)
        {
            newPos = new Vector2(Random.Range(-7f, 7f), Random.Range(-4f, 3.5f));
            newR = Random.Range(1f, 1.5f);
            conflicted = false;
            foreach (GameObject oldTarget in LiveTargetsArray)
            {
                // if conflict exists
                if (Vector2.Distance(newPos, oldTarget.transform.position) <= (newR + oldTarget.transform.localScale.x) / 2)
                {
                    conflicted = true;
                    break;
                }
            }
            if (!conflicted) break;
            if (--TrialCount <= 0) return;
        }
        target = Get(); // get one target out of object pool
        GameObject targetPrefab = TargetsBag.Next(); // assign the type of new targets
        target.RemoveComponent(target.GetComponent<TargetBase>().GetType(), true);
        target.CopyComponent(targetPrefab.GetComponent<TargetBase>());
        target.GetComponent<SpriteRenderer>().color = targetPrefab.GetComponent<SpriteRenderer>().color;
        target.GetComponent<TargetBase>().R = newR;
        target.transform.position = newPos;
        LiveTargetsArray.Add(target);
        target.GetComponent<TargetBase>().Begin();
    }
}
