using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMineable
{
    public void MineResource();
    public bool IsNear { get; set; }
}
