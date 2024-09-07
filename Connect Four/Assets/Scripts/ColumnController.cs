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
    private PieceController _unplacedPiece;
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

    public void DropPieceIfExists()
    {
        // Debug.Log("Cell clicked");
        // Debug.Log("Clicked on cell in column " + column);
        if (_unplacedPiece != null && !_gameBoardController.GetIsGameOver())
        {
            // Debug.Log("Piece is dropped.");
            _unplacedPiece.SetParent(_cellGrid[_bottomCellIndex].gameObject);
            _unplacedPiece.SetDynamic();
            _gameBoardController.SetIsWaitingForPieceToLand(true);
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHovering = true;
        CreatePieceIfHovering();
    }

    public void CreatePiece()
    {
        // Debug.Log("isWaitingForPieceToLand " + isWaitingForPieceToLand);
        if (!_gameBoardController.GetIsWaitingForPieceToLand() && !_gameBoardController.GetIsGameOver())
        {
            _unplacedPiece = Instantiate(PiecePrefab, transform.position + new Vector3(0, 5, 0), Quaternion.identity, transform).GetComponent<PieceController>();
            _unplacedPiece.SetPieceSize(_pieceWidth);
            _unplacedPiece.SetColumnController(this);
            _gameBoardController.SetPieceOwnerAndColor(_unplacedPiece);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {

        _isHovering = false;
        DestroyPiece();

    }

    private void DestroyPiece()
    {
        if (_unplacedPiece != null && !_gameBoardController.GetIsWaitingForPieceToLand())
        {
            Destroy(_unplacedPiece.gameObject);
            _unplacedPiece = null;
        }
    }

    public void SetGameOver()
    {
        if (_unplacedPiece)
        {
            DestroyPiece();
        }
    }

    public void AcknowledgePieceHasStopped()
    {
        _cellGrid[_bottomCellIndex].SetPiece(_unplacedPiece);
        _unplacedPiece = null;
        _bottomCellIndex += 1;
        _gameBoardController.SetIsWaitingForPieceToLand(false);
        _gameBoardController.CheckIfGameWon();
        _gameBoardController.SwitchTurns();
        _gameBoardController.CreatePieceInColumnWhichHoveringOver();

    }

    public CellController GetCellAtRow(int row)
    {
        return _cellGrid[row];
    }

    public void CreatePieceIfHovering()
    {
        if (_isHovering && _bottomCellIndex < _cellGrid.Length && !_gameBoardController.GetIsTakingOver())
        {
            CreatePiece();
        }
    }

}
