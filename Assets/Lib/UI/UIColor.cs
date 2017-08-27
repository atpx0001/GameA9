using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[ExecuteInEditMode]
public class UIColor: Graphic {
    Color old = Color.white;
    protected override void Awake() {
        color = new Color(0, 0, 0, 0);
        //raycastTarget = false;
        //canvasRenderer.cull = true;
    }

    protected override void OnEnable() {
        base.OnEnable();
        old = Color.white;
    }

    void Update() {
        if(canvasRenderer.GetColor() != old) {
            old = canvasRenderer.GetColor();
            var crs = GetComponentsInChildren<CanvasRenderer>(true);
            for(int i = crs.Length;--i>=0;) {
                var cr2 = crs[i];
                if(cr2 == canvasRenderer) continue;
                cr2.SetColor(old);
            }
        }
    }
}