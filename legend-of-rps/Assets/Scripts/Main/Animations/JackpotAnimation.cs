using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JackpotAnimation : RxAnimationPlay
{
    protected override void ObservationTargetDesignation()
    {
        if (gameObject != null)
            ObservingObjectValueIssuance("PlayAnim");
    }
}
