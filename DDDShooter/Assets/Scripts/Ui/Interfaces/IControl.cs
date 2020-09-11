namespace Geekbrains
{
    public interface IControl
    {
        UnityEngine.GameObject Instance { get; }
        UnityEngine.UI.Selectable Control { get; }
    }
}
