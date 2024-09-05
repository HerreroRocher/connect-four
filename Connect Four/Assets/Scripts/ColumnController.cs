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
    private int _bottomCellIndex = 0;
    private bool _isHovering = false;
    private GameBoardController _gameBoard;

    public void InstantiateCells(int rows)
    {
        _cellGrid = new CellController[rows];
        // Debug.Log("Column created");
        for (int cellRowNo = 0; cellRowNo < rows; cellRowNo++)
        {
            _cellGrid[cellRowNo] = Instantiate(CellPrefab, transform).GetComponent<CellController>();
        }
    }

    public void SetGameBoardController(GameBoardController gameBoardController)
    {
        _gameBoard = gameBoardController;
    }

    public void OnPointerDown(PointerEventData pointer)
    {
        // Debug.Log("Cell clicked");
        // Debug.Log("Clicked on cell in column " + column);
        if (_unplacedPiece != null && !_gameBoard.GetIsGameOver())
        {
            _unplacedPiece.SetParent(_cellGrid[_bottomCellIndex].gameObject);
            _unplacedPiece.SetDynamic();
            _gameBoard.SetIsWaitingForPieceToLand(true);
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_bottomCellIndex < _cellGrid.Length)
        {
            _isHovering = true;
            CreatePiece();
        }
    }

    private void CreatePiece()
    {
        // Debug.Log("isWaitingForPieceToLand " + isWaitingForPieceToLand);
        if (!_gameBoard.GetIsWaitingForPieceToLand() && !_gameBoard.GetIsGameOver())
        {
            _unplacedPiece = Instantiate(PiecePrefab, transform.position + new Vector3(0, 5, 0), Quaternion.identity, transform).GetComponent<PieceController>();
            _gameBoard.SetPieceOwnerAndColor(_unplacedPiece);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _isHovering = false;
        DestroyPiece();

    }

    private void DestroyPiece()
    {
        if (_unplacedPiece != null && !_gameBoard.GetIsWaitingForPieceToLand())
        {
            Destroy(_unplacedPiece.gameObject);
            _unplacedPiece = null;
        }
    }

    private void Update()
    {
        if (_gameBoard.GetIsWaitingForPieceToLand() && _unplacedPiece && _cellGrid != null)
        {

            if (_unplacedPiece.GetHasStoppedMoving())
            {
                _cellGrid[_bottomCellIndex].SetPiece(_unplacedPiece);
                _unplacedPiece = null;
                _bottomCellIndex += 1;
                _gameBoard.SetIsWaitingForPieceToLand(false);
                _gameBoard.CheckIfGameWon();
            }

        }
        if (_isHovering && !_gameBoard.GetIsWaitingForPieceToLand() && _unplacedPiece == null && _bottomCellIndex < _cellGrid.Length)
        {
            CreatePiece();
        }
        if (_gameBoard.GetIsGameOver() && _unplacedPiece != null)
        {
            DestroyPiece();
        }
    }


    public CellController GetCellAtRow(int row)
    {
        return _cellGrid[row];
    }

}
