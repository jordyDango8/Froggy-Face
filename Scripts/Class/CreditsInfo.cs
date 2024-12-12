using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CreditsInfo
{       
    [SerializeField]
    internal string area;

    [SerializeField]
    internal string name;
    
    [SerializeField]
    internal string link;

    internal CreditsInfo(string _area, string _name, string _link)
    {
        this.area = _area;
        this.name = _name;
        this.link = _link;
    }
}
