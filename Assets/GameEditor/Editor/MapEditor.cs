using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(MapForEdit))]
public class MapEditor: Editor {
    public override void OnInspectorGUI() {
        base.DrawDefaultInspector();
        if(pickedNode != null) {
            OnNodeUI(pickedNode);
        }
    }
    MapForEdit map { get { return target as MapForEdit; } }

    void OnNodeUI(NodeForEdit node) {
        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button("-", GUILayout.Width(20))) {
            if(EditorUtility.DisplayDialog("提示", "确定要删除节点马?", "是", "否")) {
                map.nodes.Remove(node);
            }
        }
        GUILayout.Label("名称:", GUILayout.Width(60));
        node.name = EditorGUILayout.TextField(node.name);
        EditorGUILayout.EndHorizontal();
        UnityEngine.Object obj = node.transMap;
        obj = EditorGUILayout.ObjectField(obj, typeof(MapForEdit));
        node.transMap = (MapForEdit)obj;
        if(node.transMap != null) {

            List<string> list = new List<string>(node.transMap.nodes.Count);
            int selIdx = 0;
            for(int i = 0; i < node.transMap.nodes.Count; i++) {
                NodeForEdit dnd = node.transMap.nodes[i];
                list.Add(dnd.name + "_" + dnd.id);
                if(dnd.id == node.transId)
                    selIdx = i;
            }
            selIdx = EditorGUILayout.Popup(selIdx, list.ToArray());
            if(selIdx >= 0)
                node.transId = node.transMap.nodes[selIdx].id;
        }
        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button(GetNodeString(map.FindRoad(node, NodeForEdit.MoveDir.WN)), GUILayout.Width(60), GUILayout.Height(60)))
            LinkNode(node, NodeForEdit.MoveDir.WN);
        if(GUILayout.Button(GetNodeString(map.FindRoad(node, NodeForEdit.MoveDir.N)), GUILayout.Width(60), GUILayout.Height(60)))
            LinkNode(node, NodeForEdit.MoveDir.N);
        if(GUILayout.Button(GetNodeString(map.FindRoad(node, NodeForEdit.MoveDir.EN)), GUILayout.Width(60), GUILayout.Height(60)))
            LinkNode(node, NodeForEdit.MoveDir.EN);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button(GetNodeString(map.FindRoad(node, NodeForEdit.MoveDir.W)), GUILayout.Width(60), GUILayout.Height(60)))
            LinkNode(node, NodeForEdit.MoveDir.W);
        GUILayout.Label(GetNodeString(node), f1, GUILayout.Width(60), GUILayout.Height(60));
        if(GUILayout.Button(GetNodeString(map.FindRoad(node, NodeForEdit.MoveDir.E)), GUILayout.Width(60), GUILayout.Height(60)))
            LinkNode(node, NodeForEdit.MoveDir.E);
        EditorGUILayout.EndHorizontal();

        EditorGUILayout.BeginHorizontal();
        if(GUILayout.Button(GetNodeString(map.FindRoad(node, NodeForEdit.MoveDir.WS)), GUILayout.Width(60), GUILayout.Height(60)))
            LinkNode(node, NodeForEdit.MoveDir.WS);
        if(GUILayout.Button(GetNodeString(map.FindRoad(node, NodeForEdit.MoveDir.S)), GUILayout.Width(60), GUILayout.Height(60)))
            LinkNode(node, NodeForEdit.MoveDir.S);
        if(GUILayout.Button(GetNodeString(map.FindRoad(node, NodeForEdit.MoveDir.ES)), GUILayout.Width(60), GUILayout.Height(60)))
            LinkNode(node, NodeForEdit.MoveDir.ES);
        EditorGUILayout.EndHorizontal();

        GUILayout.Label("这里有:");
        for(int i = node.items.Count - 1; i >= 0; i--) {
            if(node.items[i] == null) {
                node.items.RemoveAt(i);
                continue;
            }
            EditorGUILayout.BeginHorizontal();
            if(GUILayout.Button("-", GUILayout.Width(20))) {
                if(EditorUtility.DisplayDialog("提示", "要删除物品吗?", "是", "否")) {
                    node.items.RemoveAt(i);
                    continue;
                }
            }
            node.items[i].name = EditorGUILayout.TextField(node.items[i].name);
            GUILayout.Label("[" + node.items[i].id + "]");
            EditorGUILayout.EndHorizontal();
        }

        ItemTypes selItem = (ItemTypes)EditorGUILayout.EnumPopup(ItemTypes.新物品);
        if(selItem != ItemTypes.新物品) {
            ItemForEdit item = new ItemForEdit(GetNewId(), selItem);
            node.items.Add(item);
        }
        for(int i = node.npcs.Count - 1; i >= 0; i--) {
            EditorGUILayout.BeginHorizontal();
            if(GUILayout.Button("-", GUILayout.Width(20))) {
                if(EditorUtility.DisplayDialog("提示", "要删NPC吗?", "是", "否")) {
                    node.npcs.RemoveAt(i);
                }
            }
            node.npcs[i].name = EditorGUILayout.TextField(node.npcs[i].name);
            GUILayout.Label("[" + node.npcs[i].id + "]");
            EditorGUILayout.EndHorizontal();
        }
        if(GUILayout.Button("新NPC")) {
            NpcForEdit npc = new NpcForEdit(GetNewId(), "NewNpc");
            node.npcs.Add(npc);
        }
    }

    void LinkNode(NodeForEdit src, NodeForEdit.MoveDir dir) {
        NodeForEdit dest = map.FindRoad(src, dir);
        int did = (int)dir;
        if(dest != null) {
            src.road[did] = !src.road[did];
        } else {
            dest = AddNewNode(src.name);
            dest.pos = src.pos + src.dir[did];
            src.road[did] = true;
            dest.road[(did + 4) % 8] = true;
        }
        SceneView.RepaintAll();
    }

    string GetNodeString(NodeForEdit node) {
        if(node == null) return "New";
        return node.name + "\n" + node.id;
    }

    NodeForEdit pickedNode = null;
    GUIStyle _f0 = null;
    GUIStyle _f1 = null;
    GUIStyle f0 {
        get {
            if(_f0 == null) {
                _f0 = new GUIStyle();
                _f0.fontSize = 20;
                _f0.alignment = TextAnchor.UpperLeft;
                _f0.normal.textColor = Color.white;
            }
            return _f0;
        }
    }
    GUIStyle f1 {
        get {
            if(_f1 == null) {
                if(_f1 == null) {
                    _f1 = new GUIStyle(GUI.skin.button);
                    _f1.normal.background = null;
                }
            }
            return _f1;
        }
    }

    Vector3 Map2World(Vector2 pos, float size) {
        Vector3 dpos = new Vector3(pos.x, pos.y, 0) * size * 3 + map.transform.position;
        return dpos;
    }

    int GetNewId() {
        SerializedProperty sp = serializedObject.FindProperty("curId");
        int id = map.curId;
        sp.intValue = id + 1;
        serializedObject.ApplyModifiedProperties();
        return id;
    }

    NodeForEdit AddNewNode(string nodeName) {
        NodeForEdit node = new NodeForEdit(GetNewId(), nodeName);
        map.nodes.Add(node);
        pickedNode = node;
        return node;
    }

    private void OnSceneGUI() {
        if(map.nodes == null) map.nodes = new List<NodeForEdit>();
        if(map.nodes.Count == 0) AddNewNode("NewNode");
        float size = 5;// HandleUtility.GetHandleSize(Vector3.zero);
        float fSize = HandleUtility.GetHandleSize(Vector3.zero);
        f0.fontSize = (int)(20 * size / fSize);
        f0.fontSize = Mathf.Clamp(f0.fontSize, 5, 100);
        for(int i = 0; i < map.nodes.Count; i++) {
            if(map.nodes[i] == null || !map.nodes[i].hasInit) continue;
            NodeForEdit node = map.nodes[i];
            Vector3 dpos = Map2World(node.pos, size);

            if(pickedNode != node) {
                f0.normal.textColor = Color.white;
            } else {
                f0.normal.textColor = Color.black;
                Handles.CubeCap(0, dpos, Quaternion.identity, size * 2);
            }

            Vector3 lPos = dpos + Vector3.left * size * .9f + Vector3.up * size * .9f;
            Handles.Label(lPos, node.name + "[" + node.id + "]", f0);
            if(Handles.Button(dpos, Quaternion.identity, size, size, Handles.RectangleCap) && pickedNode != node) {
                pickedNode = node;
                Repaint();
            }
            foreach(var val in node.npcs) {
                lPos += Vector3.down * f0.fontSize * .02f * size;
                Handles.Label(lPos, val.name + "[" + val.id + "]", f0);
            }
            foreach(var val in node.items) {
                lPos += Vector3.down * f0.fontSize * .02f * size;
                Handles.Label(lPos, val.name + "[" + val.id + "]", f0);
            }

            if(node == pickedNode) Handles.color = Color.cyan;
            foreach(var e in Enum.GetValues(typeof(NodeForEdit.MoveDir))) {
                NodeForEdit.MoveDir key = (NodeForEdit.MoveDir)e;
                int did = (int)key;
                if(node.road[did]) {
                    NodeForEdit dest = map.FindRoad(node, key);
                    if(dest == null) {
                        node.road[did] = false;
                        SceneView.RepaintAll();
                        continue;
                    }
                    if(dest == pickedNode) Handles.color = Color.yellow;
                    Vector3 p1 = Map2World(node.pos, size);
                    Vector3 p2 = Map2World(dest.pos, size);
                    Vector3 dp = (p2 - p1).normalized * size;
                    p1 += dp;
                    p2 -= dp;
                    if(p1.x != p2.x && p1.y != p2.y) {
                        p1 += dp * .5f;
                        p2 -= dp * .5f;
                    }
                    float fx = p1.y < p2.y ? -1 : 1;
                    fx = p1.y == p2.y ? 0 : fx;
                    float fy = p1.x > p2.x ? -1 : 1;
                    fy = p1.x == p2.x ? 0 : fy;
                    Vector3 fix = new Vector3(fx, fy, 0) * size * .05f;
                    if(p1.x != p2.x && p1.y != p2.y) {
                        fix *= 0.7f;
                    }
                    p1 += fix;
                    p2 += fix;
                    Handles.DrawLine(p1, p2);

                    // 画箭头部分
                    Vector3 dir = (p2 - p1).normalized;
                    dir = Quaternion.Euler(0, 0, 90) * dir;
                    p1 += dir * size;
                    dir = (p2 - p1).normalized * size * .2f;
                    p1 = p2 - dir;
                    Handles.DrawLine(p1, p2);
                }
            }
            Handles.color = Color.white;
        }

    }
}
