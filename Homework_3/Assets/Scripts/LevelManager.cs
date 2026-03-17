using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private GameObject movingPlatformPrefab;
    [SerializeField] private GameObject fallingPlatformPrefab;
    [SerializeField] private GameObject carrotPrefab;
    [SerializeField] private GameObject powerUpPrefab;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject trampolinePrefab;

    [SerializeField] private int numberOfPlatforms = 10;

    [SerializeField] private float carrotOffsetY = 10f;
    [SerializeField] private float powerUpOffsetY = 10f;
    [SerializeField] private float trampolineChance = 0.15f;
    [SerializeField] private float enemyChance = 0.2f;
    [SerializeField] private float powerUpChance = 0.1f;

    private bool previousPlatformHadTrampoline = false;
    private int trampolineDirection = 0;


    private int direction;
    private Transform spawnPoint;
    List<Transform> availableNormalPlatforms = new List<Transform>();

    private void Start()
    {
        GenerateProcedurialLevel();
    }

    private GameObject ChoosePlatform()
    {
        float rnd = Random.value;

        if (rnd < 0.1f)
            return fallingPlatformPrefab;
        else if (rnd < 0.2f)
            return movingPlatformPrefab;
        else
            return platformPrefab;
    }

    public void GenerateProcedurialLevel()
    {
        previousPlatformHadTrampoline = false;
        trampolineDirection = 0;
        availableNormalPlatforms.Clear();

        int fixSide = 0;
        bool fixNext = false;
        bool waitForPlatform3 = false;

        GameObject startPlatform = Instantiate(platformPrefab, new Vector3(0, -2f, 0), Quaternion.identity);
        Transform spawnPos = startPlatform.transform;

        for (int i = 0; i < numberOfPlatforms - 2; i++)
        {
            int directionToUse;
            Transform spawnPointToUse;

            if (waitForPlatform3)
            {
                directionToUse = fixSide;
                spawnPointToUse = directionToUse == -1 ? spawnPos.Find("LeftSpawn") : spawnPos.Find("RightSpawn");
                if (spawnPointToUse == null)
                    spawnPointToUse = spawnPos;
                waitForPlatform3 = false;
            }
            else if (fixNext)
            {
                directionToUse = Random.value < 0.5f ? -1 : 1;
                spawnPointToUse = directionToUse == -1 ? spawnPos.Find("LeftSpawn") : spawnPos.Find("RightSpawn");
                if (spawnPointToUse == null)
                    spawnPointToUse = spawnPos;
                fixSide = directionToUse;
                waitForPlatform3 = true;
                fixNext = false;
            }
            else if (previousPlatformHadTrampoline)
            {
                directionToUse = trampolineDirection;
                spawnPointToUse = directionToUse == -1 ? spawnPos.Find("LeftHighSpawn") : spawnPos.Find("RightHighSpawn");
                if (spawnPointToUse == null)
                    spawnPointToUse = spawnPos;
                previousPlatformHadTrampoline = false;
            }
            else
            {
                directionToUse = Random.value < 0.5f ? -1 : 1;
                spawnPointToUse = directionToUse == -1 ? spawnPos.Find("LeftSpawn") : spawnPos.Find("RightSpawn");
            }

            Vector3 newPos = spawnPointToUse.position;
            GameObject newPlatformPrefab = ChoosePlatform();
            GameObject newPlatform = Instantiate(newPlatformPrefab, newPos, Quaternion.identity);

            bool isNormalPlatform = newPlatformPrefab == platformPrefab;

            if (isNormalPlatform)
            {
                availableNormalPlatforms.Add(newPlatform.transform);

                bool enemySpawned = false;
                if (Random.value < enemyChance)
                {
                    Transform enemySpawn = directionToUse == -1 ? newPlatform.transform.Find("LeftItem") : newPlatform.transform.Find("RightItem");
                    Instantiate(enemyPrefab, enemySpawn.position, Quaternion.identity);
                    enemySpawned = true;
                }

                if (!enemySpawned && Random.value < powerUpChance)
                {
                    Instantiate(powerUpPrefab, newPos + Vector3.up * powerUpOffsetY, Quaternion.identity);
                    availableNormalPlatforms.RemoveAt(availableNormalPlatforms.Count - 1);
                }

                if (!enemySpawned && Random.value < trampolineChance)
                {
                    int trampDir = Random.value < 0.5f ? -1 : 1;
                    Transform trampSpawn = trampDir == -1 ? newPlatform.transform.Find("LeftItem") : newPlatform.transform.Find("RightItem");
                    Instantiate(trampolinePrefab, trampSpawn.position, Quaternion.identity);

                    previousPlatformHadTrampoline = true;
                    trampolineDirection = trampDir;
                }
            }

            if (newPlatformPrefab == movingPlatformPrefab)
            {
                fixNext = true;
            }

            spawnPos = newPlatform.transform;
        }

        int finalDirection = Random.value < 0.5f ? -1 : 1;
        Transform finalSpawn = finalDirection == -1 ? spawnPos.Find("LeftSpawn") : spawnPos.Find("RightSpawn");
        GameObject finalPlatform = Instantiate(platformPrefab, finalSpawn.position, Quaternion.identity);
        Instantiate(carrotPrefab, finalSpawn.position + Vector3.up * carrotOffsetY, Quaternion.identity);

        int carrotsPlaced = 0;
        while (carrotsPlaced < 2 && availableNormalPlatforms.Count > 0)
        {
            int index = Random.Range(0, availableNormalPlatforms.Count);
            Transform plat = availableNormalPlatforms[index];
            Instantiate(carrotPrefab, plat.position + Vector3.up * carrotOffsetY, Quaternion.identity);
            availableNormalPlatforms.RemoveAt(index);
            carrotsPlaced++;
        }
    }
}