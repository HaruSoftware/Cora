using Newtonsoft.Json;

namespace Cora
{

    public class Enterprise
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int UserRole { get; set; }
        public string HashId { get; set; }
        public string BusinessName { get; set; }
        public string TradeName { get; set; }
        public string LegalName { get; set; }
        public string CompanyId { get; set; }
        public string StateRegisterId { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ContactPerson { get; set; }
        public string SecretKey { get; set; }

        //USER STATISTICS
        public string LastActivity { get; set; }
        public string BuildVersion { get; set; }
        public string RegistrationDate { get; set; }
        public string CreatorId { get; set; }
        public Address Address { get; set; }
        [JsonProperty("AccountStatus")]
        public AccountStatus Status { get; set; }
        public bool AuthorizeAcess(string password) => password == Password;

        [JsonIgnore]
        public string GetStatusValue
        {
            get
            {
                if(Status != null)
                {
                    return Status.Allowed == true ? "Ativo" : "Inativo";
                }
                else
                {
                    return "Erro ao verificar";
                }
            }
        }

        public static Enterprise Default()
        {
            return new Enterprise
            {
                Id = string.Empty,
                Username = string.Empty,
                Password = string.Empty,
                UserRole = 0,
                HashId = string.Empty,
                SecretKey = string.Empty,
                BusinessName = string.Empty,
                TradeName = string.Empty,
                LegalName = string.Empty,
                CompanyId = string.Empty,
                StateRegisterId = string.Empty,
                Phone = string.Empty,
                Email = string.Empty,
                ContactPerson = string.Empty,
                LastActivity = string.Empty,
                BuildVersion = string.Empty,
                RegistrationDate = string.Empty,
                Address = new Address(),
                Status =  new AccountStatus()
            };
        }

    }
    public class AccountStatus
    {
        public bool Allowed { get; set; }
        public string Reason { get; set; }
        public string SystemMessage { get; set; }

        public static AccountStatus Default()
        {
            return new AccountStatus
            {
                Allowed = true,
                Reason = "None",
                SystemMessage = "None",
            };
        }
    }
}
