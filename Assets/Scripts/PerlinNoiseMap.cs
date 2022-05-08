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
    [SerializeField]
    private float delay;

    [Header("Mixed Generation")]
    [SerializeField]
    private BasicProcGen basicProcGen;

    private bool _isGenerating = false;
    private List<GameObject> _genTiles;

    public void GenerateMapUsingBasicProcGen()
    {
        RemoveAllTiles();

        if(_isGenerating)
            return;
        _isGenerating = true;

        basicProcGen.GenerateMap(() => {

            StartCoroutine(COGenerateOnBasic());

        }, 0.0f, false);
    }

    public void GenerateMapWithPerlin()
    {
        RemoveAllTiles();

        if (_isGenerating)
            return;
        _isGenerating = true;
        StartCoroutine(COGenerate());
    }

    public void RemoveAllTiles()
    {
        if (_genTiles.Count == 0)
            return;
        foreach(GameObject obj in _genTiles)
        {
            Destroy(obj);
        }
        _genTiles.Clear();
    }

    private void Awake()
    {
        _genTiles = new List<GameObject>();
    }

    private IEnumerator COGenerateOnBasic()
    {
        for(float x=basicProcGen.Min.x; x<basicProcGen.Max.x; x++)
        {
            for (float z=basicProcGen.Min.y; z<basicProcGen.Max.y; z++)
            {
                if (basicProcGen.Visited.Contains(new Vector3(x*2, 0, z*2)))
                {
                    int index = GetIndexFromPerlin((int)x, (int)z);
                    // Instantiate
                    GameObject obj = Instantiate(tiles[index], new Vector3(x * 2, 0, z * 2), Quaternion.identity);
                    _genTiles.Add(obj);
                    yield return new WaitForSeconds(delay);
                }
            }
        }
        _isGenerating = false;
        yield return null;
    }

    private IEnumerator COGenerate()
    {
        for(int x=0; x<mapWidth; x++)
        {
            for(int z=0; z<mapHeight; z++)
            {
                int index = GetIndexFromPerlin(x, z);
                // Instantiate
                GameObject obj = Instantiate(tiles[index], new Vector3(x*2, 0, z*2), Quaternion.identity);
                _genTiles.Add(obj);
            }
        }
        _isGenerating = false;
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
