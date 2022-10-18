using UniRx.Triggers;
using UniRx;
using UnityEngine;
public class AnimationStateExit : RxComponent<Animator, AnimatorStateInfo>
{


    protected override void ObservationTargetDesignation()
    {
        objectObservation = Applicable.GetBehaviour<ObservableStateMachineTrigger>().OnStateExitAsObservable().Select(stateInfo => stateInfo.StateInfo);

    }

    protected virtual void finishedStatusInformation(AnimatorStateInfo statusInformation)
    {

    }
}
