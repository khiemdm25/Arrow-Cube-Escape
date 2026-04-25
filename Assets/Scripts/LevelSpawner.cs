using UnityEngine;

public class LevelSpawner : MonoBehaviour
{
    private void Start()
    {
        LevelSO level = LevelManager.Instance.GetCurrentLevel();

        if (level == null)
        {
            Debug.LogError("Level NULL !!!");
            return;
        }

        Debug.Log("Spawn Level: " + LevelManager.Instance.currentLevelIndex);

        GameObject cube = Instantiate(level.cubePrefab, Vector3.zero, Quaternion.identity);

        Zoom zoom = Camera.main.GetComponent<Zoom>();
        if (zoom != null)
        {
            zoom.SetTarget(cube.transform);
        }
    }
}