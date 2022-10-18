
using UnityEngine.UI;
public class RxButtonInteractable : RxUI<Button, bool>
{

    protected override void ObservingObjectValueIssuance(bool value)
    {
        Applicable.interactable = value;
    }


}

