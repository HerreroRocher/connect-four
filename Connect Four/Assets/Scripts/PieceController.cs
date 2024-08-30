using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class PieceController : MonoBehaviour
{

    private bool hasLanded = false;


    public void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Cell") && !hasLanded)
        {
            hasLanded = true;
            Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
            rigidbody.velocity = Vector2.zero;
            rigidbody.isKinematic = true;

        }

    }


}
