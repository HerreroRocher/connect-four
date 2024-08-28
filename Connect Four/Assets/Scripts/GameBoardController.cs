using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Include the UI namespace to access GridLayoutGroup

public class NewBehaviourScript : MonoBehaviour
{

    public int cols;
    public int rows;
    private GridLayoutGroup gridLayoutGroup;

    public GameObject cellPrefab; // Reference to the cell prefab

    //want to reer to cell in column 3 row 4 as grid[3][4], so put array of columns, and then the row order 

    private CellController[,] grid;



    // Start is called before the first frame update
    void Start()
    {
        cols = 6;
        rows = 7;

        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        if (gridLayoutGroup != null)
        {
            // Set the constraint type to FixedColumnCount (or FixedRowCount as needed)
            gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;

            // Set the number of columns
            gridLayoutGroup.constraintCount = cols;
        }
        else
        {
            Debug.LogError("GridLayoutGroup component not found!");
        }

        grid = new CellController[cols, rows];


        Debug.Log("Start method called");

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                GameObject cell = Instantiate(cellPrefab, new Vector3(col, row, 0), Quaternion.identity, transform);
                grid[col, row] = cell.GetComponent<CellController>();
                grid[col, row].setCoords(col, row);
            }
        }


    }

    public void checkCellClicks()
    {

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < cols; col++)
            {
                CellController currentCellChecking = grid[col, row];
                if (currentCellChecking.getUnattendedCheck() == true)
                {
                    int columnClicked = currentCellChecking.getColumn();
                    currentCellChecking.setCheckAttended();
                    Debug.Log("Cell clicked at column " + (columnClicked + 1) + " and attended to");
                }

            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        checkCellClicks();
    }
}
