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
            unplacedPiece.SetParent(cellGrid[bottomCellIndex].gameObject);
            unplacedPiece.SetDynamic();
            waitingForPieceToLand = true;
            turnNeedsToBeSwitched = true;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hovering = true;
        CreatePiece();
    }

    void CreatePiece()
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

            if (unplacedPiece.GetStoppedMoving())
            {
                GetBottomCell().SetPiece(unplacedPiece);
                unplacedPiece = null;
                bottomCellIndex += 1;
                waitingForPieceToLand = false;


            }

        }
        if (hovering && !waitingForPieceToLand && unplacedPiece == null)
        {
            CreatePiece();
        }
        if (gameOver && unplacedPiece != null)
        {
            DestroyPiece();
        }
    }

    CellController GetBottomCell()
    {
        return cellGrid[bottomCellIndex];
    }

    public void SetGameOver(bool gameOver)
    {
        this.gameOver = gameOver;
    }

    public bool GetWaitingForPieceToLand()
    {
        return waitingForPieceToLand;
    }

    public void SetWaitingForPieceToLand(bool waitingForPieceToLand)
    {
        this.waitingForPieceToLand = waitingForPieceToLand;
    }

    public bool GetPieceNeedsToBeParentedAndColoured()
    {
        return pieceNeedsToBeParentedAndColoured;
    }

    public void SetPieceNeedsToBeParentedAndColoured(bool pieceNeedsToBeParentedAndColoured)
    {
        this.pieceNeedsToBeParentedAndColoured = pieceNeedsToBeParentedAndColoured;
    }

    public PieceController GetUnplacedPiece()
    {
        return unplacedPiece;
    }

    public bool GetTurnNeedsToBeSwitched()
    {
        return turnNeedsToBeSwitched;
    }

    public void SetTurnNeedsToBeSwitched(bool turnNeedsToBeSwitched)
    {
        this.turnNeedsToBeSwitched = turnNeedsToBeSwitched;
    }

    public void SetRows(int rows)
    {
        this.rows = rows;
        cellGrid = new CellController[rows];
        InstantiateCells();
    }

    public void SetColumn(int column)
    {
        this.column = column;
    }

    public CellController GetCellAtRow(int row)
    {
        return cellGrid[row];
    }

}
