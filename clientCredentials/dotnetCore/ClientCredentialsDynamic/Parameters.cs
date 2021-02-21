using System;

namespace ClientCredentials
{
    public class Parameters
    {
        public string[] Args{ get; }
        public string Identity{ get; }
        public string Api{ get; }
        public string ClientId{ get; }
        public string ClientSecret{ get; }
        public string UserIdentity{ get; }
        public string UserIdentityProvider{ get; }

        public Parameters(string[] args)
        {
            Args = args;
            Identity = GetParameter("-Identity");
            Api = GetParameter("-Api");
            ClientId = GetParameter("-ClientId");
            ClientSecret = GetParameter("-ClientSecret");
            UserIdentity = GetParameter("-UserIdentity");
            UserIdentityProvider = GetParameter("-UserIdentityProvider");
        }

        private string GetParameter(string name)
        {
            var index = Array.IndexOf(Args, name);
            if( index == -1 ) return null;
            if( ++index > Args.Length -1 ) return null;
            return Args[index];
        }

        public bool IsValid => !string.IsNullOrWhiteSpace(Identity) &&
                               !string.IsNullOrWhiteSpace(Api) &&
                               !string.IsNullOrWhiteSpace(ClientId) &&
                               !string.IsNullOrWhiteSpace(ClientSecret) &&
                               !string.IsNullOrWhiteSpace(UserIdentityProvider) &&
                               !string.IsNullOrWhiteSpace(UserIdentity);

        public string Usage => @"
Invalid Parameters

Usage:

ClientCredentialsDynamic.exe -Identity [your-identity-url]
                             -Api [your-sharedo-url]
                             -ClientId [your-client-id]
                             -ClientSecret [your-client-secret]
                             -UserIdentity [identity-of-user-to-impersonate]
                             -UserIdentityProvider [identity-provider-of-user-to-impersonate]
        ";
    }
}
