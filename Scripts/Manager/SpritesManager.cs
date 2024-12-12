using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpritesManager : MonoBehaviour
{
    #region variables
    internal static SpritesManager spritesManager;

    [SerializeField]
    internal ImageInfo[] faces;
    
    [SerializeField]
    internal ImageInfo[] hair;
    
    [SerializeField]
    internal ImageInfo[] eyeBrows;
    
    [SerializeField]
    internal ImageInfo[] eyes;
    
    [SerializeField]
    internal ImageInfo[] nose;
    
    [SerializeField]
    internal ImageInfo[] nasolabialFold;
    
    [SerializeField]
    internal ImageInfo[] mouths;

    [SerializeField]
    internal Sprite[] rightFaceParts;
    #endregion

    void Awake()
    {
        if(spritesManager == null)
        {
            spritesManager = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
