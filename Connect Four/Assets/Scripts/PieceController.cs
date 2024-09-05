using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PieceController : MonoBehaviour
{

    public GameObject BackgroundAndPiecesCanvas;
    private Rigidbody2D _rigidbody;
    private bool _hasStoppedMoving;
    private GameObject _parent;
    private Vector3 _parentCoordinates;
    private bool _isParentSet = false;
    private Vector3 _startPosition;
    private Vector3 _spawnPosition;
    private bool _isCanvasSwitched = false;
    private int _belongsTo;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        if (_rigidbody == null)
        {
            Debug.LogError("Rigidbody2D component is missing from this GameObject!");
        }
        else
        {
            // Debug.Log("Rigidbody initialised");
        }
        BackgroundAndPiecesCanvas = GameObject.FindGameObjectWithTag("Background and Pieces Canvas");

        _startPosition = transform.position;
    }

    private void switchCanvas()
    {

        if (BackgroundAndPiecesCanvas != null)
        {
            this.transform.SetParent(BackgroundAndPiecesCanvas.transform, false);
            gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(105, 105);


        }
    }

    private void StopPiece()
    {
        // Debug.Log("Collision detected");
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.gravityScale = 0;
        _rigidbody.angularVelocity = 0;
        _rigidbody.angularDrag = 0;
        _rigidbody.isKinematic = true;
        _hasStoppedMoving = true;
        SetPieceCoordinates(_parentCoordinates);
    }

    private void SetPieceCoordinates(Vector3 coordinates)
    {
        this.transform.position = coordinates;
    }

    private void Update()
    {
        // Debug.Log(transform.position);
        if (_isParentSet)
        {
            // Debug.Log("this.transform.position: " + this.transform.position);
            // Debug.Log("parentCoordinates " + parentCoordinates);
            // Debug.Log("rigidbody.velocity " + rigidbody.velocity);
            // Debug.Log("Vector2.zero " + Vector2.zero);
            if (this.transform.position.y <= _parentCoordinates.y && _rigidbody.velocity != Vector2.zero)
            {
                StopPiece();
            }
        }


        if (_startPosition != null)
        {
            if (!_isCanvasSwitched)
            {
                if (transform.position != _startPosition)
                {
                    _spawnPosition = transform.position;
                    switchCanvas();
                    transform.position = _spawnPosition;
                }
            }
        }
    }

    public void SetColor(Color color)
    {
        this.GetComponent<Image>().color = color;
    }

    public void SetBelongsTo(int belongsTo)
    {
        this._belongsTo = belongsTo;
    }

    public int GetBelongsTo()
    {
        return _belongsTo;
    }

    public void SetDynamic()
    {
        // Debug.Log(rigidbody);
        // Debug.Log(rigidbody == null);
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
    }

    public void SetParent(GameObject parent)
    {
        _isParentSet = true;
        this._parent = parent;
        _parentCoordinates = parent.transform.position;
    }

    public bool GetHasStoppedMoving()
    {
        return _hasStoppedMoving;
    }

}
