using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CellController : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update

    private bool occupied = false;
    private int column;
    private int row;
    public GameObject piecePrefab;

    public void DropPiece(Vector2 spawnPosition)
    {
        Instantiate(piecePrefab, spawnPosition, Quaternion.identity);
    }
    public void Start()
    {
        // Debug.Log("Cell created");
    }

    public void setRow(int row){
        this.row = row;
    }

    public void setColumn(int column){
        this.column = column;
    }

    public void setImage(Sprite sprite){
        GetComponent<Image>().sprite = sprite;
    }

    public void OnPointerDown(PointerEventData pointer)
    {
        // Debug.Log("Cell clicked");
        Debug.Log("Clicked on cell in column " + column + " row " + row);
        // unattendedCheck = true;
        // DropPiece();

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
