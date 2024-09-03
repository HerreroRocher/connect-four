using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameBoardController : MonoBehaviour
{

    public TextMeshProUGUI NextPlayer; // Use TextMeshProUGUI for UI text components

    public int columns;
    public int rows;
    public GameObject columnPrefab;
    public GameObject baseRow;
    public GameObject baseCellPrefab;
    public GameObject leftBaseCellPrefab;
    public GameObject rightBaseCellPrefab;

    //want to reer to cell in column 3 row 4 as grid[3][4], so put array of columns, and then the row order 

    private ColumnController[] columnGrid;


    private int nextPlayerTurn = 0;

    private Color[] playerColours;

    private string[] playerNames;

    private bool gameOver = false;

    public int inARowReq;
    private bool waitingForPieceToLand = false;



    // Start is called before the first frame update
    void Start()
    {


        columns = GameData.columns;
        rows = GameData.rows;
        inARowReq = GameData.inARowRequirements;
        playerColours = GameData.colours;
        playerNames = GameData.players;


        columnGrid = new ColumnController[columns];
        InstantiateBoard();
        NextPlayer.text = "It's your turn,\n" + playerNames[nextPlayerTurn];

        // Debug.Log("Board created");
    }

    // Update is called once per frame
    void Update()
    {
        checkForColumnSignals();
        waitingForPieceToLand = getWaitingForStatuses();
        setWaitingForStatuses();
        setGameOverStatus();

    }

    private void setGameOverStatus()
    {
        for (int column = 0; column < columns; column++)
        {
            columnGrid[column].gameOver = gameOver;

        }
    }

    private bool getWaitingForStatuses()
    {

        for (int column = 0; column < columns; column++)
        {
            if (columnGrid[column].waitingForPieceToLand != waitingForPieceToLand)
            {
                if (columnGrid[column].waitingForPieceToLand == false)
                {
                    //This runs when a piece in any column lands
                    GameWonCheck(1 - nextPlayerTurn);
                    // GameWonCheck(nextPlayerTurn);
                }
                return columnGrid[column].waitingForPieceToLand;

            }

        }
        return waitingForPieceToLand;

    }

    private void setWaitingForStatuses()
    {
        for (int column = 0; column < columns; column++)
        {
            columnGrid[column].waitingForPieceToLand = waitingForPieceToLand;
            Debug.Log("Column: " + columnGrid[column].column + ", waitingForPieceToLand: " + columnGrid[column].waitingForPieceToLand);
        }


    }
    // Debug.Log("Game Waiting For Piece to Land: " + waitingForPieceToLand);


    private void InstantiateBoard()
    {
        InstantiateBaseRow();
        InstantiateColumns();
    }

    private void InstantiateColumns()
    {
        for (int columnNo = 0; columnNo < columns; columnNo++)
        {
            GameObject columnObj = Instantiate(columnPrefab, transform);
            columnGrid[columnNo] = columnObj.GetComponent<ColumnController>();
            columnGrid[columnNo].setColumn(columnNo + 1);
            columnGrid[columnNo].setRows(rows);
        }
    }

    private void InstantiateBaseRow()
    {
        for (int columnNo = -1; columnNo < columns + 1; columnNo++)
        {
            if (columnNo == -1)
            {
                GameObject baseCellObj = Instantiate(leftBaseCellPrefab, baseRow.transform);

            }
            else if (columnNo == columns)
            {
                GameObject baseCellObj = Instantiate(rightBaseCellPrefab, baseRow.transform);

            }
            else
            {

                GameObject baseCellObj = Instantiate(baseCellPrefab, baseRow.transform);
            }
        }
    }



    public void checkForColumnSignals()
    {

        for (int column = 0; column < columns; column++)
        {
            ColumnController columnController = columnGrid[column];
            if (columnController.pieceNeedsToBeParentedAndColoured && columnController.unplacedPiece != null)
            {
                PieceController piece = columnController.unplacedPiece;
                piece.setColour(playerColours[nextPlayerTurn]);
                piece.setBelongsTo(nextPlayerTurn);
                columnController.pieceNeedsToBeParentedAndColoured = false;

            }

            if (!GameWonCheck(nextPlayerTurn) && columnController.turnNeedsToBeSwitched)
            {
                nextPlayerTurn = 1 - nextPlayerTurn;
                NextPlayer.text = "It's your turn,\n" + playerNames[nextPlayerTurn];
                columnController.turnNeedsToBeSwitched = false;

            }

        }

    }

    public bool GameWonCheck(int playerNo)
    {

        if (checkWinner(playerNo))
        {
            // Debug.Log("WINNER");
            gameOver = true;
            NextPlayer.text = playerNames[playerNo] + " wins!";
            return true;
        }

        return false;

    }

    public bool checkWinner(int playerNo)
    {


        // creating a representation of this players pieces in the board
        int[,] playerGrid = new int[columns, rows];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                CellController currentCellChecking = columnGrid[col].getCellAtRow(row);

                bool occupied = currentCellChecking.getOccupied();

                if (occupied)
                {
                    if (currentCellChecking.getPiece().getBelongsTo() == playerNo)
                    {
                        playerGrid[col, row] = 1;

                    }
                }
                else
                {
                    playerGrid[col, row] = 0;
                }





            }
        }

        // Debug.Log("Logging grid for player " + playerNo);
        // Log2DArray(playerGrid);

        // only gonna check up direction, right, and diagonal
        // gonna have to use recursive function to count the number of in-a-rows in a certain direction

        int upwardsWin(int col, int row)
        {
            int count = 1;
            if (playerGrid[col, row] == 1)
            {
                if (row + 1 < rows)
                {
                    count += upwardsWin(col, row + 1);
                }

                return count;
            }

            return 0;


        }

        int rightwardsWin(int col, int row)
        {
            int count = 1;
            if (playerGrid[col, row] == 1)
            {
                if (col + 1 < columns)
                {
                    count += rightwardsWin(col + 1, row);
                }

                return count;
            }

            return 0;

        }

        int diagonalRightUpWin(int col, int row)
        {
            int count = 1;
            if (playerGrid[col, row] == 1)
            {
                if (col + 1 < columns && row + 1 < rows)
                {
                    count += diagonalRightUpWin(col + 1, row + 1);

                }
                return count;
            }

            return 0;

        }

        int diagonalRightDownWin(int col, int row)
        {
            int count = 1;
            if (playerGrid[col, row] == 1)
            {
                if (col + 1 < columns && row - 1 >= 0)
                {
                    count += diagonalRightDownWin(col + 1, row - 1);
                }
                return count;
            }

            return 0;

        }

        for (int col = 0; col < columns; col++)

        {
            for (int row = 0; row < rows; row++)
            {
                // Debug.Log("Number of pieces in a row upwards at column " + (col + 1) + " row " + (row + 1) + " = " + upwardsWin(col, row));
                // Debug.Log("Number of pieces in a row rightwards at column " + (col + 1) + " row " + (row + 1) + " = " + rightwardsWin(col, row));
                // Debug.Log("Number of pieces in a row diagonalRightUp at column " + (col + 1) + " row " + (row + 1) + " = " + diagonalRightUpWin(col, row));
                // Debug.Log("Number of pieces in a row diagonalRightDown at column " + (col + 1) + " row " + (row + 1) + " = " + diagonalRightDownWin(col, row));

                if (upwardsWin(col, row) == inARowReq)
                {

                    int[,] cells = new int[inARowReq, 2];

                    for (int cellNo = 0; cellNo < inARowReq; cellNo++)
                    {
                        cells[cellNo, 0] = col;
                        cells[cellNo, 1] = row + cellNo;
                    }

                    // Log2DArray(cells);
                    colourWinningPieces(cells, inARowReq);

                    return true;
                }
                else if (diagonalRightDownWin(col, row) == inARowReq)
                {
                    int[,] cells = new int[inARowReq, 2];

                    for (int cellNo = 0; cellNo < inARowReq; cellNo++)
                    {
                        cells[cellNo, 0] = col + cellNo;
                        cells[cellNo, 1] = row - cellNo;
                    }

                    // Log2DArray(cells);
                    colourWinningPieces(cells, inARowReq);
                    return true;
                }
                else if (diagonalRightUpWin(col, row) == inARowReq)
                {
                    int[,] cells = new int[inARowReq, 2];

                    for (int cellNo = 0; cellNo < inARowReq; cellNo++)
                    {
                        cells[cellNo, 0] = col + cellNo;
                        cells[cellNo, 1] = row + cellNo;
                    }

                    // Log2DArray(cells);
                    colourWinningPieces(cells, inARowReq);
                    return true;
                }
                else if (rightwardsWin(col, row) == inARowReq)
                {
                    int[,] cells = new int[inARowReq, 2];

                    for (int cellNo = 0; cellNo < inARowReq; cellNo++)
                    {
                        cells[cellNo, 0] = col + cellNo;
                        cells[cellNo, 1] = row;
                    }

                    // Log2DArray(cells);
                    colourWinningPieces(cells, inARowReq);
                    return true;
                }

            }
        }




        return false;
    }



    public void Log2DArray(int[,] array)

    {
        int rows = array.GetLength(0);
        int columns = array.GetLength(1);
        string logMessage = "2D Array:\n";

        for (int col = columns - 1; col > -1; col--)
        {
            for (int row = 0; row < rows; row++)
            {
                logMessage += array[row, col] + "\t"; // Use tab for spacing
            }
            logMessage += "\n"; // New line for the next row
                                // Debug.Log("Row iterated");
        }

        Debug.Log(logMessage);
    }

    public void colourWinningPieces(int[,] cells, int inARowReq)
    {

        for (int i = 0; i < inARowReq; i++)
        {
            int colNo = cells[i, 0];
            int rowNo = cells[i, 1];
            // Debug.Log("Need to colour cell in column " + (colNo + 1) + " row " + (rowNo + 1));

            CellController currentCellChecking = columnGrid[colNo].getCellAtRow(rowNo);

            currentCellChecking.setWon();


        }

    }


}
