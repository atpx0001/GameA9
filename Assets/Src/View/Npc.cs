using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Npc: MonoBehaviour {
    NpcForEdit gd;
    public void SetData(NpcForEdit gd) {
        this.gd = gd;
        if(gd != null)
            transform.GetComponentInChildren<Text>().text = gd.name;
    }
}
