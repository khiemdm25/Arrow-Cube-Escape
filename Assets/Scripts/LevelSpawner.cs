using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    private void Start()
    {
        LevelSO level = LevelManager.Instance.GetCurrentLevel();

        // Spawn cube
        Instantiate(level.cubePrefab, Vector3.zero, Quaternion.identity);
    }
}