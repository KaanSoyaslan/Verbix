using DG.Tweening;
using KwlEventBus;
using UnityEngine;

public class ButtonFeeling : MonoBehaviour
{
    private Tween punchTween;
    private Tween rotTween;

    private void OnEnable()
    {
        KwlBus<ButtonFeelingEvent>.AddListener(ButtonFeelingRequest);
    }
    private void OnDisable()
    {
        KwlBus<ButtonFeelingEvent>.RemoveListener(ButtonFeelingRequest);
    }

    private void ButtonFeelingRequest(ButtonFeelingEvent obj)
    {
        if (obj.type == BtnTweenType.rotate)
        {
            RotateObj(obj.obj);
        }
        else 
        {
            PunchObj(obj.obj);
        }

    }

    private void RotateObj(Transform obj)
    {
        rotTween.Kill(true);
        rotTween = obj.DOShakeRotation(0.2f, new Vector3(0, 0, 5f), 10, 90f, false);
    }

    private void PunchObj(Transform obj)
    {
        punchTween.Kill(true);
        punchTween = obj.DOPunchScale(Vector3.one * 0.2f, 0.2f, 0, 0);
    }
    public enum BtnTweenType
    {
        rotate,
        punch
    }
}
