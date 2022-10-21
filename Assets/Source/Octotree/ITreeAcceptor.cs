using System.Collections.Generic;

public interface ITreeAcceptor<TLocatable> : ILocatable
    where TLocatable : ILocatable
{
    void AcceptLocatables(IEnumerable<TLocatable> locatable);
}
