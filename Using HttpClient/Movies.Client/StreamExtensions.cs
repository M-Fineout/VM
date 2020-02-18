
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Movies.Client
{
    //public static class StreamExtensions
    //{
    //    //We make it generically typed, as we want to be able to pass through the type of the object we want to deserialize into.
    //    public static T ReadAndDeserializeFromJson<T>(this Stream stream)
    //    {
    //        if (stream == null)
    //        {
    //            throw new ArgumentNullException(nameof(stream));
    //        }

    //        if (!stream.CanRead)
    //        {
    //            throw new NotSupportedException("Can't read from this stream");
    //        }

    //        using (var streamReader = new StreamReader(stream))
    //        {
    //            using (var jsonTextReader = new JsonTextReader(streamReader))
    //            {
    //                var jsonSerializer = new JsonSerializer();
    //                return jsonSerializer.Deserialize<T>(jsonTextReader);
    //            }
    //        }
    //    }

    //    public static void SerializeToJsonAndWrite<T>(this Stream stream, T objectToWrite)
    //    {
    //        if (stream == null)
    //        {
    //            throw new ArgumentNullException();
    //        }

    //        if (!stream.CanRead)
    //        {
    //            throw new NotSupportedException("Cannot write to stream");
    //        }

    //        using (var streamWriter = new StreamWriter(stream, new UTF8Encoding(), 1024, true)) //leave stream open for 'POST'
    //        {
    //            using (var jsonTextWriter = new JsonTextWriter(streamWriter))
    //            {
    //                var jsonSerializer = new JsonSerializer();
    //                jsonSerializer.Serialize(jsonTextWriter, objectToWrite);
    //                jsonTextWriter.Flush(); //This ensures that the buffer is flushed to the underlying TextWriter. If we don't do that, we might end up with an empty or incomplete stream.
    //            }
    //        }
    //    }
    //}
}

