using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CellController : MonoBehaviour
{
    // Start is called before the first frame update

    private bool _isOccupied = false;
    private PieceController _piece;
    private BoxCollider2D _boxCollider;

    private void Start()
    {
        // Debug.Log("Cell created");
        _boxCollider = GetComponent<BoxCollider2D>();

    }

    public void SetPiece(PieceController piece)
    {
        this._piece = piece;
        _isOccupied = true;
    }

    public PieceController GetPiece()
    {
        return _piece;
    }

    public bool GetIsOccupied()
    {
        return _isOccupied;
    }


    public void SetWon()
    {
        _piece.SetColor(new Color(0, 0.5f, 0, 1));
    }




}
