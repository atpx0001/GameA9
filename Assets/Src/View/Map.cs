using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map: MonoBehaviour {
    public Node nodeSeed;
    public MapForEdit gd;
    Dictionary<NodeForEdit, Node> nodeDict = new Dictionary<NodeForEdit, Node>();
    Node curNode;
    public Transform itemTable;
    private void Start() {
        curNode = CreateNode(gd.nodes[0], Vector3.zero, 0);
        nodeSeed.gameObject.SetActive(false);
        curNode.transform.localPosition = Vector3.zero;

        FlushView(curNode);
    }

    void FlushView(Node cur) {
        cur.gameObject.SetActive(true);
        cur.ShowRoad();
        cur.fadeOut = false;
        cur.transform.SetSiblingIndex(1);
        for(int i = 0; i < 8; i++) {
            if(curNode.gd.road[i]) {
                NodeForEdit node = gd.FindRoad(curNode.gd, (NodeForEdit.MoveDir)i);
                if(node != null) {
                    CreateNode(node, curNode.GetTargetPos(i), i);
                }
            }
        }

        List<UnitForEdit> objs = new List<UnitForEdit>();
        foreach(var v in cur.gd.items)
            objs.Add(v);
        foreach(var v in cur.gd.npcs)
            objs.Add(v);
        Item iseed = itemTable.GetChild(0).GetComponent<Item>();
        iseed.gameObject.SetActive(false);
        for(int i = 0; i < objs.Count; i++) {
            Item it = null;
            if(i < itemTable.childCount) {
                it = itemTable.GetChild(i).GetComponent<Item>();
            } else {
                it = Instantiate(iseed, iseed.transform.parent);
            }
            it.gameObject.SetActive(true);
            it.SetData(objs[i]);
        }
        for(int i = objs.Count; i < itemTable.childCount; i++) {
            itemTable.GetChild(i).gameObject.SetActive(false);
        }
    }


    Node CreateNode(NodeForEdit node, Vector3 pos, int did) {
        Node obj = null;
        nodeDict.TryGetValue(node, out obj);
        if(obj == null) {
            obj = Instantiate(nodeSeed, transform);

            obj.name = node.name + "_" + node.id;
            nodeDict[node] = obj;
        }
        obj.SetData(node);
        obj.moveDir = did;
        obj.transform.localPosition = pos;
        obj.gameObject.SetActive(true);
        obj.onClick = MoveToNode;
        obj.fadeOut = false;
        if(obj.cg.alpha != 1)
            obj.cg.alphaTo(0.1f, 1);
        return obj;
    }

    public void MoveToNode(Node node) {
        if(curNode == node) return;
        curNode.HideRoad();
        curNode = node;
        if(curNode.gd.transMap != null) {
            int nextNodeId = curNode.gd.transId;
            gd = curNode.gd.transMap;

            curNode.SetData(gd.FindNode(nextNodeId));
            //Node newNode = CreateNode(gd.FindNode(nextNodeId), Vector3.zero, 0);
            //newNode.transform.localPosition = curNode.transform.localPosition;
        }
        foreach(var old in nodeDict.Values) {
            old.fadeOut = true;
        }
        FlushView(node);
        foreach(var obj in nodeDict.Values) {
            if(obj.fadeOut) {
                obj.cg.alphaTo(0.1f, 0).setOnCompleteHandler((agt) => {
                    obj.gameObject.SetActive(false);
                });
            }
            Vector3 moveDest = node.gd.dir[(node.moveDir + 4) % 8] * 50;
            moveDest.x *= 1.5f;
            moveDest += obj.transform.localPosition;
            obj.transform.localPositionTo(0.1f, moveDest);
        }
    }
}
