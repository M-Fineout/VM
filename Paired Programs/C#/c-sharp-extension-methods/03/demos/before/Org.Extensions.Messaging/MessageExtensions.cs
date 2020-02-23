using Newtonsoft.Json;
using System.Text;

namespace Org.Messaging
{
    public static class MessageExtensions
    {
        /// <summary>
        /// Serliazes a message body and wraps it in an envelope for sending
        /// </summary>
        /// <param name="message">Message to wrap</param>
        /// <returns>Wrapped message</returns>
        
        //for types which you own, a marker interface can be a good way of restricting the target of an extension method without restricting your class design. This is a non-breaking change, as my event class already implements IMessage, so the extension method still applies, and when I run the test everything compiles and the test passes, but now I don't get the confusing behavior of Visual Studio suggesting my message wrapping extension on every type of object. 
        //public static Envelope Wrap(this IMessage message)
        public static Envelope Wrap<TBody>(this TBody message) where TBody: IMessage
        {
            var json = JsonConvert.SerializeObject(message); //serialize to json
            return new Envelope
            {
                Subject = message.GetType().FullName,
                Body = Encoding.Unicode.GetBytes(json) // populates body by encoding the json to a byte array
            };
        }
    }
}
