using DG.Tweening;
using Lofelt.NiceVibrations;
using UnityEngine;

public class Panel : MonoBehaviour
{
    private Tween scaleTween;
    [SerializeField] private GameObject panel;

    public virtual void TogglePanel(bool isActive)
    {
        KwlVibrationMaster.Instance.TriggerVibration(HapticPatterns.PresetType.LightImpact);

        scaleTween.Kill(true);
        if (isActive)
        {
            panel.SetActive(true);
            scaleTween = panel.transform.DOScale(Vector3.one, 0.25f);
        }
        else
        {
            scaleTween = panel.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => panel.SetActive(false));
        }
    }
}
