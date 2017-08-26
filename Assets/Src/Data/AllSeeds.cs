using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllSeeds {
    public static int _nodeId = 0;
    public static int GetNextNodeID() {
        int id = _nodeId;
        _nodeId++;
        return id;
    }
    public static Dictionary<int, Node> nodes;

    public static void Load() {

    }

    public static void Save() {

    }
}
