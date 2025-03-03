using UnityEngine;
using KwlEventBus;

public class KwlWarningTextDemo : MonoBehaviour
{
    [SerializeField] private bool showWarningText;
    [SerializeField] private string warningText;
    [SerializeField] private Color warningColor;
    [SerializeField] private float warningShowTime = 2f;

    void Update()
    {
        if (showWarningText)
        {
            showWarningText = false;
            EmitTestEvent();
        }
    }
    private void EmitTestEvent()
    {
        KwlBus<SendWarningEvent>.NotifyListeners(new SendWarningEvent(warningText, warningColor, warningShowTime));
    }
}
