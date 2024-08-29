using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CellController : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update

    private bool occupied;
    private Image cellImage;
    private int column;
    private int row;

    private bool unattendedCheck;

    private int belongsTo;

    private Color color;

    public void Start()
    {
        // Debug.Log("Cell created");
        occupied = false;
        belongsTo = -1;
        cellImage = GetComponent<Image>();
        unattendedCheck = false;
    }

    public void OnPointerDown(PointerEventData pointer)
    {
        // Debug.Log("Cell clicked");
        // Debug.Log("Clicked on cell in column " + (column + 1) + " row " + (row + 1));
        unattendedCheck = true;
    }


    public void setCoords(int column, int row)
    {
        this.column = column;
        this.row = row;

    }

    public bool getUnattendedCheck()
    {
        return unattendedCheck;
    }

    public int getColumn()
    {
        return column;
    }

    public void setCheckAttended()
    {
        unattendedCheck = false;
    }

    public bool getUnoccupied()
    {
        return !occupied;
    }

    public void setOccupied(Color color, int belongsTo)
    {
        // Debug.Log("Set Occupied at column " + (column + 1) + " row " + (row + 1));
        occupied = true;
        this.belongsTo = belongsTo;
        this.color = color;
        // Debug.Log("Color meant to place for player " + belongsTo + ": " + color);
        cellImage.color = color;
        
    }

    public int getBelongsTo()
    {

        return belongsTo;
    }

    public void setWon()
    {
        cellImage.color = new Color(0, 0.5f, 0, 1);; // Directly test with one sprite

    }




}
