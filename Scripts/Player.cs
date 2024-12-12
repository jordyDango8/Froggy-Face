using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    internal delegate void RedirectDelegate();
    internal static event RedirectDelegate RedirectEvent;

    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    AudioManager audioManager;


    [SerializeField]
    Rigidbody2D rb;
    [SerializeField]
    float movementForce;
    [SerializeField]
    float gravity;
    [SerializeField]
    float bounceForce;
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
        gameManager = GameManager.gameManager;
        audioManager = AudioManager.audioManager;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
        Bounce(new Vector2(-bounceForce, 0));
        //rb.gravityScale = gravity * 20f; //for fast test
    }

    void OnCollisionEnter2D()
    {
        //Debug.Log("velocity= " + rb.velocity);
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
                    audioManager.Play(EnumManager.audios.ballHit1);
                    break;
                case 2:
                    audioManager.Play(EnumManager.audios.ballHit2);
                    break;
                case 3:
                    audioManager.Play(EnumManager.audios.ballHit3);
                    break;
                default:
                    Debug.Log("no hit");
                    break;

            }
        }
    }

    internal void Bounce(Vector2 _direction)
    {
        //Debug.Log("bounce");
        //rb.AddForce(direction);
        rb.velocity = _direction * bounceForce;
    }

    internal void SetBounceForce(float _newValue)
    {
        Debug.Log("setBounceForce");
        bounceForce = _newValue;
        
    }

    internal void ApplyGravity()
    {
        rb.gravityScale = gravity;
    }   

    public void MoveUp()
    {
        Move(new Vector2(0, movementForce * 2f));
    }

    public void MoveRight()
    {
        Move(new Vector2(movementForce, 0));
    }

    public void MoveDown()
    {
        Move(new Vector2(0, -movementForce * 2f));
    }

    public void MoveLeft()
    {
        Move(new Vector2(-movementForce, 0));
    }

    void Move(Vector2 _direction)
    {
        audioManager.Play(EnumManager.audios.dash);
        rb.AddForce(_direction);        
        if(RedirectEvent != null)
        {
            RedirectEvent();
        }
    }
}
