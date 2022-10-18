
using UnityEngine.UI;
using UniRx;


public class InputFieldEndEdit : RxInputField
{

    protected override void ObservationTargetDesignation()
    {
        objectObservation = Applicable.OnEndEditAsObservable();
    }

    protected override void ObservingObjectValueIssuance(string val)
    {
        editingFinished(val);
    }

    protected virtual void editingFinished(string value)
    {

    }

}

