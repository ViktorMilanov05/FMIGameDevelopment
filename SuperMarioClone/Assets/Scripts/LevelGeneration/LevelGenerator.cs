using System;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField]
    private LevelChunk firstChunk;
    [SerializeField]
    private LevelChunk[] middleChunks;
    [SerializeField] 
    private LevelChunk endChunk;
    [SerializeField]
    private int middleChunkCount = 3;

    private void Awake()
    {
        Generate();
    }

    private void Generate()
    {
        Vector3 nextSpawn = transform.position;
        LevelChunk start = Instantiate(firstChunk, nextSpawn, Quaternion.identity);
        Console.WriteLine($"Start generated at {nextSpawn}");
        nextSpawn = start.EndPoint;

        int lastIndex = -1;
        for (int i = 0; i < middleChunkCount; i++)
        {
            System.Random random = new System.Random();
            int index = random.Next(0, middleChunks.Length);
            if(middleChunks.Length > 1 && index == lastIndex)
            {
                index = (index + 1) % middleChunks.Length;
            }
            lastIndex = index;

            LevelChunk chunk = Instantiate(middleChunks[index], nextSpawn, Quaternion.identity);
            Console.WriteLine($"Chunk {i + 1} generated at {nextSpawn}");
            nextSpawn = chunk.EndPoint;
        }

        Instantiate(endChunk, nextSpawn, Quaternion.identity);
        Console.WriteLine($"Chunk end generated at {nextSpawn}");
    }
}