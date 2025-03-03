using KwlEventBus;
using UnityEngine;
public class DemoEventEmitter : MonoBehaviour
{
    [SerializeField] private bool emitEvent;

    void Update()
    {
        if (emitEvent)
        {
            emitEvent = false;
            EmitTestEvent();
        }
    }
    private void EmitTestEvent()
    {
        KwlBus<DemoEvent>.NotifyListeners(new DemoEvent(3, "test message", 1f));
    }
}
