using UnityEngine;
using TMPro;
using KwlEventBus;
using Lofelt.NiceVibrations;
using System.Collections.Generic;
using DG.Tweening;
using static StudyManager;

public class TestManager : Panel
{
    [SerializeField] private TMP_Text firstWordTXT;
    [SerializeField] private TMP_Text secondWordTXT;
    [SerializeField] private GameObject[] stars;
    [SerializeField] private GameObject deleteWordPanel;
    [SerializeField] private GameObject secretPanel;
    [SerializeField] private GameObject showBtnObj;

    private List<string> wordPairs;
    private int currentDataNum;
    private Tween scaleTween;
    private int nextBtnClickCount;
    private StudyType currentStudyType;

    private void PrepareWords()
    {
        nextBtnClickCount = 0;

        if (wordPairs == null)
            wordPairs = new List<string>();
        wordPairs.Clear();
        currentDataNum = 0;
        string rawData = PlayerPrefs.GetString("Words");
        string[] linedData = rawData.Split("%");

        for (int i = 0; i < linedData.Length - 1; i++)
        {
            wordPairs.Add(linedData[i].Trim());
        }
        if (wordPairs.Count == 0)
        {
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
        if (IsIdUnapproved(currentId))
        {
            ShowNextData();
            return;
        }
        else if (nextBtnClickCount == 0) //first time see
        {
            base.TogglePanel(true);
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
    private bool IsIdUnapproved(int id)
    {
        if (id == -1) return true; //removed word check

        bool isDailyInvalid = currentStudyType == StudyType.Daily && id >= 6;
        bool isLibraryInvalid = currentStudyType == StudyType.Library && id < 6;

        return isDailyInvalid || isLibraryInvalid;
    }
    public void ShowAns()
    {
        secretPanel.transform.localScale = Vector3.zero;
        secretPanel.SetActive(true);
        secretPanel.transform.DOScale(Vector3.one, 0.25f);
        showBtnObj.SetActive(false);
    }
    public void AnswereButton(bool isAnsTrue)
    {
        nextBtnClickCount++;
        secretPanel.SetActive(false);
        showBtnObj.SetActive(true);

        string rawData = PlayerPrefs.GetString("Words");
        string[] linedData = rawData.Split("%");
        string newData = "";
        for (int i = 0; i < linedData.Length - 1; i++)
        {
            if (i == currentDataNum)
            {
                string[] splittedData = linedData[i].Split("½");
                int currentWordData = int.Parse(splittedData[2]);

                int newWordData = isAnsTrue == true ? 
                    currentWordData + 1:
                    currentWordData - 1;

                newData += splittedData[0] + "½" + splittedData[1] + "½" + Mathf.Clamp(newWordData, 0, 6) + "%";

            }
            else
            {
                newData += linedData[i] + "%";

            }
        }
        PlayerPrefs.SetString("Words", newData);

        if (isAnsTrue)
        {
            KwlVibrationMaster.Instance.TriggerVibration(HapticPatterns.PresetType.MediumImpact);
        }
        else
        {
            KwlVibrationMaster.Instance.TriggerVibration(HapticPatterns.PresetType.HeavyImpact);
        }

        ShowNextData();
    }
    private void ShowNextData()
    {
        currentDataNum++;
        if (currentDataNum >= wordPairs.Count)
        {

            if (nextBtnClickCount == 0)//first data
            {
                KwlBus<SendWarningEvent>.NotifyListeners(new SendWarningEvent("There is no word to study!", Color.magenta, 1f));
                KwlVibrationMaster.Instance.TriggerVibration(HapticPatterns.PresetType.MediumImpact);
            }
            else
            {
                KwlBus<SendWarningEvent>.NotifyListeners(new SendWarningEvent("Done!", Color.green, 1f));
                KwlVibrationMaster.Instance.TriggerVibration(HapticPatterns.PresetType.Success);
                TogglePanel(false);
            }
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
                newData += splittedData[0] + "½" + splittedData[1] + "½-1%";

            }
            else
            {
                newData += linedData[i] + "%";

            }
        }
        PlayerPrefs.SetString("Words", newData);

        KwlBus<SendWarningEvent>.NotifyListeners(new SendWarningEvent("Word Pair Deleted!", Color.magenta, 1f));
        KwlVibrationMaster.Instance.TriggerVibration(HapticPatterns.PresetType.MediumImpact);

        secretPanel.SetActive(false);
        showBtnObj.SetActive(true);

        nextBtnClickCount++;
        ShowNextData();
        DeleteWordPanel(false);
    }
    public override void TogglePanel(bool isActive)
    {
        base.TogglePanel(isActive);
    }
    public void ActivatePanelRequest(int StudyTypeNum)
    {
        currentStudyType = (StudyType)StudyTypeNum;
        PrepareWords();

    }
 
}
