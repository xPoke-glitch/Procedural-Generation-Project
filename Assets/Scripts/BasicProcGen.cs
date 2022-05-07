using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProcGen : MonoBehaviour
{
    [SerializeField]
    private Vector3 startPosition;
    [SerializeField]
    private float delay;
    [SerializeField]
    private int totalTiles = 10;
    [SerializeField]
    private GameObject tilePrefab;
    [SerializeField]
    private GameObject waterTilePrefab;

    private List<Vector3> _visited = new List<Vector3>();
    private Vector3 _currentPosition;

    private float _maxZ = 0;
    private float _minZ = 0;
    private float _maxX = 0;
    private float _minX = 0;

    private void Start()
    {
        _currentPosition = startPosition;
        StartCoroutine(COGenerate(GenerateWater, delay));
    }

    private void GenerateWater()
    {
        StartCoroutine(COGenerateWater(delay));
    }

    private IEnumerator COGenerateWater(float delay=0.5f)
    {
        for(float x = _minX; x<=_maxX; x+=2)
        {
            for(float z=_minZ; z<=_maxZ; z += 2)
            {
                if (Physics.CheckSphere(new Vector3(x - 2, 0, z), 0.01f) && Physics.CheckSphere(new Vector3(x + 2, 0, z), 0.01f) &&
                    Physics.CheckSphere(new Vector3(x, 0, z-2), 0.01f) && Physics.CheckSphere(new Vector3(x, 0, z+2), 0.01f))
                {
                    // Fill 1 box
                    Instantiate(tilePrefab, new Vector3(x, 0, z), Quaternion.identity);
                }
                if (!Physics.CheckSphere(new Vector3(x, 0, z), 0.01f))
                {
                    Instantiate(waterTilePrefab, new Vector3(x, 0, z), Quaternion.identity);
                }
                yield return new WaitForSeconds(delay);
            }
        }
    }

    private IEnumerator COGenerate(Action OnGenerationFinished, float delay=0.5f)
    {
        do
        {
            // Add current position to the visited list
            if (!_visited.Contains(_currentPosition))
            {
                _visited.Add(_currentPosition);
                // Instantiate tile in current position
                Instantiate(tilePrefab,_currentPosition,Quaternion.identity);

                // check min and max
                if (_currentPosition.z > _maxZ) _maxZ = _currentPosition.z;
                if (_currentPosition.z < _minZ) _minZ = _currentPosition.z;
                if (_currentPosition.x > _maxX) _maxX = _currentPosition.x;
                if (_currentPosition.x < _minX) _minX = _currentPosition.x;

                yield return new WaitForSeconds(delay);
            }
            PickNewPosition();
        }
        while(_visited.Count < totalTiles);
        OnGenerationFinished?.Invoke();
    }
    
    private void PickNewPosition()
    {
        int dir = UnityEngine.Random.Range(0, 4);
        switch (dir)
        {
            case 0: _currentPosition.z += 2;
                break;
            case 1: _currentPosition.z -= 2;
                break;
            case 2: _currentPosition.x += 2;
                break;
            case 3: _currentPosition.x -= 2;
                break;
        }
    }

}
