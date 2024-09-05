using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CellController : MonoBehaviour
{

    private PieceController _piece;

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




}
