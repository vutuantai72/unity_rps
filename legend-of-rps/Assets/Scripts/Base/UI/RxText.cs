
using UnityEngine.UI;

public class RxText<T> : RxUI<Text, T>
{
    protected override void ObservingObjectValueIssuance(T ObservationTargetIssueValue)
    {
        Applicable.text = DisplayFormatSpecified(ObservationTargetIssueValue);
    }
    protected virtual string DisplayFormatSpecified(T ObservationTargetIssueValue)
    {
        return ObservationTargetIssueValue.ToString();
    }
}
