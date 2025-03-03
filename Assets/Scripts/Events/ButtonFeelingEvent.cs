using UnityEngine;
using static ButtonFeeling;

public readonly struct ButtonFeelingEvent
{
    public readonly Transform obj;
    public readonly BtnTweenType type;
    public ButtonFeelingEvent(Transform _obj, BtnTweenType _type)
    {
        obj = _obj;
        type = _type;
    }
}
