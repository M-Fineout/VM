using Newtonsoft.Json;
using System.Text;

namespace Org.Messaging
{
    public static class MessageExtensions
    {
        /// <summary>
        /// Serliazes a message body and wraps it in an envelope for sending
        /// </summary>
        /// <typeparam name="TBody"></typeparam>
        /// <param name="message">Message to wrap</param>
        /// <returns>Wrapped message</returns>
        public static Envelope Wrap<TBody>(this TBody message)
            where TBody: IMessage
        {
            var json = JsonConvert.SerializeObject(message);
            return new Envelope
            {
                Subject = message.GetType().FullName,
                Body = Encoding.Unicode.GetBytes(json)
            };
        }
    }
}
