using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ColumnController : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{

    private CellController[] cellGrid;
    private PieceController unplacedPiece;
    private GameObject cellPrefab;
    private GameObject piecePrefab;
    private int rows;
    private int column;
    private int bottomCellIndex = 0;
    private bool pieceNeedsToBeParentedAndColoured = false;
    private bool waitingForPieceToLand = false;
    private bool gameOver = false;
    private bool hovering = false;
    private bool turnNeedsToBeSwitched = false;

    void InstantiateCells()
    {
        // Debug.Log("Column created");
        for (int cellRowNo = 0; cellRowNo < rows; cellRowNo++)
        {
            cellGrid[cellRowNo] = Instantiate(cellPrefab, transform).GetComponent<CellController>();
        }
    }

    public void OnPointerDown(PointerEventData pointer)
    {
        // Debug.Log("Cell clicked");
        // Debug.Log("Clicked on cell in column " + column);
        if (unplacedPiece != null && !gameOver)
        {
            unplacedPiece.setParent(cellGrid[bottomCellIndex].gameObject);
            unplacedPiece.setDynamic();
            waitingForPieceToLand = true;
            turnNeedsToBeSwitched = true;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
        createPiece();
    }

    void createPiece()
    {
        // Debug.Log("waitingForPieceToLand " + waitingForPieceToLand);
        if (!waitingForPieceToLand && !gameOver)
        {
            unplacedPiece = Instantiate(piecePrefab, transform.position + new Vector3(0, 5, 0), Quaternion.identity, transform).GetComponent<PieceController>();
            pieceNeedsToBeParentedAndColoured = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hovering = false;
        DestroyPiece();

    }

    void DestroyPiece()
    {
        if (unplacedPiece != null && !waitingForPieceToLand)
        {
            Destroy(unplacedPiece.gameObject);
            unplacedPiece = null;
        }
    }

    void Update()
    {
        if (waitingForPieceToLand && unplacedPiece != null)
        {

            if (unplacedPiece.getStoppedMoving())
            {
                getBottomCell().setPiece(unplacedPiece);
                unplacedPiece = null;
                bottomCellIndex += 1;
                waitingForPieceToLand = false;


            }

        }
        if (hovering && !waitingForPieceToLand && unplacedPiece == null)
        {
            createPiece();
        }
        if (gameOver && unplacedPiece != null)
        {
            DestroyPiece();
        }
    }

    CellController getBottomCell()
    {
        return cellGrid[bottomCellIndex];
    }

    public void setGameOver(bool gameOver)
    {
        this.gameOver = gameOver;
    }

    public bool getWaitingForPieceToLand()
    {
        return waitingForPieceToLand;
    }

    public void setWaitingForPieceToLand(bool waitingForPieceToLand)
    {
        this.waitingForPieceToLand = waitingForPieceToLand;
    }

    public bool getPieceNeedsToBeParentedAndColoured()
    {
        return pieceNeedsToBeParentedAndColoured;
    }

    public void setPieceNeedsToBeParentedAndColoured(bool pieceNeedsToBeParentedAndColoured)
    {
        this.pieceNeedsToBeParentedAndColoured = pieceNeedsToBeParentedAndColoured;
    }

    public PieceController getUnplacedPiece()
    {
        return unplacedPiece;
    }

    public bool getTurnNeedsToBeSwitched()
    {
        return turnNeedsToBeSwitched;
    }

    public void setTurnNeedsToBeSwitched(bool turnNeedsToBeSwitched)
    {
        this.turnNeedsToBeSwitched = turnNeedsToBeSwitched;
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

    public CellController getCellAtRow(int row)
    {
        return cellGrid[row];
    }

}
