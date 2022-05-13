using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOfLife : MonoBehaviour
{
    [SerializeField]
    private int height, width;

    private Cell[,] _cells;
    private bool[,] _cellBuffer;

    private bool _isPlaying = false;

    private float _lastTick;
    private float _tickDuration = 0.5f;

    public void TogglePlay()
    {
        _isPlaying = !_isPlaying;
    }

    private void Start()
    {
        _lastTick = Time.time;
        _cells = new Cell[width, height];
        _cellBuffer = new bool[width, height];
        
        GenerateMap();

        Camera.main.transform.position = new Vector3(width / 2f, height / 2f, -10);
        Camera.main.orthographicSize = height / 2f;

        _lastTick = Time.time;
    }

    private void Update()
    {
        if (!_isPlaying)
            return;

        if(Time.time > _lastTick + _tickDuration)
        {
            SimulateTick();
            _lastTick = Time.time;
        }
    }

    private void GenerateMap()
    {
        for(int y=0; y<height; y++)
        {
            for(int x=0; x<width; x++)
            {
                GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                newCube.transform.position = new Vector3(x,y,0);
                _cells[x, y] = newCube.AddComponent<Cell>();
            }
        }
    }

    private void SimulateTick()
    {
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                int aliveNeighCount = GetNumberOfNeighbours(x, y);

                if (_cellBuffer[x, y])
                {
                    if (aliveNeighCount < 2)
                        _cells[x, y].SetCellActive(false);
                    else if (aliveNeighCount > 3)
                        _cells[x, y].SetCellActive(false);
                    else
                        _cells[x, y].SetCellActive(true);
                }
                else
                {
                    if (aliveNeighCount == 3)
                    {
                        _cells[x, y].SetCellActive(true);
                    }
                }
            }
        }
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                _cellBuffer[x,y] = _cells[x, y].On;
            }
        }
    }

    private int GetNumberOfNeighbours(int x, int y)
    {
        int count = 0;
        for (int yOff = y-1; yOff <= y+1; yOff++)
        {
            for (int xOff = x-1; xOff <= x+1; xOff++)
            {
                if(!((xOff<0 || yOff<0) || (xOff>= height || yOff>= width)) 
                    && !(xOff == x && yOff == y) 
                    && _cellBuffer[xOff, yOff])
                {
                    count++;
                }
            }
        }
        return count;
    }

}
