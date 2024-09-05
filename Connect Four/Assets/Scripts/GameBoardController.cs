using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameBoardController : MonoBehaviour
{

    public TextMeshProUGUI NextPlayerText;
    private int _columns;
    private int _rows;
    private int _inARowRequirements;
    private Color[] _playerColors;
    private string[] _playerNames;
    public GameObject ColumnPrefab;
    public GameObject BaseRow;
    public GameObject BaseCellPrefab;
    public GameObject LeftBaseCellPrefab;
    public GameObject RightBaseCellPrefab;
    private ColumnController[] _columnGrid;
    private int _nextPlayerTurn = 0;
    private bool _isGameOver = false;
    private bool _isWaitingForPieceToLand = false;



    // Start is called before the first frame update
    private void Start()
    {


        _columns = GameData.Columns;
        _rows = GameData.Rows;
        _inARowRequirements = GameData.InARowRequirements;
        _playerColors = GameData.Colors;
        _playerNames = GameData.Players;


        _columnGrid = new ColumnController[_columns];
        InstantiateBoard();
        SetNextPlayerText();

        // Debug.Log("Board created");
    }

    // Update is called once per frame
    private void Update()
    {
        GetIsWaitingForPieceToLand();
        SetIsWaitingForPieceToLand();
        SetIsGameOverStatus();

    }


    private void SetNextPlayerText()
    {
        NextPlayerText.text = "It's your turn,\n" + _playerNames[_nextPlayerTurn];

    }

    private void SetIsGameOverStatus()
    {
        for (int column = 0; column < _columns; column++)
        {
            _columnGrid[column].SetIsGameOver(_isGameOver);

        }
    }

    private void GetIsWaitingForPieceToLand()
    {

        for (int column = 0; column < _columns; column++)
        {
            if (_columnGrid[column].GetIsWaitingForPieceToLand() != _isWaitingForPieceToLand)
            {
                _isWaitingForPieceToLand = _columnGrid[column].GetIsWaitingForPieceToLand();
                break;

            }

        }

    }

    private void SetIsWaitingForPieceToLand()
    {
        for (int column = 0; column < _columns; column++)
        {
            _columnGrid[column].SetIsWaitingForPieceToLand(_isWaitingForPieceToLand);
            // Debug.Log("Column: " + columnGrid[column].column + ", isWaitingForPieceToLand: " + columnGrid[column].isWaitingForPieceToLand);
        }


    }

    private void InstantiateBoard()
    {
        InstantiateBaseRow();
        InstantiateColumns();
    }

    private void InstantiateColumns()
    {
        for (int columnNo = 0; columnNo < _columns; columnNo++)
        {
            ColumnController columnClassInstance = Instantiate(ColumnPrefab, transform).GetComponent<ColumnController>();
            columnClassInstance.SetColumn(columnNo + 1);
            columnClassInstance.SetRows(_rows);
            columnClassInstance.SetGameBoardController(this);
            _columnGrid[columnNo] = columnClassInstance;
        }
    }

    private void InstantiateBaseRow()
    {
        for (int columnNo = -1; columnNo < _columns + 1; columnNo++)
        {
            if (columnNo == -1)
            {
                GameObject baseCellObj = Instantiate(LeftBaseCellPrefab, BaseRow.transform);

            }
            else if (columnNo == _columns)
            {
                GameObject baseCellObj = Instantiate(RightBaseCellPrefab, BaseRow.transform);

            }
            else
            {

                GameObject baseCellObj = Instantiate(BaseCellPrefab, BaseRow.transform);
            }
        }
    }

    public void SetPieceOwnerAndColor(PieceController piece)
    {
        piece.SetColor(_playerColors[_nextPlayerTurn]);
        piece.SetBelongsTo(_nextPlayerTurn);
    }

    public void CheckIfGameWon()
    {

        if (CheckWinner(_nextPlayerTurn))
        {
            // Debug.Log("WINNER");
            _isGameOver = true;
            NextPlayerText.text = _playerNames[_nextPlayerTurn] + " wins!";
        }
        else
        {
            _nextPlayerTurn = 1 - _nextPlayerTurn;
            SetNextPlayerText();
        }


    }

    private bool CheckWinner(int playerNo)
    {


        // creating a representation of this players pieces in the board
        int[,] playerGrid = new int[_columns, _rows];

        for (int row = 0; row < _rows; row++)
        {
            for (int col = 0; col < _columns; col++)
            {
                CellController currentCellChecking = _columnGrid[col].GetCellAtRow(row);

                bool isoccupied = currentCellChecking.GetIsOccupied();

                if (isoccupied)
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
                if (row + 1 < _rows)
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
                if (col + 1 < _columns)
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
                if (col + 1 < _columns && row + 1 < _rows)
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
                if (col + 1 < _columns && row - 1 >= 0)
                {
                    count += diagonalRightDownWin(col + 1, row - 1);
                }
                return count;
            }

            return 0;

        }

        for (int col = 0; col < _columns; col++)

        {
            for (int row = 0; row < _rows; row++)
            {
                // Debug.Log("Number of pieces in a row upwards at column " + (col + 1) + " row " + (row + 1) + " = " + upwardsWin(col, row));
                // Debug.Log("Number of pieces in a row rightwards at column " + (col + 1) + " row " + (row + 1) + " = " + rightwardsWin(col, row));
                // Debug.Log("Number of pieces in a row diagonalRightUp at column " + (col + 1) + " row " + (row + 1) + " = " + diagonalRightUpWin(col, row));
                // Debug.Log("Number of pieces in a row diagonalRightDown at column " + (col + 1) + " row " + (row + 1) + " = " + diagonalRightDownWin(col, row));

                if (upwardsWin(col, row) == _inARowRequirements)
                {

                    int[,] cells = new int[_inARowRequirements, 2];

                    for (int cellNo = 0; cellNo < _inARowRequirements; cellNo++)
                    {
                        cells[cellNo, 0] = col;
                        cells[cellNo, 1] = row + cellNo;
                    }

                    // Log2DArray(cells);
                    ColorWinningPieces(cells, _inARowRequirements);

                    return true;
                }
                else if (diagonalRightDownWin(col, row) == _inARowRequirements)
                {
                    int[,] cells = new int[_inARowRequirements, 2];

                    for (int cellNo = 0; cellNo < _inARowRequirements; cellNo++)
                    {
                        cells[cellNo, 0] = col + cellNo;
                        cells[cellNo, 1] = row - cellNo;
                    }

                    // Log2DArray(cells);
                    ColorWinningPieces(cells, _inARowRequirements);
                    return true;
                }
                else if (diagonalRightUpWin(col, row) == _inARowRequirements)
                {
                    int[,] cells = new int[_inARowRequirements, 2];

                    for (int cellNo = 0; cellNo < _inARowRequirements; cellNo++)
                    {
                        cells[cellNo, 0] = col + cellNo;
                        cells[cellNo, 1] = row + cellNo;
                    }

                    // Log2DArray(cells);
                    ColorWinningPieces(cells, _inARowRequirements);
                    return true;
                }
                else if (rightwardsWin(col, row) == _inARowRequirements)
                {
                    int[,] cells = new int[_inARowRequirements, 2];

                    for (int cellNo = 0; cellNo < _inARowRequirements; cellNo++)
                    {
                        cells[cellNo, 0] = col + cellNo;
                        cells[cellNo, 1] = row;
                    }

                    // Log2DArray(cells);
                    ColorWinningPieces(cells, _inARowRequirements);
                    return true;
                }

            }
        }




        return false;
    }

    private void Log2DArray(int[,] array)

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

    private void ColorWinningPieces(int[,] cells, int inARowReq)
    {

        for (int i = 0; i < inARowReq; i++)
        {
            int colNo = cells[i, 0];
            int rowNo = cells[i, 1];
            // Debug.Log("Need to color cell in column " + (colNo + 1) + " row " + (rowNo + 1));

            CellController currentCellChecking = _columnGrid[colNo].GetCellAtRow(rowNo);

            currentCellChecking.SetWon();


        }

    }


}
