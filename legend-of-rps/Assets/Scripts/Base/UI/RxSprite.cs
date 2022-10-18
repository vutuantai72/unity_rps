using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RxSprite : RxImage<Sprite>
{
    protected override void ObservingObjectValueIssuance(Sprite issueVal)
    {
        Applicable.sprite = issueVal;
    }
}