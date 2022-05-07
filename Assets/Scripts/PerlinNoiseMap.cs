using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinNoiseMap : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> tiles;

    [Header("Settings")]
    [SerializeField]
    private int mapWidth = 16;
    [SerializeField]
    private int mapHeight = 9;
    [SerializeField]
    private float magnification = 7.0f;
    [SerializeField]
    private int xOffset = 0;
    [SerializeField]
    private int yOffset = 0;

    private List<List<int>> _noiseGrid = new List<List<int>>();
    private List<List<GameObject>> _tileGrid = new List<List<GameObject>>();

    void Start()
    {
        StartCoroutine(COGenerate());
    }

    private IEnumerator COGenerate()
    {
        for(int x=0; x<mapWidth; x++)
        {
            _noiseGrid.Add(new List<int>());
            _tileGrid.Add(new List<GameObject>());

            for(int z=0; z<mapHeight; z++)
            {
                int index = GetIndexFromPerlin(x, z);
                _noiseGrid[x].Add(index);
                // Instantiate
                Instantiate(tiles[index], new Vector3(x*2, 0, z*2), Quaternion.identity);
            }
        }

        yield return null;
    }

    private int GetIndexFromPerlin(int x, int y)
    {
        float rawPerlin = Mathf.PerlinNoise(
            (x - xOffset) / magnification,
            (y - yOffset) / magnification
        );
        float clampPerlin = Mathf.Clamp(rawPerlin, 0.0f, 1.0f);
        float scaledPerlin = clampPerlin * tiles.Count;
        if(scaledPerlin == tiles.Count)
        {
            scaledPerlin = tiles.Count - 1;
        }
        return Mathf.FloorToInt(scaledPerlin);
    }
}
