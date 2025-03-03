using System.Collections.Generic;
using UnityEngine;
public class KwlSoundMaster : MonoBehaviour
{
    public static KwlSoundMaster Instance { get; private set; }
    private Dictionary<string, AudioClip> soundDictionary;
    private AudioSource audioSrc;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        audioSrc = gameObject.AddComponent<AudioSource>();
        soundDictionary = new Dictionary<string, AudioClip>();
        LoadAllSounds();
    }

    private void LoadAllSounds()
    {
        AudioClip[] allSounds = Resources.LoadAll<AudioClip>("");
        foreach (AudioClip clip in allSounds)
        {
            if (clip != null && !soundDictionary.ContainsKey(clip.name))
            {
                soundDictionary.Add(clip.name, clip);
            }
        }
    }
    public void PlaySound(string clipName)
    {
        if (soundDictionary.ContainsKey(clipName) && !AudioListener.pause)
        {
            audioSrc.PlayOneShot(soundDictionary[clipName]);
        }
        else if (!AudioListener.pause)
        {
            Debug.LogWarning($"Sound '{clipName}' not found in the dictionary.");
        }
    }
}