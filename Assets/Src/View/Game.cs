using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game: MonoBehaviour {
    public Map map;
    public UIItemView itemView;
    private void Start() {
        itemView.gameObject.SetActive(false);
    }
    public void UI_ItemOnClicked(Item sender) {
        itemView.UI_Show(sender.gd);
    }

    public void UI_NpcOnClicked(Npc sender) {

    }
}
