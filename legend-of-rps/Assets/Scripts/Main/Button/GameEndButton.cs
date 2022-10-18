using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndButton : RxButton
{
    public override void OnClickAsync()
    {
        Application.Quit();
    }
}

