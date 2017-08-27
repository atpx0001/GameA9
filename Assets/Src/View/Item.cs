﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item: MonoBehaviour {
    public ItemForEdit gd;
    public void SetData(ItemForEdit gd) {
        this.gd = gd;
        if(gd != null)
            transform.GetComponentInChildren<Text>().text = gd.name;
    }
}
