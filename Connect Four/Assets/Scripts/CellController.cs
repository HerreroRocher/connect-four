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

    void setPiece(PieceController piece){
        this.piece = piece;
        occupied = true;
    }


    // public int getColumn()
    // {
    //     return column;
    // }


    // public bool getUnoccupied()
    // {
    //     return !occupied;
    // }

    // public void setOccupied(Color color, int belongsTo)
    // {
    //     // Debug.Log("Set Occupied at column " + (column + 1) + " row " + (row + 1));
    //     occupied = true;
    //     this.belongsTo = belongsTo;
    //     this.color = color;
    //     // Debug.Log("Color meant to place for player " + belongsTo + ": " + color);
    //     cellImage.color = color;

    // }

    // public int getBelongsTo()
    // {

    //     return belongsTo;
    // }

    // public void setWon()
    // {
    //     cellImage.color = new Color(0, 0.5f, 0, 1); ; // Directly test with one sprite

    // }




}
