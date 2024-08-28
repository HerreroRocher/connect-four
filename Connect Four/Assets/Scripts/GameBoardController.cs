using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{

    public TextMeshProUGUI NextPlayer; // Use TextMeshProUGUI for UI text components

    public int cols;
    public int rows;
    private GridLayoutGroup gridLayoutGroup;

    public GameObject cellPrefab; // Reference to the cell prefab

    //want to reer to cell in column 3 row 4 as grid[3][4], so put array of columns, and then the row order 

    private CellController[,] grid;

    private int nextPlayerTurn;

    private string[] playerColours;

    private string[] playerNames;



    // Start is called before the first frame update
    void Start()
    {
        cols = 6;
        rows = 7;

        nextPlayerTurn = 0;

        playerColours = new string[] { "red", "yellow" };

        playerNames = new string[] { "Daniel", "Natasja" };
        setNextPlayerText();



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


        // Debug.Log("Start method called");

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
                    setLowestCellInCol(columnClicked);
                    // Debug.Log("Cell clicked at column " + (columnClicked + 1) + " and attended to");
                }

            }
        }

    }

    public void setLowestCellInCol(int column)
    {

        for (int row = 0; row < rows; row++)
        {
            CellController currentCellChecking = grid[column, row];
            if (currentCellChecking.getUnoccupied() == true)
            {
                // Debug.Log("Found unoccupied cell at column " + (column + 1) + " row " + (row + 1));
                currentCellChecking.setOccupied(playerColours[nextPlayerTurn]);
                switchTurns();
                setNextPlayerText();


                break;
            }

        }

    }

    public void switchTurns()
    {
        nextPlayerTurn = 1 - nextPlayerTurn;
    }

    // Update is called once per frame
    void Update()
    {
        checkCellClicks();
    }

    public void setNextPlayerText()
    {
        // Debug.Log("text switch method called");
        // Debug.Log("player no " + nextPlayerTurn);
        // Debug.Log("player name " + playerNames[nextPlayerTurn]);
        NextPlayer.text = "It's your turn,\n" + playerNames[nextPlayerTurn];

    }
}
