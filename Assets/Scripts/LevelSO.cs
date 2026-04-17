using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Level/LevelSO")]
public class LevelSO : ScriptableObject
{
    public int gridSize = 5;

    public List<ArrowData> arrows = new List<ArrowData>();
    public List<ParData> parData = new List<ParData>();
}

[System.Serializable]
public class ArrowData
{
    public GameObject arrowPrefab;
    public Vector3[] positionsOriginal;
    public Vector3Int gridPosition;
    public ArrowType arrowType;
}

public enum ArrowType
{
    Up,
    Down,
    Left,
    Right
}

[System.Serializable]
public class ParData
{
    public Rating rating;
    public int strokes;
}

public enum Rating
{
    Perfect,
    Good,
    Ok
}