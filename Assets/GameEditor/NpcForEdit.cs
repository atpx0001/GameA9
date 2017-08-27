using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class NpcForEdit {
    public string name = "电脑";
    public int id;

    public NpcForEdit(int id, string name) {
        this.id = id;
        this.name = name;
    }
}
