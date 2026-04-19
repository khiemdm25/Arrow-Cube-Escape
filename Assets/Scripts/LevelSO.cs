using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(menuName = "Level/LevelSO")]
public class LevelSO : ScriptableObject
{
    public int gridSize = 5;

    public GameObject cubePrefab;
    public ParData parData;
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