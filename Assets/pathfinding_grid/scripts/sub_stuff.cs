using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class sub_tile
{
    public bool blocked;
    public tile tile_s;
    public int num;
}


public enum efind_path { once_per_turn, max_tiles, on_click, on_hover }