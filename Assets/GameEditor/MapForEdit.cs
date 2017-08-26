using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
public class MapForEdit: MonoBehaviour {
    public int curId = 0;
    public string mapName;
    public List<NodeForEdit> nodes = new List<NodeForEdit>();

    public NodeForEdit FindRoad(NodeForEdit src, NodeForEdit.MoveDir dir) {
        Vector2 dpos = src.pos + src.dir[(int)dir];
        foreach(var dn in nodes) {
            if(dn.pos == dpos) {
                return dn;
            }
        }
        return null;
    }

    public NodeForEdit FindNode(int nodeId) {
        foreach(var node in nodes) {
            if(node.id == nodeId)
                return node;
        }
        return null;
    }
}
