using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public enum ItemTypes {
    新物品,
    电脑,
    你的电脑,
}
[Serializable]
public class ItemForEdit {
    public ItemTypes type = ItemTypes.电脑;
    public string name = "电脑";
    public int id;

    public ItemForEdit(int id, ItemTypes type) {
        this.id = id;
        this.type = type;
        this.name = type.ToString();
    }
}
