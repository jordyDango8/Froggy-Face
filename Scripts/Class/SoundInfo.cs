using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SoundInfo
{
    [SerializeField]
    internal AudioClip clip;

    //[SerializeField]
    internal string name;

    [SerializeField]
    internal bool playOnAwake;
    
    [SerializeField]
    internal bool loop;
    
    [SerializeField] [Range(0, 1)]
    internal float volume;
    
    [SerializeField]
    [Range(-3, 3)]
    internal float pitch;

    internal AudioSource source;
}
