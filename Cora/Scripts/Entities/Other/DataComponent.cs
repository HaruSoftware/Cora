using Newtonsoft.Json;

namespace Cora
{
    public class DataComponent
    {
        [JsonIgnore]
        public long Id { get; set; }
        [JsonIgnore]
        public long InstanceId { get; set; }
        [JsonIgnore]
        public long OwnerId { get; set; }
    }
}
