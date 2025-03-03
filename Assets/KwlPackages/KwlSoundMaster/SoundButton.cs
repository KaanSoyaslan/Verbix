using UnityEngine;
public class SoundButton : MonoBehaviour
{
    [SerializeField] private GameObject soundClosedObj;
    private void Start()
    {
        CheckSoundStatus();
    }
    public void ToggleSound()
    {
        PlayerPrefs.SetInt("SoundMuteStatus", PlayerPrefs.GetInt("SoundMuteStatus") == 0 ? 1 : 0);
        CheckSoundStatus();
    }
    private void CheckSoundStatus()
    {
        bool isMuted = PlayerPrefs.GetInt("SoundMuteStatus", 1) == 0;

        soundClosedObj.SetActive(isMuted);
        AudioListener.pause = isMuted;
        Debug.Log(isMuted ? "Audio Muted" : "Audio Unmuted");

    }
}