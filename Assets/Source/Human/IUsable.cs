public interface IUsable
{
    IControlAction Use(IUser user);
    string Description { get; }
}
