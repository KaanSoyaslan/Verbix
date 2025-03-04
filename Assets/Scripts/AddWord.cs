using UnityEngine;
using TMPro;
using KwlEventBus;
using Lofelt.NiceVibrations;
using static ButtonFeeling;

public class AddWord : Panel
{
    [SerializeField] private TMP_InputField firstIF;
    [SerializeField] private TMP_InputField secondIF;
    [SerializeField] private Transform addBtnObj;


    public void AddNewWord()
    {
        if (firstIF.text.Contains("½") || secondIF.text.Contains("½")|| firstIF.text.Contains("%") || secondIF.text.Contains("%"))
        {
            KwlBus<SendWarningEvent>.NotifyListeners(new SendWarningEvent("'½' and '%' are not allowed in words", Color.red));
            KwlVibrationMaster.Instance.TriggerVibration(HapticPatterns.PresetType.HeavyImpact);

            KwlBus<ButtonFeelingEvent>.NotifyListeners(new ButtonFeelingEvent(addBtnObj, BtnTweenType.rotate));

        }
        else if (string.IsNullOrWhiteSpace(firstIF.text) || string.IsNullOrWhiteSpace(secondIF.text))
        {
            KwlBus<SendWarningEvent>.NotifyListeners(new SendWarningEvent("Fields cannot be empty", Color.red));
            KwlVibrationMaster.Instance.TriggerVibration(HapticPatterns.PresetType.HeavyImpact);

            KwlBus<ButtonFeelingEvent>.NotifyListeners(new ButtonFeelingEvent(addBtnObj, BtnTweenType.rotate));

        }
        else
        {
            KwlBus<SendWarningEvent>.NotifyListeners(new SendWarningEvent("Word Saved", Color.green));
            KwlVibrationMaster.Instance.TriggerVibration(HapticPatterns.PresetType.Success);

            KwlBus<ButtonFeelingEvent>.NotifyListeners(new ButtonFeelingEvent(addBtnObj, BtnTweenType.punch));


            string words = PlayerPrefs.GetString("Words");
            words += firstIF.text + "½" + secondIF.text + "½0%";
            PlayerPrefs.SetString("Words", words);

            firstIF.text = "";
            secondIF.text = "";
        }
    }
    public override void TogglePanel(bool isActive)
    {
        base.TogglePanel(isActive);
    }




}
