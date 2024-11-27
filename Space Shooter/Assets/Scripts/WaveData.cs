using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType
{
    Horizontal,
    Diagonal, //Cheo
    Circle,
    ZigZag
}

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
    public Enemy enemy;
    public int enemyCount;
    public Transform spawnPosition;
    public MovementType movementType;
}
