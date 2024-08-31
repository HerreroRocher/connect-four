using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class ColumnController : MonoBehaviour, IPointerDownHandler
{

    private CellController[] cellGrid;

    public GameObject cellPrefab;
    public Sprite basePieceSprite;
    private int rows;
    private int column;

    public GameObject piecePrefab;

    private int bottomCellIndex = 0;

    public Transform targetCanvas;


    public void InstantiateCells()
    {
        // Debug.Log("Column created");
        for (int cellRowNo = 0; cellRowNo < rows + 1; cellRowNo++)
        {
            GameObject cellGameObj = Instantiate(cellPrefab, transform);
            CellController cellInstance = cellGameObj.GetComponent<CellController>();
            cellInstance.setRow(cellRowNo);
            cellInstance.setColumn(column);
            if (cellRowNo == 0)
            {
                cellInstance.setImage(basePieceSprite);
            }
            cellGrid[cellRowNo] = cellInstance;
        }
    }

    public void setRows(int rows)
    {
        this.rows = rows;
        cellGrid = new CellController[rows + 1];
        InstantiateCells();
    }

    public void setColumn(int column)
    {
        this.column = column;
    }

    public void OnPointerDown(PointerEventData pointer)
    {
        // Debug.Log("Cell clicked");
        // Debug.Log("Clicked on cell in column " + column);
        DropPiece();

    }

    public void DropPiece()
    {
        bottomCellIndex += 1;
        Vector3 spawnPosition = transform.position + new Vector3(0, 5, 0);
        PieceController piece = Instantiate(piecePrefab, spawnPosition, Quaternion.identity, transform).GetComponent<PieceController>();
        piece.setParent(cellGrid[bottomCellIndex].gameObject);
        getBottomCell().setPiece(piece);
    }

    public CellController getBottomCell(){
        return cellGrid[bottomCellIndex+1];
    }


}
