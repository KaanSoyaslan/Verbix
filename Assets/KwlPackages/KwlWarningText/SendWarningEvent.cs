using UnityEngine;
public readonly struct SendWarningEvent
{
    public readonly string message;
    public readonly Color color;
    public readonly float lifetime;

    public SendWarningEvent(string _message, Color _color, float _lifetime = 2f)
    {
        message = _message;
        color = _color;
        lifetime = _lifetime;
    }
}
