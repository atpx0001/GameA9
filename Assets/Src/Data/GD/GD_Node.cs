using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GD_Node: GameData {
    public SD_Node seed;
    public string name { get { return seed.name; } }

}
