using Lofelt.NiceVibrations;
using UnityEngine;
public class VibrationButton : MonoBehaviour
{
    [SerializeField] private GameObject vibrationClosedObj;
    void Start()
    {
        CheckVibrationStatus();
    }
    public void ToggleVibration()
    {
        bool isEnabled = PlayerPrefs.GetInt("HapticsDisabled") == 1;
        PlayerPrefs.SetInt("HapticsDisabled", isEnabled ? 0 : 1);
        CheckVibrationStatus();
    }
    private void CheckVibrationStatus()
    {
        bool isDisabled = PlayerPrefs.GetInt("HapticsDisabled", 0) == 1;
        HapticController.hapticsEnabled = !isDisabled;
        vibrationClosedObj.SetActive(isDisabled);
        Debug.Log(isDisabled ? "Vibration Disabled" : "Vibration Enabled");
    }
}