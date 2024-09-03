using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameBoardController : MonoBehaviour
{

    public TextMeshProUGUI NextPlayer;
    private int columns;
    private int rows;
    private int inARowReq;
    private Color[] playerColours;
    private string[] playerNames;
    public GameObject columnPrefab;
    public GameObject baseRow;
    public GameObject baseCellPrefab;
    public GameObject leftBaseCellPrefab;
    public GameObject rightBaseCellPrefab;
    private ColumnController[] columnGrid;
    private int nextPlayerTurn = 0;
    private bool gameOver = false;
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
        CheckForPiecesWhichNeedParentingAndColouring();
        GetWaitingForPieceToLand();
        SetWaitingForPieceToLand();
        SetGameOverStatus();

    }

    void SetGameOverStatus()
    {
        for (int column = 0; column < columns; column++)
        {
            columnGrid[column].SetGameOver(gameOver);

        }
    }

    void GetWaitingForPieceToLand()
    {

        for (int column = 0; column < columns; column++)
        {
            if (columnGrid[column].GetWaitingForPieceToLand() != waitingForPieceToLand)
            {
                if (columnGrid[column].GetWaitingForPieceToLand() == false)
                {
                    //This runs when a piece in any column lands
                    GameWonCheck(1 - nextPlayerTurn);
                    // GameWonCheck(nextPlayerTurn);
                }
                waitingForPieceToLand = columnGrid[column].GetWaitingForPieceToLand();
                break;

            }

        }

    }

    void SetWaitingForPieceToLand()
    {
        for (int column = 0; column < columns; column++)
        {
            columnGrid[column].SetWaitingForPieceToLand(waitingForPieceToLand);
            // Debug.Log("Column: " + columnGrid[column].column + ", waitingForPieceToLand: " + columnGrid[column].waitingForPieceToLand);
        }


    }

    void InstantiateBoard()
    {
        InstantiateBaseRow();
        InstantiateColumns();
    }

    void InstantiateColumns()
    {
        for (int columnNo = 0; columnNo < columns; columnNo++)
        {
            GameObject columnObj = Instantiate(columnPrefab, transform);
            columnGrid[columnNo] = columnObj.GetComponent<ColumnController>();
            columnGrid[columnNo].SetColumn(columnNo + 1);
            columnGrid[columnNo].SetRows(rows);
        }
    }

    void InstantiateBaseRow()
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

    void CheckForPiecesWhichNeedParentingAndColouring()
    {

        for (int column = 0; column < columns; column++)
        {
            ColumnController columnController = columnGrid[column];
            if (columnController.GetPieceNeedsToBeParentedAndColoured() && columnController.GetUnplacedPiece())
            {
                PieceController piece = columnController.GetUnplacedPiece();
                piece.SetColour(playerColours[nextPlayerTurn]);
                piece.SetBelongsTo(nextPlayerTurn);
                columnController.SetPieceNeedsToBeParentedAndColoured(false);

            }

            if (!GameWonCheck(nextPlayerTurn) && columnController.GetTurnNeedsToBeSwitched())
            {
                nextPlayerTurn = 1 - nextPlayerTurn;
                NextPlayer.text = "It's your turn,\n" + playerNames[nextPlayerTurn];
                columnController.SetTurnNeedsToBeSwitched(false);

            }

        }

    }

    bool GameWonCheck(int playerNo)
    {

        if (CheckWinner(playerNo))
        {
            // Debug.Log("WINNER");
            gameOver = true;
            NextPlayer.text = playerNames[playerNo] + " wins!";
            return true;
        }

        return false;

    }

    bool CheckWinner(int playerNo)
    {


        // creating a representation of this players pieces in the board
        int[,] playerGrid = new int[columns, rows];

        for (int row = 0; row < rows; row++)
        {
            for (int col = 0; col < columns; col++)
            {
                CellController currentCellChecking = columnGrid[col].GetCellAtRow(row);

                bool occupied = currentCellChecking.GetOccupied();

                if (occupied)
                {
                    if (currentCellChecking.GetPiece().GetBelongsTo() == playerNo)
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
                    ColourWinningPieces(cells, inARowReq);

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
                    ColourWinningPieces(cells, inARowReq);
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
                    ColourWinningPieces(cells, inARowReq);
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
                    ColourWinningPieces(cells, inARowReq);
                    return true;
                }

            }
        }




        return false;
    }

    void Log2DArray(int[,] array)

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

    void ColourWinningPieces(int[,] cells, int inARowReq)
    {

        for (int i = 0; i < inARowReq; i++)
        {
            int colNo = cells[i, 0];
            int rowNo = cells[i, 1];
            // Debug.Log("Need to colour cell in column " + (colNo + 1) + " row " + (rowNo + 1));

            CellController currentCellChecking = columnGrid[colNo].GetCellAtRow(rowNo);

            currentCellChecking.SetWon();


        }

    }


}
