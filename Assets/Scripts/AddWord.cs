using UnityEngine;
using TMPro;
using KwlEventBus;
using Lofelt.NiceVibrations;
using DG.Tweening;
using static ButtonFeeling;

public class AddWord : MonoBehaviour
{

    [SerializeField] private TMP_InputField firstIF;
    [SerializeField] private TMP_InputField secondIF;
    [SerializeField] private GameObject panel;
    [SerializeField] private Transform addBtnObj;

    private Tween scaleTween;

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
    public void TogglePanel(bool isActive)
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
