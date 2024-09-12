using System;

public interface ISpawnable
{
    public event Action<ISpawnable> Destroyed;
}
