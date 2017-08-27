using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

[ExecuteInEditMode]
public class UIColors: Graphic {
    Color old = Color.white;

    public CanvasRenderer[] renderers;
    protected override void Awake() {
        color = new Color(0, 0, 0, 0);
        //raycastTarget = false;
        //canvasRenderer.cull = true;
#if UNITY_EDITOR
        if(renderers == null || renderers.Length == 0) {
            renderers = GetComponentsInChildren<CanvasRenderer>().Where(r => r != canvasRenderer).ToArray();
        }
#endif
    }

    void Update() {
#if UNITY_EDITOR
        if(renderers == null || renderers.Length == 0) {
            return;
        }
#endif
        if(canvasRenderer.GetColor() != old) {
            old = canvasRenderer.GetColor();
            foreach(CanvasRenderer cr2 in renderers) {
                cr2.SetColor(old);
            }
        }
    }
}