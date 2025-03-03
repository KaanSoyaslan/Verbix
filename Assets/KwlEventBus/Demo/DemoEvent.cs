using UnityEngine;
public readonly struct DemoEvent
{
    public readonly int testNum;
    public readonly string testText;
    public readonly float testFloat;

    public DemoEvent(int _testNum , string _testText, float _testFloat)
    {
        this.testNum = _testNum;
        this.testText = _testText;
        this.testFloat = _testFloat;
    }
}
