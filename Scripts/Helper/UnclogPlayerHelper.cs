using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnclogPlayerHellper : MonoBehaviour
{
    AudioManager audioManager;
    Rigidbody2D rb;
    RockBehaviour rockBehaviour;

    float collisionTimeToPush = 0.0f;
    float pushCooldown = 1.5f;
    float collisionTimeToPass = 0.0f;
    float passCooldown = 2.0f;

    float pushForceMin = 20.0f;
    float pushForceMax = 30.0f;
    float pushForce;

    bool stuckB = false;

    void Start()
    {
        audioManager = AudioManager.audioManager;
        rb = GetComponentInParent<Rigidbody2D>();
        SetPushForce(Random.Range(pushForceMin, pushForceMax));
    }

    void Update()
    {
        if(stuckB)
        {
            collisionTimeToPush += Time.deltaTime;            
            collisionTimeToPass += Time.deltaTime;            
            if(collisionTimeToPush > pushCooldown)
            {
                collisionTimeToPush = 0;                
                Push(); 
            }
            if(collisionTimeToPass > passCooldown)
            {
                collisionTimeToPass = 0;                
                Unclog();               
            }
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log("collided with= " + col.name);
        if(col.CompareTag(EnumManager.tags.Obstacle.ToString()))
        {
            stuckB = true;
            rockBehaviour = col.GetComponent<RockBehaviour>();
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        //Debug.Log("stopped colliding with= " + col.name);
        if(col.CompareTag(EnumManager.tags.Obstacle.ToString()))
        {
            stuckB = false;
            collisionTimeToPush = 0;
            collisionTimeToPass = 0;
            SetPushForce(Random.Range(pushForceMin, pushForceMax));
        }
    }

    void Unclog()
    {
        //Debug.Log("unclog");
        rockBehaviour.UnclogPlayer();
    }

    void SetPushForce(float _newValue)
    {
        if(Random.Range(0, 2) == 0)
        {
            pushForce = _newValue;
        }
        else
        {
            pushForce = -_newValue;
        }
    }

    void Push()
    {
        //Debug.Log("push " + pushForce);   
        audioManager.Play(EnumManager.audios.noYay);
        rb.AddForce(new Vector2(pushForce, 0.0f));
    }
}
