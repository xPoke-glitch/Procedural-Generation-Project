using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellularAutomataGeneration : MonoBehaviour
{
    [SerializeField]
    private GameObject tileObj;

    [SerializeField]
    private int height;
    [SerializeField]
    private int width;

    [SerializeField]
    private int iterations = 3; // Conway GoL iterations

    [SerializeField]
    private int neighbours = 5;

    private void Start()
    {
        Generate();
    }

    private void Generate()
    {
        Vector2Int size = new Vector2Int(width, height);
        //int startingPosition = size.x * size.y / 2; // center, but you can choose

        bool[,] positions = new bool[size.x, size.y];

        // Random first pass
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                positions[x, y] = (Random.Range(0, 2) == 0);
            }
        }

        // Smoothing - Conway
        for (int i = 0; i < iterations; i++)
        {
            bool[,] newPositions = new bool[size.x, size.y];

            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    int neighbourCount = 0;
                    for (int xOffset = -1; xOffset <= 1; xOffset++)
                    {
                        for (int yOffset = -1; yOffset <= 1; yOffset++)
                        {
                            if (x + xOffset < 0 || y + yOffset < 0 || x + xOffset >= size.x || y + yOffset >= size.y)
                            {
                                neighbourCount++;
                            }
                            else if (positions[x + xOffset, y + yOffset])
                            {
                                neighbourCount++;
                            }
                        }
                    }
                    if (neighbourCount >= neighbours)
                    {
                        newPositions[x, y] = true;
                    }
                }
            }
            positions = newPositions;
        }

        // Show Tiles
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                if(!positions[x,y])
                    Instantiate(tileObj, new Vector3(x*2, 0, y*2), Quaternion.identity);
            }
        }
    }
}
