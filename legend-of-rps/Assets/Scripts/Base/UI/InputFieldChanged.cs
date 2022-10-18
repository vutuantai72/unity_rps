using UniRx;

public class InputFieldChanged : RxInputField
{

    protected override void ObservationTargetDesignation()
    {
        objectObservation = Applicable.OnValueChangedAsObservable();
    }

    protected override void ObservingObjectValueIssuance(string value)
    {
        uponChange(value);
    }

    protected virtual void uponChange(string changeValue)
    {

    }
}

