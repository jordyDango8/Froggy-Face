using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehaviour : MonoBehaviour
{    
    AudioManager audioManager;

    Collider2D mycollider2D;
    Rigidbody2D rb;

    float AddRigidbodyProbability = 8.0f;

    float fallForce = 3.0f;
    
    [SerializeField]
    int impactForce = 0;
    [SerializeField]
    float lowVelocity = 0.0f;
    [SerializeField]
    float mediumVelocity = 1.0f;
    [SerializeField]
    float highVelocity = 2.0f;

    void Start()
    {
        audioManager = AudioManager.audioManager;
        if(Random.Range (0, 100) < AddRigidbodyProbability)
        {
            gameObject.AddComponent<Rigidbody2D>();
            rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
        }
        else
        {
            rb = null;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag(EnumManager.tags.Player.ToString()) && rb != null)
        {
            audioManager.Play(EnumManager.audios.rockFall);
            rb.gravityScale = fallForce;
        }

        if(col.gameObject.CompareTag(EnumManager.tags.Player.ToString()) && rb != null)
        {
            //Debug.Log("velocity= " + (Mathf.Abs(rb.velocity.x)) + "," + (Mathf.Abs(rb.velocity.y)));
            if(Mathf.Abs(rb.velocity.x) > lowVelocity && Mathf.Abs(rb.velocity.x) < mediumVelocity 
            || Mathf.Abs(rb.velocity.y) > lowVelocity && Mathf.Abs(rb.velocity.y) < mediumVelocity)
            {
                impactForce = 1;
            }
            if(Mathf.Abs(rb.velocity.x) > mediumVelocity && Mathf.Abs(rb.velocity.x) < highVelocity 
            || Mathf.Abs(rb.velocity.y) > mediumVelocity && Mathf.Abs(rb.velocity.y) < highVelocity)
            {
                impactForce = 2;
            }
            if(Mathf.Abs(rb.velocity.x) > highVelocity || Mathf.Abs(rb.velocity.y) > highVelocity)
            {
                impactForce = 3;
            }
            //if(col.gameObject.CompareTag(EnumManager.tags.obstacle.ToString()))
            {
                switch(impactForce)
                {
                    case 1:
                        audioManager.Play(EnumManager.audios.rockOnRock1);
                        break;
                    case 2:
                        audioManager.Play(EnumManager.audios.rockOnRock2);
                        break;
                    case 3:
                        audioManager.Play(EnumManager.audios.rockOnRock3);
                        break;
                    default:
                        Debug.Log("no hit");
                        break;
                }
            }
        }

        if(col.gameObject.CompareTag(EnumManager.tags.BoundaryEdge.ToString()) && rb != null)
        {
            //Debug.Log("land");
            audioManager.Play(EnumManager.audios.rockLands);
        }
    }

    internal void ChangeSize(Vector3 _newSize)
    {
        transform.localScale = _newSize;
    }

    internal void ChangeRotation(Vector3 _newRotation)
    {
        transform.Rotate(_newRotation);
    }

    internal void ChangeSprite(Sprite newSprite)
    {
        GetComponent<SpriteRenderer>().sprite = newSprite;
    }

    internal void AddCollider()
    {
        mycollider2D = gameObject.AddComponent<PolygonCollider2D>();        
    }

    internal void UnclogPlayer()
    {
        StartCoroutine(UnclogPlayerCR());        
    }

    IEnumerator UnclogPlayerCR()
    {
        TurnCollider(false);
        yield return new WaitForSeconds(1);

        TurnCollider(true);
    }

    void TurnCollider(bool newState)
    {
        mycollider2D.enabled = newState;
    }
    
}
