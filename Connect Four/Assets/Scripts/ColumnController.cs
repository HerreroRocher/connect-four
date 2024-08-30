using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ColumnController : MonoBehaviour
{

    private CellController[] cellGrid;

    public GameObject cellPrefab;
    private int rows;

    public void InstantiateCells()
    {
        Debug.Log("Column created");
        for (int cellNo = 0; cellNo < rows; cellNo++)
        {
            GameObject cellObj = Instantiate(cellPrefab, transform);
            cellGrid[cellNo] = cellObj.GetComponent<CellController>();
        }
    }

    public void setRows(int rows)
    {
        this.rows = rows;
        cellGrid = new CellController[rows];
        InstantiateCells();
    }


}
