using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ColumnController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject CellPrefab;
    public GameObject PiecePrefab;


    private CellController[] _cellGrid;
    private PieceController _hoveringPiece;
    private int _bottomCellIndex = 0;
    private GameBoardController _gameBoardController;
    private bool _isHovering = false;
    private float _pieceWidth;


    public void InstantiateCells(int rows, float cellWidth)
    {
        _pieceWidth = cellWidth;
        GetComponent<GridLayoutGroup>().cellSize = new Vector2(cellWidth, cellWidth);
        _cellGrid = new CellController[rows];
        // Debug.Log("Column created");
        for (int cellRowNo = 0; cellRowNo < rows; cellRowNo++)
        {
            _cellGrid[cellRowNo] = Instantiate(CellPrefab, transform).GetComponent<CellController>();
            _cellGrid[cellRowNo].SetGameBoardController(_gameBoardController);
            _cellGrid[cellRowNo].SetColumnController(this);
        }
    }

    public void SetGameBoardController(GameBoardController gameBoardController)
    {
        _gameBoardController = gameBoardController;
    }

    public void DropHoveringPiece()
    {
        // Debug.Log("Cell clicked");
        // Debug.Log("Clicked on cell in column " + column);
        if (_hoveringPiece && !_gameBoardController.GetIsGameOver())
        {
            _hoveringPiece.DropPiece();
            _hoveringPiece.SetParent(GetBottomCell().gameObject);
            _hoveringPiece = null;
            _bottomCellIndex += 1;



        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHovering = true;
        CreateHoveringPiece();
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        _isHovering = false;
        DestroyPiece();

    }

    private void DestroyPiece()
    {
        if (_hoveringPiece && !_gameBoardController.GetIsWaitingForPieceToLand())
        {
            Destroy(_hoveringPiece.gameObject);
            _hoveringPiece = null;
        }
    }

    public void SetGameOver()
    {
        if (_hoveringPiece)
        {
            DestroyPiece();
        }
    }

    public CellController GetCellAtRow(int row)
    {
        return _cellGrid[row];
    }

    public void CreateHoveringPiece()
    {
        if (_isHovering && _bottomCellIndex < _cellGrid.Length && !_gameBoardController.GetIsTakingOver() && !_gameBoardController.GetIsEliminatingPiece())
        {
            // Debug.Log("isWaitingForPieceToLand " + isWaitingForPieceToLand);
            if (!_gameBoardController.GetIsWaitingForPieceToLand() && !_gameBoardController.GetIsGameOver())
            {
                _hoveringPiece = Instantiate(PiecePrefab, transform.position + new Vector3(0, 5, 0), Quaternion.identity, transform).GetComponent<PieceController>();
                _hoveringPiece.SetPieceSize(_pieceWidth);
                _hoveringPiece.SetColumnController(this);
                _hoveringPiece.SetGameBoardController(_gameBoardController);
                _gameBoardController.SetPieceOwnerAndColor(_hoveringPiece);
            }
        }
    }

    public CellController GetBottomCell()
    {
        return _cellGrid[_bottomCellIndex];
    }

    public void DropPiecesAboveEmptySpot()
    {
        bool emptyCellFound = false;
        for (int rowNo = 0; rowNo < _cellGrid.Length; rowNo++)
        {
            if (emptyCellFound && _cellGrid[rowNo].GetPiece())
            {
                PieceController pieceToDrop = _cellGrid[rowNo].GetPiece();
                pieceToDrop.SetParent(_cellGrid[rowNo - 1].gameObject);
                Debug.Log("Parent coordinates: " + _cellGrid[rowNo - 1].gameObject.transform.position);
                pieceToDrop.DropPiece();

                if (_cellGrid[rowNo + 1].GetPiece() && rowNo != _cellGrid.Length - 1)
                {
                    pieceToDrop.SetSwitchTurnOnLanding(false);
                } else {
                    _bottomCellIndex = rowNo;
                }

            }
            else
            {
                if (!_cellGrid[rowNo].GetPiece())
                {
                    emptyCellFound = true;
                }
            }
        }
    }
}
