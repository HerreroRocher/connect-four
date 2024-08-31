using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ColumnController : MonoBehaviour, IPointerDownHandler
{

    private CellController[] cellGrid;

    public GameObject cellPrefab;
    public GameObject baseCellPrefab;
    private int rows;
    private int column;

    public GameObject piecePrefab;

    private int bottomCellIndex = 0;
    private bool hasUnattendedCheck = false;
    private PieceController pieceThatNeedsColouring;
    private bool waitingForPieceToDrop = false;
    private PieceController pieceWeWaitingFor;


    public void InstantiateCells()
    {
        Instantiate(baseCellPrefab, transform);

        // Debug.Log("Column created");
        for (int cellRowNo = 0; cellRowNo < rows; cellRowNo++)
        {
            GameObject cellGameObj = Instantiate(cellPrefab, transform);
            CellController cellInstance = cellGameObj.GetComponent<CellController>();
            cellInstance.setRow(cellRowNo);
            cellInstance.setColumn(column);

            cellGrid[cellRowNo] = cellInstance;
        }
    }

    public void setRows(int rows)
    {
        this.rows = rows;
        cellGrid = new CellController[rows];
        InstantiateCells();
    }

    public void setColumn(int column)
    {
        this.column = column;
    }

    public void OnPointerDown(PointerEventData pointer)
    {
        // Debug.Log("Cell clicked");
        // Debug.Log("Clicked on cell in column " + column);
        pieceThatNeedsColouring = DropPiece();
        hasUnattendedCheck = true;

    }

    public PieceController DropPiece()
    {
        Vector3 spawnPosition = transform.position + new Vector3(0, 5, 0);
        PieceController piece = Instantiate(piecePrefab, spawnPosition, Quaternion.identity, transform).GetComponent<PieceController>();
        piece.setParent(cellGrid[bottomCellIndex].gameObject);
        setPieceWhenDropped(piece);
        return piece;
    }

    public void setPieceWhenDropped(PieceController piece)
    {
        pieceWeWaitingFor = piece;
        waitingForPieceToDrop = true;

    }

    public void Update()
    {
        if (waitingForPieceToDrop)
        {
            if (pieceWeWaitingFor.getStoppedMoving())
            {
                getBottomCell().setPiece(pieceWeWaitingFor);
                bottomCellIndex += 1;
                waitingForPieceToDrop = false;


            }
        }
    }

    public CellController getBottomCell()
    {
        return cellGrid[bottomCellIndex];
    }

    public bool getHasUnattendedCheck()
    {
        return hasUnattendedCheck;
    }

    public PieceController getPieceThatNeedsColouring()
    {
        return pieceThatNeedsColouring;
    }

    public void setCheckAttendedTo()
    {
        hasUnattendedCheck = false;
    }

    public CellController getCellAtRow(int row)
    {
        return cellGrid[row];
    }


}
