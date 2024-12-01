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
public enum Pattern
{
    Circle,
    Rectangle,
    Line,
    Asteroid,
    Planet
}

[System.Serializable]
public class WaveData 
{
    public Pattern pattern;
    public Enemy enemy;
    public Asteroid asteroid;
    public Planet[] planet;
    public int enemyCount;
    public Transform spawnPosition;
    public MovementType movementType;
}
