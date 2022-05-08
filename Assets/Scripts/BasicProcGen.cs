using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicProcGen : MonoBehaviour
{
    public List<Vector3> Visited { get => _visited; }
    public Vector2 Min { get { return new Vector2(_minX, _minZ); } }
    public Vector2 Max { get { return new Vector2(_maxX, _maxZ); } }

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
    private List<GameObject> _genTiles = new List<GameObject>();

    private float _maxZ = 0;
    private float _minZ = 0;
    private float _maxX = 0;
    private float _minX = 0;

    private bool _isGenerating = false;

    public void GenerateMap(Action OnFinishCallback, float delay = 0.1f, bool instatiateObj = true)
    {
        RemoveAllTiles();

        if (_isGenerating)
            return;
        _isGenerating = true;
        StartCoroutine(COGenerate(OnFinishCallback, delay, instatiateObj));
    }

    public void FillHoles(Action OnFinishCallback, float delay = 0.1f)
    {
        if (_isGenerating)
            return;
        _isGenerating = true;

        StartCoroutine(COFillAllHoles(OnFinishCallback, delay));
    }

    public void GenerateMapWithWater()
    {
        RemoveAllTiles();

        if (_isGenerating)
            return;
        _isGenerating = true;
        StartCoroutine(COGenerate(GenerateWater, delay));
    }

    public void RemoveAllTiles()
    {
        if (_genTiles.Count == 0)
            return;
        foreach (GameObject obj in _genTiles)
        {
            Destroy(obj);
        }
        _visited.Clear();
        _genTiles.Clear();

        _maxX = 0;
        _minX = 0;
        _maxZ = 0;
        _maxX = 0;
    }

    private void Start()
    {
        _currentPosition = startPosition;
    }

    private void GenerateWater()
    {
        StartCoroutine(COGenerateWater(delay));
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

    private IEnumerator COGenerateWater(float delay=0.5f)
    {
        for(float x = _minX; x<=_maxX; x+=2)
        {
            for(float z=_minZ; z<=_maxZ; z += 2)
            {
                if (Physics.CheckSphere(new Vector3(x - 2, 0, z), 0.01f) && Physics.CheckSphere(new Vector3(x + 2, 0, z), 0.01f) &&
                   Physics.CheckSphere(new Vector3(x, 0, z - 2), 0.01f) && Physics.CheckSphere(new Vector3(x, 0, z + 2), 0.01f))
                {
                    // Fill 1 box
                    GameObject obj = Instantiate(tilePrefab, new Vector3(x, 0, z), Quaternion.identity);
                    _genTiles.Add(obj);
                }
                if (!Physics.CheckSphere(new Vector3(x, 0, z), 0.01f))
                {
                    GameObject obj = Instantiate(waterTilePrefab, new Vector3(x, 0, z), Quaternion.identity);
                    _genTiles.Add(obj);
                }
                yield return new WaitForSeconds(delay);
            }
        }
        _isGenerating = false;
    }

    private IEnumerator COFillAllHoles(Action OnFinishCallback, float delay = 0.5f)
    {
        for (float x = _minX; x <= _maxX; x += 2)
        {
            for (float z = _minZ; z <= _maxZ; z += 2)
            {
                if (Physics.CheckSphere(new Vector3(x - 2, 0, z), 0.01f) && Physics.CheckSphere(new Vector3(x + 2, 0, z), 0.01f) &&
                    Physics.CheckSphere(new Vector3(x, 0, z - 2), 0.01f) && Physics.CheckSphere(new Vector3(x, 0, z + 2), 0.01f))
                {
                    // Fill 1 box
                    GameObject obj = Instantiate(tilePrefab, new Vector3(x, 0, z), Quaternion.identity);
                    _genTiles.Add(obj);
                }
                yield return new WaitForSeconds(delay);
            }
        }
        OnFinishCallback?.Invoke();
        _isGenerating = false;
    }

    private IEnumerator COGenerate(Action OnFinishCallback, float delay=0.5f, bool insantiateObj = true)
    {
        do
        {
            // Add current position to the visited list
            if (!_visited.Contains(_currentPosition))
            {
                _visited.Add(_currentPosition);

                if (insantiateObj)
                {
                    // Instantiate tile in current position
                    GameObject obj = Instantiate(tilePrefab, _currentPosition, Quaternion.identity);
                    _genTiles.Add(obj);
                }

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
        OnFinishCallback?.Invoke();
        _isGenerating = false;
    }
}
