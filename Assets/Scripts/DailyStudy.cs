using UnityEngine;
using TMPro;
using KwlEventBus;
using Lofelt.NiceVibrations;
using System.Collections.Generic;
using DG.Tweening;

public class DailyStudy : Panel
{
    [SerializeField] private TMP_Text firstWordTXT;
    [SerializeField] private TMP_Text secondWordTXT;
    [SerializeField] private GameObject[] stars;
    [SerializeField] private GameObject deleteWordPanel;

    private List<string> wordPairs;
    private int currentDataNum;
    private Tween scaleTween;
    private void PrepareWords()
    {
        if (wordPairs == null)
            wordPairs = new List<string>();
        wordPairs.Clear();
        currentDataNum = 0;
        string rawData = PlayerPrefs.GetString("Words");
        string[] linedData = rawData.Split("%");
     
        foreach (string words in linedData)
        {
            wordPairs.Add(words.Trim());
        }
        if (wordPairs.Count == 0)
        {
            TogglePanel(false);
            KwlBus<SendWarningEvent>.NotifyListeners(new SendWarningEvent("There is no word to study!", Color.magenta, 1f));
            KwlVibrationMaster.Instance.TriggerVibration(HapticPatterns.PresetType.MediumImpact);
        }
        else
        {
            ShowData();
        }
    }
    private void ShowData()
    {       
        string[] splittedData = wordPairs[currentDataNum].Split("½");
        int currentId = int.Parse(splittedData[2]);
        if (currentId == -1 || currentId >= 6) //removed data
        {
            ShowNextData();
            return;
        }
        for (int i = 0; i < stars.Length; i++)
        {
            stars[i].SetActive(false);
        }

        firstWordTXT.text = splittedData[0];
        secondWordTXT.text = splittedData[1];

        int starCount = currentId / 2;
        for (int i = 0; i < starCount; i++)
        {
            stars[i].SetActive(true);
        }
    }
    public void ShowNextData()
    {
        currentDataNum++;
        if (currentDataNum >= wordPairs.Count-1)
        {
            KwlBus<SendWarningEvent>.NotifyListeners(new SendWarningEvent("Done!", Color.green, 1f));
            KwlVibrationMaster.Instance.TriggerVibration(HapticPatterns.PresetType.Success);
            TogglePanel(false);
        }
        else
        {
            ShowData();
        }
    }
    public void DeleteWordPanel(bool isActivete)
    {
        deleteWordPanel.SetActive(isActivete);

        KwlVibrationMaster.Instance.TriggerVibration(HapticPatterns.PresetType.LightImpact);
        scaleTween.Kill(true);
        if (isActivete)
        {
            deleteWordPanel.SetActive(true);
            scaleTween = deleteWordPanel.transform.DOScale(Vector3.one, 0.25f);
        }
        else
        {
            scaleTween = deleteWordPanel.transform.DOScale(Vector3.zero, 0.25f).OnComplete(() => deleteWordPanel.SetActive(false));
        }
    }

    public void DeleteWord()
    {
        string rawData = PlayerPrefs.GetString("Words");
        string[] linedData = rawData.Split("%");
        string newData = "";
        for (int i = 0; i < linedData.Length - 1; i++)
        {
            if (i == currentDataNum)
            {
                string[] splittedData = linedData[i].Split("½");
                newData += splittedData[0] +"½" + splittedData[1] + "½-1%";

            }
            else
            {
                newData += linedData[i] + "%";

            }
        }
        PlayerPrefs.SetString("Words", newData);

        KwlBus<SendWarningEvent>.NotifyListeners(new SendWarningEvent("Word Pair Deleted!", Color.magenta, 1f));
        KwlVibrationMaster.Instance.TriggerVibration(HapticPatterns.PresetType.MediumImpact);

        ShowNextData();
        DeleteWordPanel(false);
    }
    public override void TogglePanel(bool isActive)
    {
        if (isActive)
        {
            PrepareWords();
        }
        base.TogglePanel(isActive);
    }
}
