using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncerHelper : MonoBehaviour
{
    [SerializeField]
    Vector2 direction;

    void OnTriggerEnter2D(Collider2D _col)
    {
        if(_col.CompareTag("Player"))
        {
            //Debug.Log("choque con " + col.tag);
            _col.GetComponent<Player>().Bounce(direction);
        }
    }
}
