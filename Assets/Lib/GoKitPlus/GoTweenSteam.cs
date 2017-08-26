using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GoTweenSteam {
    public bool autoDestroyOnComplete = true;
    public Action<AbstractGoTween> onComplete = null;
    public Action<AbstractGoTween> onUpdate = null;

    public List<GoTween> gtList = new List<GoTween>();
    Action<AbstractGoTween> curComplete = null;
    Action<AbstractGoTween> curUpdate = null;


    int curIdx = 0;
    public void Append(GoTween gt) {
        gtList.Add(gt);
    }

    public void Play() {
        curIdx = -1;
        PlayNext();
    }

    void PlayNext() {
        curIdx++;
        GoTween gt = gtList[curIdx];
        if(onUpdate != null) {
            curUpdate = gt._onUpdate;
            gt._onUpdate = OnUpdate;
        }

        curComplete = gt._onComplete;
        gt._onComplete = OnComplete;
        Go.addTween(gt);
        gt.play();
    }
    void OnUpdate(AbstractGoTween agt) {
        if(curUpdate != null)
            curUpdate(agt);
        if(onUpdate != null)
            onUpdate(agt);
    }

    void OnComplete(AbstractGoTween agt) {
        if(curComplete != null) {
            curComplete(agt);
            if(onUpdate != null)
                agt._onUpdate = curUpdate;
            agt._onComplete = curComplete;
        }
        if(curIdx >= gtList.Count - 1) {
            if(onComplete != null) {
                onComplete(agt);
                if(autoDestroyOnComplete)
                    Destroy();
            }
        } else {
            PlayNext();
        }
    }

    public void AppendDelay(float time) {
        GoTween gt = new GoTween(this, time, new GoTweenConfig());
        Append(gt);
    }

    public void Destroy() {
        for(int i = 0; i < gtList.Count; i++) {
            gtList[i].destroy();
        }
        gtList.Clear();
        onComplete = null;
        onUpdate = null;
        curUpdate = null;
        curComplete = null;
    }
}
