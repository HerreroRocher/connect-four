using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{

    public int columns = 7;
    public int rows = 6;
    public GameObject cellPrefab; // Reference to the cell prefab



    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Start method called");

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                Instantiate(cellPrefab, new Vector3(x, y, 0), Quaternion.identity, transform);
            }
        }


    }

    // Update is called once per frame
    void Update()
    {

    }
}
