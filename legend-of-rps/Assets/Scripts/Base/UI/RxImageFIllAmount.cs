
using UnityEngine.UI;

public class RxImageFillAmount : RxImage<float>
{
    protected override void ObservingObjectValueIssuance(float val)
    {
        Applicable.fillAmount = val;
    }
}
