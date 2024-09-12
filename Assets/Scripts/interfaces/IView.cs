public interface IView
{
    void UpdateSpawned(ulong count);
    void UpdateInstantiated(ulong count);
    void UpdateActive(ulong count);
}