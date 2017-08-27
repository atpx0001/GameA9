using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map: MonoBehaviour {
    public Node nodeSeed;
    public Npc npcSeed;
    public Item itemSeed;
    public MapForEdit gd;
    Dictionary<NodeForEdit, Node> nodeDict = new Dictionary<NodeForEdit, Node>();
    Node curNode;
    public Transform itemTable;
    public List<Npc> npcs;
    public List<Item> items;
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

        foreach(var v in items) v.gameObject.SetActive(false);
        foreach(var v in npcs) v.gameObject.SetActive(false);
        itemSeed.gameObject.SetActive(false);
        npcSeed.gameObject.SetActive(false);
        int curIdx = 0;
        foreach(var v in cur.gd.items) {
            Item dval = null;
            if(curIdx >= items.Count) {
                dval = Instantiate(itemSeed, itemTable);
                items.Add(dval);
            } else {
                dval = items[curIdx];
            }
            dval.SetData(v);
            dval.gameObject.SetActive(true);
            curIdx++;
        }
        curIdx = 0;
        foreach(var v in cur.gd.npcs) {
            Npc dval = null;
            if(curIdx >= npcs.Count) {
                dval = Instantiate(npcSeed, itemTable);
                npcs.Add(dval);
            } else {
                dval = npcs[curIdx];
            }
            dval.SetData(v);
            dval.gameObject.SetActive(true);
            curIdx++;
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
