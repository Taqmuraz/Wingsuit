using System.Collections.Generic;

public interface IMobileControlProvider
{
    IEnumerable<IMobileKeyProvider> GetKeys();
    IEnumerable<IMobileAxisProvider> GetAxes();
}
