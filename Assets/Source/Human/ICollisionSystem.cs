public interface ICollisionSystem
{
    void SubscribeCollisionHandler(ICollisionHandler handler);
    void UnsubscribeCollisionHandler(ICollisionHandler handler);
}
