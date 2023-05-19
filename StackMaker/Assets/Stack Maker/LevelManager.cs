using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] List<GameObject> levelPrefabs;

    int currentLevel = 1;

    GameObject currentLevelInstance;

    private void Start()
    {
        currentLevelInstance = Instantiate(levelPrefabs[currentLevel - 1], Vector3.zero, Quaternion.identity);
    }

    public void NextLevel()
    {
        currentLevel++;

        if(currentLevelInstance != null)
        {
            Destroy(currentLevelInstance);
        }

        currentLevelInstance = Instantiate(levelPrefabs[currentLevel - 1], Vector3.zero, Quaternion.identity);
    }
}
