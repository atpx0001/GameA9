using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;

[ExecuteInEditMode]
public class AdjustCamera: MonoBehaviour {
    private int oldW;
    private int oldH;

    void Awake() {
        Update();
    }

    void Update() {
        if(Screen.width != oldW || Screen.height != oldH) {
            Camera c = GetComponent<Camera>();
            if(c != null) {
                c.orthographicSize = Screen.height / 2f;
            }
            oldW = Screen.width;
            oldH = Screen.height;
        }
    }
}