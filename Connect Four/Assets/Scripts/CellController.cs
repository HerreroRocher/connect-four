using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CellController : MonoBehaviour, IPointerDownHandler
{

    private PieceController _piece;
    private GameBoardController _gameBoardController;
    private ColumnController _columnController;

    public void SetPiece(PieceController piece)
    {
        this._piece = piece;
    }

    public PieceController GetPiece()
    {
        return _piece;
    }

    public bool GetIsOccupied()
    {
        return _piece != null;

    }

    public void SetWon()
    {
        _piece.SetColor(new Color(0, 0.5f, 0, 1));
    }

    public void OnPointerDown(PointerEventData pointer)
    {
        // Debug.Log("_gameBoardController == null is " + (_gameBoardController == null));

        if (_gameBoardController.GetIsTakingOver())
        {
            // Debug.Log("GameBoard line isn't issue");
            if (_piece != null)
            {
                _gameBoardController.SetPieceOwnerAndColor(_piece);
                _gameBoardController.SetIsTakingOver(false);
                _gameBoardController.CheckIfGameWon();
                _gameBoardController.SwitchTurns();
                _columnController.CreatePieceIfHovering();

            }
        }
        else
        {
            _columnController.DropPieceIfExists();
        }

    }

    public void SetGameBoardController(GameBoardController gameBoardController)
    {
        _gameBoardController = gameBoardController;
        // Debug.Log("_gameBoardController set");
        // Debug.Log("_gameBoardController == null is " + (_gameBoardController == null));

    }

    public void SetColumnController(ColumnController columnController)
    {
        _columnController = columnController;
        // Debug.Log("_gameBoardController set");
        // Debug.Log("_gameBoardController == null is " + (_gameBoardController == null));

    }

}
