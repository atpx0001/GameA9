using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class AdjustCanvas: MonoBehaviour {
    private int oldW;
    private int oldH;

    void Awake()
    {
        Update();
    }

    void Update()
    {
        if (Screen.width != oldW || Screen.height != oldH)
        {
            Canvas c = GetComponent<Canvas>();
            if (c != null)
            {
                if (c.renderMode == RenderMode.WorldSpace)
                {
                    RectTransform rt = c.GetComponent<RectTransform>();
                    rt.sizeDelta = new Vector2(Screen.width, Screen.height);
                }
                else
                {
                    CanvasScaler cs = GetComponent<CanvasScaler>();
                    if (cs != null)
                    {
                        cs.uiScaleMode = UnityEngine.UI.CanvasScaler.ScaleMode.ScaleWithScreenSize;
                        cs.referenceResolution = new Vector2(Screen.width, Screen.height);
                    }
                }
            }
            oldW = Screen.width;
            oldH = Screen.height;
        }
    }
}