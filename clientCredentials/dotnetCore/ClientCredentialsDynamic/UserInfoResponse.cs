using System;
using System.Linq;
using System.Collections.Generic;

namespace ClientCredentials
{
    public class UserInfoResponse
    {
        public bool IsAuthenticated{ get; set; }
        public string ClientId{ get; set; }
        public Guid? UserId{ get; set; }

        public string UserName{ get; set; }
        public string Provider{ get; set; }

        public string FirstName{ get; set; }
        public string Surname{ get; set; }
        public string FullName{ get; set; }

        public Guid? OrganisationId{ get; set; }

        public string Persona{ get; set; }

        public List<string> GlobalPermissions{ get; set; }

        public UserInfoResponse()
        {
            GlobalPermissions = new List<string>();
        }

        public void PrettyPrint()
        {
            Console.WriteLine("{");
            Write("IsAuthenticated", IsAuthenticated);
            Write("ClientId", ClientId);
            Write("UserId", UserId);
            Write("UserName", UserName);
            Write("Provider", Provider);
            Write("FirstName", FirstName);
            Write("Surname", Surname);
            Write("FullName", FullName);
            Write("OrganisationId", OrganisationId);
            Write("Persona", Persona);
            PrettyPrintGlobalPermissions();
            Console.WriteLine("}");
        }

        public void PrettyPrintGlobalPermissions()
        {
            const int max = 2;
            var output = String.Join(", ", GlobalPermissions.Take(max));
            if( GlobalPermissions.Count > max) output += $" [...+{GlobalPermissions.Count-max} more]";
            Write("GlobalPermissions", output);
        }

        private void Write(string key, object output)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"    {key}");
            Console.ResetColor();
            Console.WriteLine($": {output}");
        }
    }
}