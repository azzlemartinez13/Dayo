using System;

[Serializable]
public struct Wave
{
    public Spider[] EnemyPrefabs;

    public int[] EnemyAmounts;

    public float[] SpawnIntervals;

    public float RoundInterval;
}
