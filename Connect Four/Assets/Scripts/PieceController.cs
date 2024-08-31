using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PieceController : MonoBehaviour
{

    private Rigidbody2D rigidbody;
    private bool stoppedMoving;
    private GameObject parent;
    private Vector3 parentCoordinates;
    public GameObject backgroundAndPiecesCanvas;
    private bool parentSet = false;
    private Vector3 startPosition;
    private Vector3 spawnPosition;
    private bool canvasSwitched = false;
    private int belongsTo;

    public void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        backgroundAndPiecesCanvas = GameObject.FindGameObjectWithTag("Background and Pieces Canvas");

    }

    public void switchCanvas()
    {

        if (backgroundAndPiecesCanvas != null)
        {
            this.transform.SetParent(backgroundAndPiecesCanvas.transform, false);
            GetComponent<RectTransform>().sizeDelta = new Vector2(105, 105);

        }
    }

    public void setParent(GameObject parent)
    {
        parentSet = true;
        this.parent = parent;
        parentCoordinates = parent.transform.position;
    }

    void StopPiece()
    {
        // Debug.Log("Collision detected");
        rigidbody.velocity = Vector2.zero;
        rigidbody.gravityScale = 0;
        rigidbody.angularVelocity = 0;
        rigidbody.angularDrag = 0;
        rigidbody.isKinematic = true; // Optional
        stoppedMoving = true;
        SetPieceCoordinates(parentCoordinates);
    }

    public bool getStoppedMoving()
    {
        return stoppedMoving;
    }
    void SetPieceCoordinates(Vector3 coordinates)
    {
        this.transform.position = coordinates;
    }

    void Update()
    {
        // Debug.Log(transform.position);
        if (parentSet)
        {
            // Debug.Log("this.transform.position: " + this.transform.position);
            // Debug.Log("parentCoordinates " + parentCoordinates);
            // Debug.Log("rigidbody.velocity " + rigidbody.velocity);
            // Debug.Log("Vector2.zero " + Vector2.zero);
            if (this.transform.position.y <= parentCoordinates.y && rigidbody.velocity != Vector2.zero)
            {
                StopPiece();
            }
        }


        if (startPosition != null)
        {
            if (!canvasSwitched)
            {
                if (transform.position != startPosition)
                {
                    spawnPosition = transform.position;
                    switchCanvas();
                    transform.position = spawnPosition;
                }
            }
        }
    }

    public void setColour(Color colour)
    {
        this.GetComponent<Image>().color = colour;
    }

    public void setBelongsTo(int belongsTo)
    {
        this.belongsTo = belongsTo;
    }

    public int getBelongsTo()
    {
        return belongsTo;
    }


}
