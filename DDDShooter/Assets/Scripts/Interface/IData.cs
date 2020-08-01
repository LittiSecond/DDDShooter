namespace DddShooter
{
    public interface IData<T>
    {
        void Save(T data, string path = null);
        T Load(string path = null);
        void SetPath(string path);
    }
}
