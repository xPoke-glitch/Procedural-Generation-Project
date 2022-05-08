using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Maze : MonoBehaviour
{
    [SerializeField]
    private GameObject tilePrefab;

    [Header("Settings")]
    [SerializeField]
    private int dimension;
    [SerializeField]
    private int delay;

    private List<List<MazeTile>> tiles = new List<List<MazeTile>>();

    private void Start()
    {
        StartCoroutine(COGenerateMazeMatrix(() => {

            MazeGeneration(0, 0);
            
            //MazeTile chosenCell = GetRandomUnvisitedNeighbour(9, 0);

        },delay));
    }

    private IEnumerator COGenerateMazeMatrix(Action OnGenerateFinish, float delay = 0.1f)
    {
        for (int i = 0; i < dimension; ++i)
        {
            tiles.Add(new List<MazeTile>());
            for (int j = 0; j < dimension; ++j)
            {
                GameObject obj = Instantiate(tilePrefab, new Vector3(i * 4, 0, j * 4), Quaternion.identity);
                
                MazeTile temp = obj.GetComponent<MazeTile>();
                temp.X = i;
                temp.Y = j;
                
                tiles[i].Add(temp);
                yield return new WaitForSeconds(delay);
            }
        }
        OnGenerateFinish?.Invoke();
    }

    // TO-DO: Maybe better as coroutine
    private void MazeGeneration(int x, int y)
    {
        Stack<MazeTile> stack = new Stack<MazeTile>();
        MazeTile currentCell = null;

        // Choose the initial cell
        currentCell = tiles[x][y]; // TO-DO: Make it choosable
        currentCell.X = x;
        currentCell.Y = y;

        // Visit the cell
        currentCell.IsVisited = true;
        // Push it to the stack
        stack.Push(currentCell);


        // While stack is not empty
        while (stack.Count != 0)
        {
            // Pop current cell from stack
            currentCell = stack.Pop();

            // Initialize neighbours list
            FindUnvisitedNeighbours(currentCell);

            // If the current cell has any neighbours which have not been visited
            while (currentCell.UnvisitedNeighbours.Count != 0)
            {
                // Choose one of the unvisited neighbours
                MazeTile chosenCell = GetRandomUnvisitedNeighbour(currentCell.X, currentCell.Y);
                // Remove the wall between the current cell and the chosen cell
                currentCell.RemoveWall(chosenCell.X - currentCell.X, chosenCell.Y - currentCell.Y);
                chosenCell.RemoveWall(currentCell.X - chosenCell.X, currentCell.Y - chosenCell.Y);
                // Mark the chosen cell as visited and push it to the stack
                chosenCell.IsVisited = true;
                stack.Push(chosenCell);
            }   
        }

    }

    private MazeTile GetRandomUnvisitedNeighbour(int x, int y)
    {
        MazeTile currentTile = tiles[x][y];

        FindUnvisitedNeighbours(currentTile);

        // Get random unvisited neighbour
        int randIndex = UnityEngine.Random.Range(0, currentTile.UnvisitedNeighbours.Count);
        MazeTile neighbour = currentTile.UnvisitedNeighbours[randIndex];
        currentTile.UnvisitedNeighbours.Remove(neighbour);

        return neighbour;
    }

    private void FindUnvisitedNeighbours(MazeTile currentTile)
    {
        int x = currentTile.X;
        int y = currentTile.Y;

        // Find unvisited neighbours
        currentTile.UnvisitedNeighbours = new List<MazeTile>();
        // Up
        if (x - 1 >= 0 && !tiles[x - 1][y].IsVisited)
        {
            currentTile.UnvisitedNeighbours.Add(tiles[x - 1][y]);
        }
        // Down
        if (x + 1 < dimension && !tiles[x + 1][y].IsVisited)
        {
            currentTile.UnvisitedNeighbours.Add(tiles[x + 1][y]);
        }
        // Right
        if (y + 1 < dimension && !tiles[x][y+1].IsVisited)
        {
            currentTile.UnvisitedNeighbours.Add(tiles[x][y + 1]);
        }
        // Left
        if (y - 1 >= 0 && !tiles[x][y - 1].IsVisited)
        {
            currentTile.UnvisitedNeighbours.Add(tiles[x][y - 1]);
        }
    }
}
