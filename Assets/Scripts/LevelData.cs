using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="LevelData", fileName ="New level")]
public class LevelData : ScriptableObject
{
    public LevelSO level;

    public List<LevelSO> alllevel = new List<LevelSO>();
}