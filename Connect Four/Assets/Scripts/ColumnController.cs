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
    private GameBoardController _gameBoardController;

    private bool _isHovering = false;


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
        _gameBoardController = gameBoardController;
    }

    public void OnPointerDown(PointerEventData pointer)
    {
        // Debug.Log("Cell clicked");
        // Debug.Log("Clicked on cell in column " + column);
        if (_unplacedPiece != null && !_gameBoardController.GetIsGameOver())
        {
            _unplacedPiece.SetParent(_cellGrid[_bottomCellIndex].gameObject);
            _unplacedPiece.SetDynamic();
            _gameBoardController.SetIsWaitingForPieceToLand(true);
        }

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _isHovering = true;
        if (_bottomCellIndex < _cellGrid.Length)
        {
            CreatePiece();
        }
    }

    public void CreatePiece()
    {
        // Debug.Log("isWaitingForPieceToLand " + isWaitingForPieceToLand);
        if (!_gameBoardController.GetIsWaitingForPieceToLand() && !_gameBoardController.GetIsGameOver())
        {
            _unplacedPiece = Instantiate(PiecePrefab, transform.position + new Vector3(0, 5, 0), Quaternion.identity, transform).GetComponent<PieceController>();
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
        _gameBoardController.CreatePieceInColumnWhichHoveringOver();

    }


    public CellController GetCellAtRow(int row)
    {
        return _cellGrid[row];
    }

    public void CreatePieceIfHovering()
    {
        if (_isHovering && _bottomCellIndex < _cellGrid.Length)
        {
            CreatePiece();
        }
    }

}
