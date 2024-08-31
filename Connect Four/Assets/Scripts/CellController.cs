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
    private int column;
    private int row;
    public GameObject piecePrefab;
    private BoxCollider2D boxCollider;



    public void Start()
    {
        // Debug.Log("Cell created");
        boxCollider = GetComponent<BoxCollider2D>();

    }
    public void setRow(int row)
    {
        this.row = row;
    }

    public void setColumn(int column)
    {
        this.column = column;
    }

    public void setImage(Sprite sprite)
    {
        GetComponent<Image>().sprite = sprite;
    }

    public void setPiece(PieceController piece)
    {
        this.piece = piece;
        occupied = true;
    }

    public PieceController getPiece()
    {
        return piece;
    }


    // public int getColumn()
    // {
    //     return column;
    // }


    public bool getOccupied()
    {
        return occupied;
    }


    public void setWon()
    {
        piece.setColour(new Color(0, 0.5f, 0, 1));
    }




}
