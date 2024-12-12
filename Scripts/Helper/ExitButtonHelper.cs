using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitButtonHelper : MonoBehaviour
{
    internal delegate void ExitDelegate();
    internal static event ExitDelegate ExitEvent;

    public void Exit()
    {
        if(ExitEvent != null)
        {
            ExitEvent();
        }
    }

}
