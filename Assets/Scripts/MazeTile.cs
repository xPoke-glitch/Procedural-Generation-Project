using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeTile : MonoBehaviour
{
    public bool IsVisited { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public List<MazeTile> UnvisitedNeighbours { get; set; }

    [Header("Walls")]
    [SerializeField]
    private GameObject leftWall;
    [SerializeField]
    private GameObject rightWall;
    [SerializeField]
    private GameObject upWall;
    [SerializeField]
    private GameObject downWall;

    private void Awake()
    {
        IsVisited = false;
    }

    public void RemoveWall(int x, int y)
    {
        // -1;0 UP
        if(x == -1 && y == 0)
        {
            Destroy(upWall);
        } // 0;-1 LEFT
        else if(x==0 && y == -1)
        {
            Destroy(leftWall);
        } // 1;0 DOWN
        else if (x == 1 && y == 0)
        {
            Destroy(downWall);
        } //0;1 RIGHT
        else if (x == 0 && y == 1)
        {
            Destroy(rightWall);
        }
    }
}
