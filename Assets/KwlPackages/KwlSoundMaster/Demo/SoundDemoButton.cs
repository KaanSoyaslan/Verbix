using UnityEngine;

public class SoundDemoButton : MonoBehaviour
{
    [SerializeField] private bool playSound;

    void Update()
    {
        if (playSound)
        {
            playSound = false;
            EmitTestEvent();
        }
    }
    private void EmitTestEvent()
    {
        KwlSoundMaster.Instance.PlaySound("Bruh");
    }
}
