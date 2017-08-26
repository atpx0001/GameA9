using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item: MonoBehaviour {
    ItemForEdit gd;
    public void SetData(UnitForEdit gd) {
        ItemForEdit ie = gd as ItemForEdit;
        if(ie != null) SetData(ie);
        NpcForEdit ne = gd as NpcForEdit;
        if(ne != null) SetData(ne);
    }
    public void SetData(ItemForEdit gd) {
        if(gd != null)
            transform.GetComponentInChildren<Text>().text = gd.name;
    }
    public void SetData(NpcForEdit gd) {
        if(gd != null)
            transform.GetComponentInChildren<Text>().text = gd.name;
    }
}
