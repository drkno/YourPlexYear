using System.Text.Json.Serialization;

namespace Your2020.Model.Tautulli
{
    public class Response<T>
    {
        [JsonPropertyName("response")]
        public InnerResponse ResponseWrapper { get; set; }

        public T Value => ResponseWrapper.Data;

        public class InnerResponse
        {
            [JsonPropertyName("result")]
            public string Result { get; set; }
            [JsonPropertyName("message")]
            public object Message { get; set; }
            [JsonPropertyName("data")]
            public T Data { get; set; }
        }
    }
}
