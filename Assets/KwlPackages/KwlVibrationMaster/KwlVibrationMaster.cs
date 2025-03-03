using UnityEngine;
using Lofelt.NiceVibrations;
public class KwlVibrationMaster : MonoBehaviour
{
    public static KwlVibrationMaster Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    public void TriggerVibration(HapticPatterns.PresetType type)
    {
        HapticPatterns.PlayPreset(type);
    }
}