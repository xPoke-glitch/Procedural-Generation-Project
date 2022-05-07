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

    private List<Vector3> _visited = new List<Vector3>();
    private Vector3 _currentPosition;

    private void Start()
    {
        _currentPosition = startPosition;
        StartCoroutine(Generate(delay));
    }

    private IEnumerator Generate(float delay=0.5f)
    {
        do
        {
            // Add current position to the visited list
            if (!_visited.Contains(_currentPosition))
            {
                _visited.Add(_currentPosition);
                // Instantiate tile in current position
                Instantiate(tilePrefab,_currentPosition,Quaternion.identity);

                yield return new WaitForSeconds(delay);
            }
            PickNewPosition();
        }
        while(_visited.Count < totalTiles);
    }
    
    private void PickNewPosition()
    {
        int dir = Random.Range(0, 4);
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
