using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CellController : MonoBehaviour, IPointerDownHandler
{
    // Start is called before the first frame update

    private string selection_status;
    private Image cellImage;
    public Sprite redSprite;
    public Sprite yellowSprite;
    public Sprite unoccupiedSprite;
    private int column;
    private int row;

    private bool unattendedCheck;

    public void Start()
    {
        Debug.Log("Cell created");
        selection_status = "unoccupied";
        cellImage = GetComponent<Image>();
        unattendedCheck = false;
    }

    public void OnPointerDown(PointerEventData pointer)
    {
        Debug.Log("Cell clicked");
        Debug.Log("Clicked on cell in column " + (column + 1) + " row " + (row + 1));
        //Shade bottom most unoccupied cell in column column
        if (selection_status == "unoccupied")
        {
            cellImage.sprite = redSprite; // Directly test with one sprite
            selection_status = "red";
        }
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

}
