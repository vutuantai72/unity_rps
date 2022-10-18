using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RxAnimationPlay : RxComponent<Animator, string>
{

    protected override void ObservingObjectValueIssuance(string IssueVal)
    {
        Applicable.Play(IssueVal);
    }
}
