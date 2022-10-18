
using UnityEngine;
using UniRx;
using System;
using UniRx.Triggers;

public class RxContentList<PublicationType> : MonoBehaviour
{
    protected IReadOnlyReactiveCollection<PublicationType> objObservation;
    private void Start()
    {
        ObservationTargetDesignation();
        ObservationTargetSubscriptionStart();
    }

    protected void ObservationTargetSubscriptionStart()
    {
        objObservation
            .ObserveAdd()
            .Subscribe(elementAdd)
            .AddTo(this);

        objObservation
            .ObserveMove()
            .Subscribe(elementMove)
            .AddTo(this);

        objObservation
            .ObserveRemove()
            .Subscribe(elementRemove)
            .AddTo(this);

        objObservation
            .ObserveReplace()
            .Subscribe(elementReplace)
            .AddTo(this);
    }

    protected virtual void ObservationTargetDesignation()
    {

    }

    protected virtual void elementAdd(CollectionAddEvent<PublicationType> element)
    {

    }

    protected virtual void elementMove(CollectionMoveEvent<PublicationType> element)
    {

    }

    protected virtual void elementRemove(CollectionRemoveEvent<PublicationType> element)
    {

    }

    protected virtual void elementReplace(CollectionReplaceEvent<PublicationType> element)
    {

    }

}

