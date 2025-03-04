using KwlEventBus;
using Lofelt.NiceVibrations;
using TMPro;
using UnityEngine;
using static ButtonFeeling;

public class DataManupulate : Panel
{
    [SerializeField] private TMP_InputField dataIF;
    [SerializeField] private Transform saveBtnObj;


    //word½word½0%"

    public void SaveData()
    {
        string readableData = dataIF.text;
        string rawData = readableData.Replace("\n", "%");


        string[] linedData = rawData.Split("%");
        for (int i = 0; i < linedData.Length-1; i++)
        {
            string[] splittedData = linedData[i].Split("½");

            if (splittedData.Length != 3)
            {
                //wrong data
                KwlBus<SendWarningEvent>.NotifyListeners(new SendWarningEvent("There is error in this line (data_lenght_error): " + linedData[i], Color.red, 3f));
                KwlVibrationMaster.Instance.TriggerVibration(HapticPatterns.PresetType.HeavyImpact);
                KwlBus<ButtonFeelingEvent>.NotifyListeners(new ButtonFeelingEvent(saveBtnObj, BtnTweenType.rotate));

                return;
            }
            else if (splittedData.Length == 3 && !int.TryParse(splittedData[2], out _))
            {
                //wrong data
                KwlBus<SendWarningEvent>.NotifyListeners(new SendWarningEvent("There is error in this line (data_type_error): " + linedData[i], Color.red, 3f));
                KwlVibrationMaster.Instance.TriggerVibration(HapticPatterns.PresetType.HeavyImpact);
                KwlBus<ButtonFeelingEvent>.NotifyListeners(new ButtonFeelingEvent(saveBtnObj, BtnTweenType.rotate));

                return;
            }
        }
        PlayerPrefs.SetString("Words", rawData);

        KwlBus<SendWarningEvent>.NotifyListeners(new SendWarningEvent("New data saved", Color.green));
        KwlVibrationMaster.Instance.TriggerVibration(HapticPatterns.PresetType.Success);
        KwlBus<ButtonFeelingEvent>.NotifyListeners(new ButtonFeelingEvent(saveBtnObj, BtnTweenType.punch));
        SetField();
    }
    public void ResetField()
    {
        SetField();
    }

    private void SetField()
    {
        string rawData = PlayerPrefs.GetString("Words");
        string[] linedData = rawData.Split("%");
        string readableData = "";
        for (int i = 0; i < linedData.Length-1; i++)
        {
            readableData += linedData[i]+"\n";
        }
        dataIF.text = readableData;
    }

    public override void TogglePanel(bool isActive)
    {
        if (isActive)
        {
            SetField();
        }
        base.TogglePanel(isActive);
    }

}
