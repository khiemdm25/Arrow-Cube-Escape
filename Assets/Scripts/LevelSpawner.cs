using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    private void Start()
    {
        LevelSO level = LevelManager.Instance.GetCurrentLevel();
        Instantiate(level.cubePrefab, Vector3.zero, Quaternion.identity);
    }
}