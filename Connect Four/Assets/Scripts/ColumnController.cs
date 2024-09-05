using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ColumnController : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject CellPrefab;
    public GameObject PiecePrefab;
    private CellController[] _cellGrid;
    private PieceController _unplacedPiece;
    private int _rows;
    private int _column;
    private int _bottomCellIndex = 0;
    private bool _pieceNeedsParentingAndColoring = false;
    private bool _isWaitingForPieceToLand = false;
    private bool _isGameOver = false;
    private bool _isHovering = false;
    private GameBoardController _gameBoardController;

    private void InstantiateCells()
    {
        // Debug.Log("Column created");
        for (int cellRowNo = 0; cellRowNo < _rows; cellRowNo++)
        {
            _cellGrid[cellRowNo] = Instantiate(CellPrefab, transform).GetComponent<CellController>();
        }
    }

    public void SetGameBoardController(GameBoardController gameBoardController){
        _gameBoardController = gameBoardController;
    }

    public void OnPointerDown(PointerEventData pointer)
    {
        // Debug.Log("Cell clicked");
        // Debug.Log("Clicked on cell in column " + column);
        if (_unplacedPiece != null && !_isGameOver)
        {
            _unplacedPiece.SetParent(_cellGrid[_bottomCellIndex].gameObject);
            _unplacedPiece.SetDynamic();
            _isWaitingForPieceToLand = true;
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHovering = true;
        CreatePiece();
    }

    private void CreatePiece()
    {
        // Debug.Log("isWaitingForPieceToLand " + isWaitingForPieceToLand);
        if (!_isWaitingForPieceToLand && !_isGameOver)
        {
            _unplacedPiece = Instantiate(PiecePrefab, transform.position + new Vector3(0, 5, 0), Quaternion.identity, transform).GetComponent<PieceController>();
            _pieceNeedsParentingAndColoring = true;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHovering = false;
        DestroyPiece();

    }

    private void DestroyPiece()
    {
        if (_unplacedPiece != null && !_isWaitingForPieceToLand)
        {
            Destroy(_unplacedPiece.gameObject);
            _unplacedPiece = null;
        }
    }

    private void Update()
    {
        if (_isWaitingForPieceToLand && _unplacedPiece != null)
        {

            if (_unplacedPiece.GetHasStoppedMoving())
            {
                GetBottomCell().SetPiece(_unplacedPiece);
                _unplacedPiece = null;
                _bottomCellIndex += 1;
                _isWaitingForPieceToLand = false;
                _gameBoardController.CheckIfGameWon()


            }

        }
        if (_isHovering && !_isWaitingForPieceToLand && _unplacedPiece == null)
        {
            CreatePiece();
        }
        if (_isGameOver && _unplacedPiece != null)
        {
            DestroyPiece();
        }
    }

    private CellController GetBottomCell()
    {
        return _cellGrid[_bottomCellIndex];
    }

    public void SetIsGameOver(bool isGameOver)
    {
        this._isGameOver = isGameOver;
    }

    public bool GetIsWaitingForPieceToLand()
    {
        return _isWaitingForPieceToLand;
    }

    public void SetIsWaitingForPieceToLand(bool isWaitingForPieceToLand)
    {
        this._isWaitingForPieceToLand = isWaitingForPieceToLand;
    }

    public bool GetPieceNeedsParentingAndColoring()
    {
        return _pieceNeedsParentingAndColoring;
    }

    public void SetPieceNeedsParentingAndColoring(bool pieceNeedsParentingAndColoring)
    {
        this._pieceNeedsParentingAndColoring = pieceNeedsParentingAndColoring;
    }

    public PieceController GetUnplacedPiece()
    {
        return _unplacedPiece;
    }

    public void SetRows(int rows)
    {
        this._rows = rows;
        _cellGrid = new CellController[rows];
        InstantiateCells();
    }

    public void SetColumn(int column)
    {
        this._column = column;
    }

    public CellController GetCellAtRow(int row)
    {
        return _cellGrid[row];
    }

}
