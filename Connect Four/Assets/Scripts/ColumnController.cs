using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ColumnController : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    private CellController[] cellGrid;
    public PieceController unplacedPiece;
    public GameObject cellPrefab;
    public GameObject piecePrefab;
    private int rows;
    private int column;
    private int bottomCellIndex = 0;
    public bool pieceNeedsToBeParentedAndColoured = false;
    public bool thisColumnWaitingForPieceToLand = false;
    public bool anotherColumnWaitingForPieceToLand = false;
    public bool gameOver = false;
    public bool hovering = false;
    public bool turnNeedsToBeSwitched = false;

    public void InstantiateCells()
    {
        // Debug.Log("Column created");
        for (int cellRowNo = 0; cellRowNo < rows; cellRowNo++)
        {
            cellGrid[cellRowNo] = Instantiate(cellPrefab, transform).GetComponent<CellController>();
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
        if (unplacedPiece != null && !gameOver)
        {
            unplacedPiece.setParent(cellGrid[bottomCellIndex].gameObject);
            unplacedPiece.setDynamic();
            thisColumnWaitingForPieceToLand = true;
            turnNeedsToBeSwitched = true;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
        createPiece();
    }

    public void createPiece()
    {
        if (!thisColumnWaitingForPieceToLand && !anotherColumnWaitingForPieceToLand && !gameOver)
        {
            unplacedPiece = Instantiate(piecePrefab, transform.position + new Vector3(0, 5, 0), Quaternion.identity, transform).GetComponent<PieceController>();
            pieceNeedsToBeParentedAndColoured = true;
        }
    }



    // Called when the pointer exits the UI element or GameObject
    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
        if (unplacedPiece != null && !thisColumnWaitingForPieceToLand)
        {
            Destroy(unplacedPiece.gameObject);
            unplacedPiece = null;
        }
    }


    public void Update()
    {
        if (thisColumnWaitingForPieceToLand)
        {

            if (unplacedPiece.getStoppedMoving())
            {
                getBottomCell().setPiece(unplacedPiece);
                unplacedPiece = null;
                bottomCellIndex += 1;
                thisColumnWaitingForPieceToLand = false;
                if (hovering)
                {
                    createPiece();
                }

            }

        }


    }

    public CellController getBottomCell()
    {
        return cellGrid[bottomCellIndex];
    }

    public CellController getCellAtRow(int row)
    {
        return cellGrid[row];
    }


}
