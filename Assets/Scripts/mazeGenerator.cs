using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class mazeGenerator : MonoBehaviour
{
    [System.Serializable]
    public class Cell
    {
        public bool visited;

        public GameObject north;//1
        public GameObject east;//2
        public GameObject west;//3
        public GameObject south;//4
    }

    private int visitedCells = 0;
    private bool startedBuilding = false;
    private int currentNeighbor = 0;
    private List<int> lastCells;

    public GameObject wall;
    public GameObject torche;
    public float wallLength = 1.0f;
    public int xSize = 5;
    public int ySize = 5;
    public int numberOfTorchesX = 30;
    public int numberOfTorchesY = 30;

    public float forwardDistance = 1.0f;

    private int currentCell = 0;
    private int totalCells;
    private int backingUp = 0;
    private int wallBreak = 0;

    private Cell[] cells;

    private Vector3 initialPos;
    private GameObject wallHolder;
    public float startingPos;

    void Start()
    {
        CreateWalls();
    }

    void CreateWalls()
    {
        wallHolder = new GameObject();
        wallHolder.name = "Maze";

        initialPos = new Vector3((-xSize / 2) + wallLength / 2.0f, startingPos, (-ySize / 2) + wallLength / 2);
        Vector3 myPos = initialPos;
        GameObject tempWall;

        //x-axis
        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j <= xSize; j++)
            {
                myPos = new Vector3(initialPos.x + (j * wallLength) - wallLength / 2, startingPos, initialPos.z + (i * wallLength) - wallLength / 2);
                tempWall = Instantiate(wall, myPos, Quaternion.identity) as GameObject;
                tempWall.transform.parent = wallHolder.transform;
                if (j % Random.Range(numberOfTorchesX, numberOfTorchesY) == 0) {
                    GameObject newTorche = Instantiate(torche, tempWall.transform.position, tempWall.transform.rotation) as GameObject;
                    newTorche.transform.position = tempWall.transform.position;
                    //newTorche.transform = new Vector3(1, 0.3f, 0.3f);
                    newTorche.transform.rotation = Quaternion.Euler(40.0f, 90.0f, 0.0f);
                    newTorche.transform.Translate(newTorche.transform.right * forwardDistance, newTorche.transform);
                }
            }
        }

        //y-axis
        for (int i = 0; i <= ySize; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                myPos = new Vector3(initialPos.x + (j * wallLength), startingPos, initialPos.z + (i * wallLength) - wallLength);
                tempWall = Instantiate(wall, myPos, Quaternion.Euler(0.0f, 90.0f, 0.0f)) as GameObject;
                tempWall.transform.parent = wallHolder.transform;
                if (j % Random.Range(numberOfTorchesX, numberOfTorchesY) == 0) {
                    GameObject newTorche = Instantiate(torche, tempWall.transform.position, tempWall.transform.rotation) as GameObject;
                    newTorche.transform.position = tempWall.transform.position;
                    //newTorche.transform = new Vector3(1, 0.3f, 0.3f);
                    newTorche.transform.rotation = Quaternion.Euler(40.0f, 90.0f, 0.0f);
                    //newTorche.transform.Translate(newTorche.transform.right * forwardDistance, newTorche.transform);
                }
            }
        }

        CreateCells();

    }

    void CreateCells()
    {
        lastCells = new List<int>();
        lastCells.Clear();
        totalCells = xSize * ySize;
        GameObject[] allWalls;
        int children = wallHolder.transform.childCount;
        allWalls = new GameObject[children];
        cells = new Cell[xSize * ySize];
        int eastWestProcess = 0;
        int childProcess = 0;
        int termCount = 0;

        for (int i = 0; i < children; i++)
        {
            allWalls[i] = wallHolder.transform.GetChild(i).gameObject;
        }

        for (int cellProcess = 0; cellProcess < cells.Length; cellProcess++)
        {
            cells[cellProcess] = new Cell();
            cells[cellProcess].east = allWalls[eastWestProcess];
            cells[cellProcess].south = allWalls[childProcess + (xSize + 1) * ySize];
            if (termCount == xSize)
            {
                eastWestProcess += 2;
                termCount = 0;
            }
            else
            {
                eastWestProcess++;
            }

            termCount++;
            childProcess++;

            cells[cellProcess].west = allWalls[eastWestProcess];
            cells[cellProcess].north = allWalls[(childProcess + (xSize + 1) * ySize) + xSize - 1];
        }

        CreateMaze();
    }

    void BreakWall()
    {
        switch (wallBreak)
        {
            case 1:
                Destroy(cells[currentCell].north);
                break;
            case 2:
                Destroy(cells[currentCell].east);
                break;
            case 3:
                Destroy(cells[currentCell].west);
                break;
            case 4:
                Destroy(cells[currentCell].south);
                break;
        }

    }

    void CreateMaze()
    {
        while (visitedCells < totalCells)
        {
            if (startedBuilding)
            {
                GetNeighbor();
                if (cells[currentNeighbor].visited == false && cells[currentCell].visited == true)
                {
                    BreakWall();
                    cells[currentNeighbor].visited = true;
                    visitedCells++;
                    lastCells.Add(currentCell);
                    currentCell = currentNeighbor;

                    if (lastCells.Count > 0)
                    {
                        backingUp = lastCells.Count - 1;
                    }
                }
            }
            else
            {
                currentCell = Random.Range(0, totalCells);
                cells[currentCell].visited = true;
                visitedCells++;
                startedBuilding = true;
            }

        }
    }

    void GetNeighbor()
    {
        int[] connectingWall = new int[4];
        int length = 0;
        int[] neighbors = new int[4];
        int check = 0;

        check = (currentCell + 1) / xSize;
        check -= 1;
        check *= xSize;
        check += xSize;

        //west
        if (currentCell + 1 < totalCells && (currentCell + 1) != check)
        {
            if (cells[currentCell + 1].visited == false)
            {
                neighbors[length] = currentCell + 1;
                connectingWall[length] = 3;
                length++;
            }
        }

        //east
        if (currentCell - 1 >= 0 && currentCell != check)
        {
            if (cells[currentCell - 1].visited == false)
            {
                neighbors[length] = currentCell - 1;
                connectingWall[length] = 2;
                length++;
            }
        }

        //north
        if (currentCell + xSize < totalCells)
        {
            if (cells[currentCell + xSize].visited == false)
            {
                neighbors[length] = currentCell + xSize;
                connectingWall[length] = 1;
                length++;
            }
        }

        //south
        if (currentCell - xSize >= 0)
        {
            if (cells[currentCell - xSize].visited == false)
            {
                neighbors[length] = currentCell - xSize;
                connectingWall[length] = 4;
                length++;
            }
        }

        if (length != 0)
        {
            int chosen = Random.Range(0, length);
            currentNeighbor = neighbors[chosen];
            wallBreak = connectingWall[chosen];

        }
        else
        {
            if (backingUp > 0)
            {
                currentCell = lastCells[backingUp];
                backingUp--;
            }
        }

    }
}
