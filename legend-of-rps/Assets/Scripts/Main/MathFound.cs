using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MathFound : MonoBehaviour
{
    [SerializeField]
    private Button buttonAccept;
    [SerializeField]
    private Button buttonDeny;
    [SerializeField] private SkeletonGraphic matchFoundAnimation;
    //[SerializeField] private float matchFoundAnimationTimeToPlay;
    private void OnEnable()
    {
        this.buttonAccept.interactable = true;
        this.buttonDeny.interactable = true;
        matchFoundAnimation.gameObject.SetActive(true);
        matchFoundAnimation.AnimationState.SetAnimation(0, "match_found", false).TimeScale = 0.2f;

    }

}
