using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Cora
{
    public class Address : DataComponent
    {
        public string PostalCode { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public string Complement { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        [JsonIgnore]
        public string FullAddress
        {
            get => string.Format("{0}, {1} {2}, {3}-{4}", Street, Number, District, City, State);
        }

        public Address()
        {
            PostalCode = string.Empty;
            Street = string.Empty;
            Number = 0;
            Complement = string.Empty;
            District = string.Empty;
            City = string.Empty;
            State = string.Empty;
        }
        public static Address Default()
        {
            return new Address
            {
                PostalCode = "None",
                Street = "None",
                Number = 0,
                Complement = "None",
                District = "None",
                City = "None",
                State = "None",
            };
        }
    }
}
