using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ColumnController : MonoBehaviour
{

    private CellController[] cellGrid;

    public GameObject cellPrefab;
    public Sprite basePieceSprite;
    private int rows;
    private int column;

    public void InstantiateCells()
    {
        // Debug.Log("Column created");
        for (int cellRowNo = 0; cellRowNo < rows+1; cellRowNo++)
        {
            GameObject cellGameObj = Instantiate(cellPrefab, transform);
            CellController cellInstance = cellGameObj.GetComponent<CellController>();
            cellInstance.setRow(cellRowNo);
            cellInstance.setColumn(column);
            if (cellRowNo == 0){
                cellInstance.setImage(basePieceSprite);
            }
            cellGrid[cellRowNo] = cellInstance;
        }
    }

    public void setRows(int rows)
    {
        this.rows = rows;
        cellGrid = new CellController[rows+1];
        InstantiateCells();
    }

    public void setColumn(int column)
    {
        this.column = column;
    }


}
