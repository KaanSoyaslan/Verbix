using KwlEventBus;
using UnityEngine;

public class DemoEventListener : MonoBehaviour
{
    private void OnEnable()
    {
        KwlBus<DemoEvent>.AddListener(OnTestEventCalled);
    }
    private void OnDisable()
    {
        KwlBus<DemoEvent>.RemoveListener(OnTestEventCalled);
    }

    private void OnTestEventCalled(DemoEvent obj)
    {
        Debug.LogWarning(string.Format("Event Received! \n {0} , {1} , {2}", obj.testNum, obj.testText, obj.testFloat));
    }
}
