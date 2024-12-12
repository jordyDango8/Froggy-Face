using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHelper : MonoBehaviour
{
    #region variables
    internal delegate void GotFacePartDelegate();
    internal static event GotFacePartDelegate GotFacePartEvent;

    [SerializeField]
    GameManager gameManager;
    
    [SerializeField ]
    AudioManager audioManager;

    [SerializeField]
    PlayerDataManager playerDataManager;

    [SerializeField]
    SpriteRenderer spriteRenderer;
    
    [SerializeField]
    Vector2 newSize;
    
    [SerializeField]
    Sprite myOriginalSprite;
    
    [SerializeField]
    Sprite myEditedSprite;  

    [SerializeField]
    Collider2D myCol;

    [SerializeField]
    Vector2 originalSpriteRendererSize = new Vector2(1, 1);
    #endregion
    

    void Start()
    {
        AdjustSpriteRendererSize();
    }

    void OnTriggerEnter2D(Collider2D _col)
    {
        if(_col.CompareTag("Player"))
        {
            //Debug.Log("player entered in " + gameObject.name);
            GotFacePart();            
        }
    }

    void Assignments()
    {
        gameManager = GameManager.gameManager;
        audioManager = AudioManager.audioManager;
        playerDataManager = PlayerDataManager.playerDataManager;

        spriteRenderer = GetComponent<SpriteRenderer>(); // needs to be here because
        myCol = this.GetComponent<Collider2D>();
    }

    void AdjustSpriteRendererSize()
    {        
        newSize = new Vector2(0.9f, 0.9f);
        if(spriteRenderer.sprite.rect.width / spriteRenderer.sprite.rect.height > 2)
        {
            //Debug.Log("1:2");
            newSize = new Vector2(0.9f, 0.45f);
        }
        if(spriteRenderer.sprite.rect.width / spriteRenderer.sprite.rect.height > 3)
        {
            //Debug.Log("1:4");
            newSize = new Vector2(0.9f, 0.3f);
        }
        spriteRenderer.size = newSize;
    }

    internal void AssignSprites(Sprite _newOriginalSprite, Sprite _newEditedSprite)
    {
        myOriginalSprite = _newOriginalSprite;
        myEditedSprite = _newEditedSprite;
    }

    internal void ChangeSprite(Sprite _newSprite)
    {
        //Debug.Log("changeSprite");
        spriteRenderer.sprite = _newSprite;
    }

    void GotFacePart()
    {
        audioManager.Play(EnumManager.audios.gotItem);        
        playerDataManager.GotFacePart(myOriginalSprite, myEditedSprite);
        gameManager.SetFacePartIndex(gameManager.GetFacePartIndex() + 1);          
        if(GotFacePartEvent != null)
        {            
            GotFacePartEvent();
        }
        myCol.enabled = false;        
    }

    void OnEnable()
    {
        Assignments();
    }
}

