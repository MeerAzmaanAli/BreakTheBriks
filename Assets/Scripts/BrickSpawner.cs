using System;
using System.Linq;
using UnityEngine;
using System.Collections.Generic;

public class BrickSpawner : MonoBehaviour
{
    public GameObject weakBrickPrefab;
    public GameObject midBrickPrefab;
    public GameObject strongBrickPrefab;
    public GameObject explosiveBrickPrefab;
    int rows;  // Number of rows of bricks
    int columns ;  // Number of columns of bricks
    public float brickWidth = 1.5f;  // Width of each brick
    public float brickHeight = 0.5f;  // Height of each brick
    public float padding = 0.1f;  // Space between bricks

    public Vector2 offset = new Vector2(0, 0); // Adjustable offset

    public List< GameObject> spawnedBricks;

    public int brickCount;

    public int explosiveBrickCount;
    public int maxExplosiveBricks;

    public void SpawnBricks(int r, int c)
    {
        rows = r;
        columns = c;
        
        // Calculate the position offset to center the grid
        float startX = -(columns * (brickWidth + padding)) / 2 + brickWidth / 2 + offset.x;
        float startY = (rows * (brickHeight + padding)) / 2 - brickHeight / 2 + offset.y;
        brickCount = 0;


        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                // Calculate the position for each brick
                Vector3 position = new Vector3(startX + col * (brickWidth + padding), startY - row * (brickHeight + padding), 0);

                GameObject brickPrefab = GetRandomBrickPrefab();
                GameObject brickInstance = Instantiate(brickPrefab, position, Quaternion.identity);
                spawnedBricks.Add(brickInstance);

            }
        }
        brickCount = spawnedBricks.Count;
    }

    // Rchoose between weak, mid, and strong bricks
    GameObject GetRandomBrickPrefab()
    {
        int brickType = UnityEngine.Random.Range(0, 4); // 0 = weak, 1 = mid, 2 = strong, 3 = explosive

        if (brickType == 3 && explosiveBrickCount >= maxExplosiveBricks)
        {
            brickType = UnityEngine.Random.Range(0, 3); // Force it to be a non-explosive brick
        }

        switch (brickType)
        {
            case 0: return weakBrickPrefab;
            case 1: return midBrickPrefab;
            case 2: return strongBrickPrefab;
            case 3:
                explosiveBrickCount++;
                return explosiveBrickPrefab;
            default: return weakBrickPrefab;
        }
    }
}
