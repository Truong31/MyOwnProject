using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WaveData
{
    public enum Pattern
    {
        Circle,
        Rectangle,
        ZigZag,
        Line
    }

    public Pattern pattern;
    public GameObject enemy;
    public Transform appearPosition;
    public int enemyCount;
}
