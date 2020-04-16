using Newtonsoft.Json;

namespace CaptainPav.WebApiProxy
{
    public static class ProxyDefaults
    {
        public static readonly IHttpResponseMessageReader DefaultReader = new DefaultHttpResponseMessageReader(new NetExceptionReconstructor());
        public static readonly JsonSerializerSettings DefaultSerializerSettings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.Objects,
            PreserveReferencesHandling = PreserveReferencesHandling.Objects,
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameAssemblyFormatHandling = TypeNameAssemblyFormatHandling.Full
        };
    }
}