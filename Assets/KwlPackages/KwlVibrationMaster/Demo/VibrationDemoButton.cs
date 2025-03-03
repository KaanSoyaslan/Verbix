using Lofelt.NiceVibrations;
using UnityEngine;

public class VibrationDemoButton : MonoBehaviour
{
    [SerializeField] private bool playVibration;

    void Update()
    {
        if (playVibration)
        {
            playVibration = false;
            EmitTestEvent();
        }
    }
    private void EmitTestEvent()
    {
        KwlVibrationMaster.Instance.TriggerVibration(HapticPatterns.PresetType.Success);
    }
}