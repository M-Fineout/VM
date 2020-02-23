namespace Org.Messaging
{
    public interface IMessageQueue
    {
        void Publish(Envelope message);
    }
}
