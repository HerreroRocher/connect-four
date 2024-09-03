using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CellController : MonoBehaviour
{
    // Start is called before the first frame update

    private bool occupied = false;
    private PieceController piece;
    private BoxCollider2D boxCollider;

    void Start()
    {
        // Debug.Log("Cell created");
        boxCollider = GetComponent<BoxCollider2D>();

    }

    public void SetPiece(PieceController piece)
    {
        this.piece = piece;
        occupied = true;
    }

    public PieceController GetPiece()
    {
        return piece;
    }

    public bool GetOccupied()
    {
        return occupied;
    }


    public void SetWon()
    {
        piece.SetColour(new Color(0, 0.5f, 0, 1));
    }




}
