using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIItemView: MonoBehaviour {
    public Text txtName;
    public Text txtInfo;
    public void SetData(ItemForEdit gd) {
        txtName.text = gd.name;
        txtInfo.text = gd.name + "的描述说明。";
    }

    public void UI_Show(ItemForEdit gd) {
        SetData(gd);
        gameObject.SetActive(true);
    }

    public void UI_Close() {
        gameObject.SetActive(false);
    }

}
