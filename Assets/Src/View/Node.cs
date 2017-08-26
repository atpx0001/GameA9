using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Node: MonoBehaviour {
    public NodeForEdit gd;
    public Text txtNode;
    public Transform lineRoot;
    public int moveDir;
    public bool fadeOut = false;
    public CanvasGroup cg;
    public void SetData(NodeForEdit node) {
        gd = node;
        txtNode.text = gd.name;
        HideRoad();
    }

    public Vector3 GetTargetPos(int did, bool passLocalPos = false) {
        Vector2 curDir = gd.dir[did];
        curDir.x *= 1.5f;
        Vector3 pos = curDir * 50;
        if(!passLocalPos)
            pos += transform.localPosition;
        return pos;
    }

    public void ShowRoad() {
        for(int i = 0; i < gd.road.Length; i++) {
            if(gd.road[i]) {
                RectTransform trs = lineRoot.GetChild(i).GetComponent<RectTransform>();
                lineRoot.GetChild(i).gameObject.SetActive(true);
                Vector3 p2 = GetTargetPos(i, true);
                trs.localPosition = p2 * .5f;
                float dis = Vector3.Distance(Vector3.zero, p2);
                trs.sizeDelta = new Vector2(dis, 3);

                Vector2 dir = gd.dir[i];
                float arc = Vector3.Angle(p2, Vector3.left);
                if(dir.y > 0) arc = -arc;
                trs.rotation = Quaternion.Euler(0, 0, arc);
            }
        }
    }

    public void HideRoad() {
        for(int i = 0; i < lineRoot.childCount; i++) {
            RectTransform trs = lineRoot.GetChild(i).GetComponent<RectTransform>();
            trs.gameObject.SetActive(false);
        }
    }

    public Action<Node> onClick;
    public void OnClick() {
        if(onClick != null)
            onClick(this);
    }
}
