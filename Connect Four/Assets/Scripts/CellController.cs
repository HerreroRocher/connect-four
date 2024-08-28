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

    public void Start()
    {
        Debug.Log("Cell created");
        selection_status = "unoccupied";
        cellImage = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData pointer)
    {
        Debug.Log("Cell clicked");
        Debug.Log("Clicked on cell in column " + (column + 1) + " row " + (row + 1));
        if (selection_status == "unoccupied")
        {
            Debug.Log("SS is unoccupied");
            cellImage.sprite = redSprite; // Directly test with one sprite
            selection_status = "red";
        }
    }


    public void setCoords(int column, int row)
    {
        this.column = column;
        this.row = row;

    }
}
