//Defines methods for every object that has an lock-unlock behaviour
public interface IUnlockable
{
    bool IsUnlocked { get; }
    bool Unlock();
}
