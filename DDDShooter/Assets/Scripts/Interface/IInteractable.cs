namespace Geekbrains
{
    public interface IInteractable
    {
        void Interact();
        string GetMessageIfTarget();
        bool IsTarget { set; }
    }
}
