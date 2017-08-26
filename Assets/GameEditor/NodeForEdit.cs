using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class NodeForEdit {
    public enum MoveDir {
        N = 0,
        W = 1,
        WN = 2,
        WS = 3,
        S = 4,
        E = 5,
        ES = 6,
        EN = 7,
    }

    [NonSerialized]
    Vector2[] _dir = null;
    public Vector2[] dir {
        get {
            if(_dir == null) {
                _dir = new Vector2[] {
        new Vector2(0,1), // N
        new Vector2(-1,0),// W
        new Vector2(-1,1),// WN
        new Vector2(-1,-1),// WS
        new Vector2(0,-1),// S
        new Vector2(1,0), // E
        new Vector2(1,-1),// ES
        new Vector2(1,1), // EN
                };
            }
            return _dir;
        }
    }

    public int id;
    public string name;
    public Vector2 pos;
    public bool hasInit = false;
    public bool[] road = { false, false, false, false, false, false, false, false };

    public int transId;
    public MapForEdit transMap;
    public NodeForEdit(int id, string name) {
        this.id = id;
        this.name = name;
        hasInit = true;
    }

    public List<ItemForEdit> items = new List<ItemForEdit>();
    public List<NpcForEdit> npcs = new List<NpcForEdit>();
}
