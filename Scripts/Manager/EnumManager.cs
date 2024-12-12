using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.ComponentModel;

public class EnumManager : MonoBehaviour
{    
    internal enum idioms 
    {
        English, 
        Spanish,
        Japanese,
    };

    internal enum textIndicators
    {        
        CreditsStartsHere,
        CreditsEndsHere,  
        NewAreaStartsHere,      
    }
    
    internal enum scenes 
    {
        LogosScene,
        MainScreenScene,
        PinballScene,
        FaceBuilderScene,
        LoseScene,
        WinScene,
    };

    internal enum audios
    {
        //music
        faceBuilder,
        mainMenu,
        pinball,
        rightFace,
        win,
        wrongFace,

        //Ambient
        waterfall,

        //SFX
        ballHit1,
        ballHit2,
        ballHit3,
        clicCancel,        
        clicConfirm,
        dash,
        frogSound1,
        frogSound2,
        frogSound3,
        frogSound4,
        frogSound5,
        frogSound6,
        gotItem,
        noYay,
        rockFall,
        rockLands,
        rockOnRock1,
        rockOnRock2,
        rockOnRock3,
        yay,              
    };

    internal enum tags
    {
        Player, 
        Obstacle,
        BoundaryEdge,
    };     

    internal enum text
    {
        firstIndications,        
        faceIndications,
        loseMessage,
        finishMessage,
        qualityOptions,
    };

    internal enum intType
    {
        fontIndex,
        languageIndex,
        qualityIndex,
        resolutionIndex,        
    };

    internal enum floatType
    {
        volume,
    }

    internal enum animParameters
    {
        tilt,
        fadeIn,
        fadeOut,
    }
    
}
