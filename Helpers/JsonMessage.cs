using System.Collections.Generic;

namespace TVSeriesAPI.Helpers
{
    public class JsonMessage<T>
    {
        public List<T> Results { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

    }
}
