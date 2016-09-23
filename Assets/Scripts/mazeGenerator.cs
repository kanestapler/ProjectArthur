using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MazeGenerator : MonoBehaviour
{
    public GameObject wall;
    public float xSize = 5.0f;
    public float ySize = 5.0f;
    public float wallLength = 1.0f;

    private Vector3 initialPos;
    

    void Start()
    {
        CreateWalls();
    }

    void CreateWalls()
    {
        initialPos = new Vector3((-xSize/2) + wallLength/2, 0f, (-ySize/2) + wallLength/2);
        Vector3 myPos = initialPos;

        //making a grid

        //makes walls along the x axis
        for(int i = 0; i < ySize; i++)
        {
            for(int j = 0; j <= xSize; j++)
            {
                myPos = new Vector3(initialPos.x + (j * wallLength) - wallLength/2.0f, 0.0f, initialPos.z + (i * wallLength) - wallLength/2.0f);
                Instantiate(wall, myPos, Quaternion.identity);
            }
        }

        //makes walls along the y axis
        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j <= xSize; j++)
            {
                myPos = new Vector3(initialPos.x + (j * wallLength), 0.0f, initialPos.z + (i * wallLength) - wallLength);
                Instantiate(wall, myPos, Quaternion.Euler(0.0f, 90.0f, 0.0f));
            }
        }

        
        CreateCells();
    }

    void CreateCells()
    {

    }

    void Update()
    {

    }
}