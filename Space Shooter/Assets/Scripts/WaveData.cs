using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType
{
    Horizontal,
    BouncePatrol, //Cheo
    Circle,
    ZigZag,
    Default
}

[System.Serializable]
public class WaveData 
{
    public enum Pattern
    {
        Circle,
        Rectangle,
        Line,
        Asteroid
    }

    public Pattern pattern;
    public Enemy enemy;
    public Asteroid asteroid;
    public int enemyCount;
    public Transform spawnPosition;
    public MovementType movementType;
}
