using UnityEngine;
using TMPro;
using KwlEventBus;
using Lofelt.NiceVibrations;

public class AddWord : MonoBehaviour
{

    [SerializeField] private TMP_InputField firstIF;
    [SerializeField] private TMP_InputField secondIF;
    [SerializeField] private GameObject panel;

    public void AddNewWord()
    {
        if (firstIF.text.Contains("½") || secondIF.text.Contains("½")|| firstIF.text.Contains("%") || secondIF.text.Contains("%"))
        {
            KwlBus<SendWarningEvent>.NotifyListeners(new SendWarningEvent("'½' and '%' are not allowed in words", Color.red));
            KwlVibrationMaster.Instance.TriggerVibration(HapticPatterns.PresetType.HeavyImpact);
        }
        else if (string.IsNullOrWhiteSpace(firstIF.text) || string.IsNullOrWhiteSpace(secondIF.text))
        {
            KwlBus<SendWarningEvent>.NotifyListeners(new SendWarningEvent("Fields cannot be empty", Color.red));
            KwlVibrationMaster.Instance.TriggerVibration(HapticPatterns.PresetType.HeavyImpact);
        }
        else
        {
            KwlBus<SendWarningEvent>.NotifyListeners(new SendWarningEvent("Word Saved", Color.green));
            KwlVibrationMaster.Instance.TriggerVibration(HapticPatterns.PresetType.Success);

            string words = PlayerPrefs.GetString("Words");
            words += firstIF.text + "½" + secondIF.text + "½0%";
            PlayerPrefs.SetString("Words", words);

            firstIF.text = "";
            secondIF.text = "";
        }
    }
    public void TogglePanel(bool OnOff)
    {
        panel.SetActive(OnOff);
    }
}
