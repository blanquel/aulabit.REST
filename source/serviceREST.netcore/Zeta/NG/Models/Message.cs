
using Newtonsoft.Json;
using NG.Utils;

namespace NG.Models
{
    public class Message
    {
        public bool status_item { get; set; }
        public string details { get; set; }

        public Message()
        {
            this.status_item = false;
            this.details = "NOT PROCCESS";
        }

        public Message(bool response, string Details)
        {
            this.status_item = response;
            this.details = string.IsNullOrEmpty(Details) ? response.ToString() : Details;
        }

        public string Res()
        {
            return JsonConvert.SerializeObject(this, Formatting.None);
        }
    }
}
