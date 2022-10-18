using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RxSound : RxView<bool>
{

    protected override void ObservingObjectValueIssuance(bool val)
    {
        gameObject.GetComponent<AudioSource>().enabled = val;
    }
}
