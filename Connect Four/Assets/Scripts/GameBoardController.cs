using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{

    public TextMeshProUGUI NextPlayer; // Use TextMeshProUGUI for UI text components

    public int columns = 7;
    public int rows = 7;
    private GridLayoutGroup gridLayoutGroup;

    public GameObject columnPrefab;
    public GameObject leftColumnPrefab;
    public GameObject rightColumnPrefab;

    //want to reer to cell in column 3 row 4 as grid[3][4], so put array of columns, and then the row order 

    private ColumnController[] columnGrid;


    private int nextPlayerTurn = 0;

    private Color[] playerColours = new Color[] { Color.red, Color.yellow };

    private string[] playerNames = new string[] { "Daniel", "Natasja" };

    private bool gameOver = false;


    public int inARowReq = 4;



    // Start is called before the first frame update
    void Start()
    {
        // cols = GameData.columns;
        // rows = GameData.rows;
        // inARowReq = GameData.inARowRequirements;
        // playerColours = GameData.colours;
        // playerNames = GameData.players;
        // Debug.Log("Start method called");

        columnGrid = new ColumnController[columns];
        InstantiateBoard();

        // Debug.Log("Board created");



    }

    private void InstantiateBoard()
    {
        for (int columnNo = -1; columnNo < columns + 1; columnNo++)
        {
            if (columnNo == -1)
            {
                Instantiate(leftColumnPrefab, transform);
            }
            else if (columnNo == columns)
            {
                Instantiate(rightColumnPrefab, transform);

            }
            else
            {
                GameObject columnObj = Instantiate(columnPrefab, transform);
                columnGrid[columnNo] = columnObj.GetComponent<ColumnController>();
                columnGrid[columnNo].setColumn(columnNo+1);
                columnGrid[columnNo].setRows(rows);
            }

        }
    }




    // public void checkCellClicks()
    // {

    //     for (int row = 0; row < rows; row++)
    //     {
    //         for (int col = 0; col < cols; col++)
    //         {
    //             CellController currentCellChecking = grid[col, row];
    //             if (currentCellChecking.getUnattendedCheck() == true)
    //             {
    //                 int columnClicked = currentCellChecking.getColumn();
    //                 currentCellChecking.setCheckAttended();
    //                 setLowestCellInCol(columnClicked);
    //                 // Debug.Log("Cell clicked at column " + (columnClicked + 1) + " and attended to");
    //             }

    //         }
    //     }

    // }

    // public void setLowestCellInCol(int column)
    // {

    //     if (!gameOver)
    //     {
    //         for (int row = 0; row < rows; row++)
    //         {
    //             CellController currentCellChecking = grid[column, row];
    //             if (currentCellChecking.getUnoccupied() == true)
    //             {
    //                 // Debug.Log("Found unoccupied cell at column " + (column + 1) + " row " + (row + 1));
    //                 currentCellChecking.setOccupied(playerColours[nextPlayerTurn], nextPlayerTurn);

    //                 if (!GameWonCheck(nextPlayerTurn))
    //                 {
    //                     switchTurns();
    //                     setSideText();
    //                 }


    //                 break;
    //             }
    //         }

    //     }

    // }

    // public void switchTurns()
    // {
    //     nextPlayerTurn = 1 - nextPlayerTurn;
    // }

    // // Update is called once per frame
    // void Update()
    // {
    //     checkCellClicks();
    // }

    // private void setSideText()
    // {
    //     // Debug.Log("text switch method called");
    //     // Debug.Log("player no " + nextPlayerTurn);
    //     // Debug.Log("player name " + playerNames[nextPlayerTurn]);
    //     NextPlayer.text = "It's your turn,\n" + playerNames[nextPlayerTurn];

    // }

    // public bool GameWonCheck(int playerNo)
    // {

    //     if (checkWinner(playerNo))
    //     {
    //         // Debug.Log("WINNER");
    //         gameOver = true;
    //         NextPlayer.text = playerNames[playerNo] + " wins!";
    //         return true;
    //     }

    //     return false;

    // }

    // public bool checkWinner(int playerNo)
    // {


    //     // creating a representation of this players pieces in the board
    //     int[,] playerGrid = new int[cols, rows];

    //     for (int row = 0; row < rows; row++)
    //     {
    //         for (int col = 0; col < cols; col++)
    //         {
    //             CellController currentCellChecking = grid[col, row];


    //             if (currentCellChecking.getBelongsTo() == playerNo)
    //             {
    //                 playerGrid[col, row] = 1;

    //             }
    //             else
    //             {
    //                 playerGrid[col, row] = 0;
    //             }




    //         }
    //     }

    //     // Debug.Log("Logging grid for player " + playerNo);
    //     // Log2DArray(playerGrid);

    //     // only gonna check up direction, right, and diagonal
    //     // gonna have to use recursive function to count the number of in-a-rows in a certain direction

    //     int upwardsWin(int col, int row)
    //     {
    //         int count = 1;
    //         if (playerGrid[col, row] == 1)
    //         {
    //             if (row + 1 < rows)
    //             {
    //                 count += upwardsWin(col, row + 1);
    //             }

    //             return count;
    //         }

    //         return 0;


    //     }

    //     int rightwardsWin(int col, int row)
    //     {
    //         int count = 1;
    //         if (playerGrid[col, row] == 1)
    //         {
    //             if (col + 1 < cols)
    //             {
    //                 count += rightwardsWin(col + 1, row);
    //             }

    //             return count;
    //         }

    //         return 0;

    //     }

    //     int diagonalRightUpWin(int col, int row)
    //     {
    //         int count = 1;
    //         if (playerGrid[col, row] == 1)
    //         {
    //             if (col + 1 < cols && row + 1 < rows)
    //             {
    //                 count += diagonalRightUpWin(col + 1, row + 1);

    //             }
    //             return count;
    //         }

    //         return 0;

    //     }

    //     int diagonalRightDownWin(int col, int row)
    //     {
    //         int count = 1;
    //         if (playerGrid[col, row] == 1)
    //         {
    //             if (col + 1 < cols && row - 1 >= 0)
    //             {
    //                 count += diagonalRightDownWin(col + 1, row - 1);
    //             }
    //             return count;
    //         }

    //         return 0;

    //     }

    //     for (int col = 0; col < cols; col++)

    //     {
    //         for (int row = 0; row < rows; row++)
    //         {
    //             // Debug.Log("Number of pieces in a row upwards at column " + (col + 1) + " row " + (row + 1) + " = " + upwardsWin(col, row));
    //             // Debug.Log("Number of pieces in a row rightwards at column " + (col + 1) + " row " + (row + 1) + " = " + rightwardsWin(col, row));
    //             // Debug.Log("Number of pieces in a row diagonalRightUp at column " + (col + 1) + " row " + (row + 1) + " = " + diagonalRightUpWin(col, row));
    //             // Debug.Log("Number of pieces in a row diagonalRightDown at column " + (col + 1) + " row " + (row + 1) + " = " + diagonalRightDownWin(col, row));

    //             if (upwardsWin(col, row) == inARowReq)
    //             {

    //                 int[,] cells = new int[inARowReq, 2];

    //                 for (int cellNo = 0; cellNo < inARowReq; cellNo++)
    //                 {
    //                     cells[cellNo, 0] = col;
    //                     cells[cellNo, 1] = row + cellNo;
    //                 }

    //                 // Log2DArray(cells);
    //                 colourWinningPieces(cells, inARowReq);

    //                 return true;
    //             }
    //             else if (diagonalRightDownWin(col, row) == inARowReq)
    //             {
    //                 int[,] cells = new int[inARowReq, 2];

    //                 for (int cellNo = 0; cellNo < inARowReq; cellNo++)
    //                 {
    //                     cells[cellNo, 0] = col + cellNo;
    //                     cells[cellNo, 1] = row - cellNo;
    //                 }

    //                 // Log2DArray(cells);
    //                 colourWinningPieces(cells, inARowReq);
    //                 return true;
    //             }
    //             else if (diagonalRightUpWin(col, row) == inARowReq)
    //             {
    //                 int[,] cells = new int[inARowReq, 2];

    //                 for (int cellNo = 0; cellNo < inARowReq; cellNo++)
    //                 {
    //                     cells[cellNo, 0] = col + cellNo;
    //                     cells[cellNo, 1] = row + cellNo;
    //                 }

    //                 // Log2DArray(cells);
    //                 colourWinningPieces(cells, inARowReq);
    //                 return true;
    //             }
    //             else if (rightwardsWin(col, row) == inARowReq)
    //             {
    //                 int[,] cells = new int[inARowReq, 2];

    //                 for (int cellNo = 0; cellNo < inARowReq; cellNo++)
    //                 {
    //                     cells[cellNo, 0] = col + cellNo;
    //                     cells[cellNo, 1] = row;
    //                 }

    //                 // Log2DArray(cells);
    //                 colourWinningPieces(cells, inARowReq);
    //                 return true;
    //             }

    //         }
    //     }




    //     return false;
    // }



    // public void Log2DArray(int[,] array)

    // {
    //     int rows = array.GetLength(0);
    //     int cols = array.GetLength(1);
    //     string logMessage = "2D Array:\n";

    //     for (int col = cols - 1; col > -1; col--)
    //     {
    //         for (int row = 0; row < rows; row++)
    //         {
    //             logMessage += array[row, col] + "\t"; // Use tab for spacing
    //         }
    //         logMessage += "\n"; // New line for the next row
    //                             // Debug.Log("Row iterated");
    //     }

    //     Debug.Log(logMessage);
    // }

    // public void colourWinningPieces(int[,] cells, int inARowReq)
    // {

    //     for (int i = 0; i < inARowReq; i++)
    //     {
    //         int colNo = cells[i, 0];
    //         int rowNo = cells[i, 1];
    //         // Debug.Log("Need to colour cell in column " + (colNo + 1) + " row " + (rowNo + 1));

    //         CellController currentCellChecking = grid[colNo, rowNo];

    //         currentCellChecking.setWon();


    //     }

    // }


}
