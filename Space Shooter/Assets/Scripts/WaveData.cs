using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovementType
{
    Horizontal,
    BouncePatrol,
    ZigZag,
    Default
}
public enum Pattern
{
    Circle,
    Rectangle,
    Line,
    Asteroid,
    Planet,
    Boss,
    BigBoss
}

[System.Serializable]
public class WaveData 
{
    [Header("Enemy type")]
    public Enemy enemy;
    public Asteroid asteroid;
    public BigBoss bigBoss;
    public Planet[] planet;
    public Boss[] boss;

    [Space]
    public Pattern pattern;
    public int enemyCount;
    public Transform spawnPosition;
    public MovementType movementType;
}
