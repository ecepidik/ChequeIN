
namespace ChequeIN.Configurations
{
    public class Authentication
    {
        public Authentication()
        {
            DisableAuthentication = false;
            DevelopmentUserId = "";
        }

        public bool DisableAuthentication { get; set; }
        public string DevelopmentUserId { get; set; }
    }
}
