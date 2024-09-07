using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PieceController : MonoBehaviour
{

    private GameObject _backgroundAndPiecesCanvas;
    private Rigidbody2D _rigidbody;
    private bool _hasStoppedMoving;
    private GameObject _parent;
    private Vector3 _parentCoordinates;
    private Vector3 _startPosition;
    private Vector3 _spawnPosition;
    private bool _isCanvasSwitched = false;
    private int _belongsTo;
    private ColumnController _columnController;
    private Vector2 _size;
    private GameBoardController _gameBoardController;
    private bool _switchTurnOnLanding = true;


    public void SetColumnController(ColumnController columnController)
    {
        _columnController = columnController;
    }

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
        _backgroundAndPiecesCanvas = GameObject.FindGameObjectWithTag("Background and Pieces Canvas");

        _startPosition = transform.position;
    }

    public void SetPieceSize(float pieceWidth)
    {
        _size = new Vector2(pieceWidth, pieceWidth);
        GetComponent<RectTransform>().sizeDelta = _size;
    }

    public void SetSwitchTurnOnLanding(bool switchTurnOnLanding)
    {
        _switchTurnOnLanding = switchTurnOnLanding;
    }
    private void switchCanvas()
    {

        if (_backgroundAndPiecesCanvas != null)
        {
            this.transform.SetParent(_backgroundAndPiecesCanvas.transform, false);
            gameObject.GetComponent<RectTransform>().sizeDelta = _size;


        }
    }

    private void StopPiece()
    {
        
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.gravityScale = 0;
        _rigidbody.angularVelocity = 0;
        _rigidbody.angularDrag = 0;
        _rigidbody.isKinematic = true;
        _hasStoppedMoving = true;
        SetPieceCoordinates(_parentCoordinates);
        Debug.Log("Piece has landed at " + _parentCoordinates);
    }

    private void SetPieceCoordinates(Vector3 coordinates)
    {
        this.transform.position = coordinates;
    }

    private void Update()
    {
        // Debug.Log(transform.position);
        if (_parent)
        {
            // Debug.Log("this.transform.position: " + this.transform.position);
            // Debug.Log("parentCoordinates " + parentCoordinates);
            // Debug.Log("rigidbody.velocity " + rigidbody.velocity);
            // Debug.Log("Vector2.zero " + Vector2.zero);
            if (this.transform.position.y <= _parentCoordinates.y && _rigidbody.velocity != Vector2.zero)
            {
                StopPiece();
                _gameBoardController.SetIsWaitingForPieceToLand(false);
                _parent.GetComponent<CellController>().SetPiece(this);
                if (_switchTurnOnLanding)
                {
                    _gameBoardController.CheckIfGameWon();
                    _gameBoardController.SwitchTurns();
                    _gameBoardController.CreatePieceInColumnWhichHoveringOver();
                }
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
        _rigidbody.gravityScale = 400;

    }

    public void SetParent(GameObject parent)
    {
        this._parent = parent;
        _parentCoordinates = parent.transform.position;
    }

    public bool GetHasStoppedMoving()
    {
        return _hasStoppedMoving;
    }

    public void DropPiece()
    {
        Debug.Log("Piece is dropped.");
        SetDynamic();
        _gameBoardController.SetIsWaitingForPieceToLand(true);
    }

    public void SetGameBoardController(GameBoardController gameBoardController)
    {
        _gameBoardController = gameBoardController;
    }
}
