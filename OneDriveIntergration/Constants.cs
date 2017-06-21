namespace OneDriveIntergration
{
    public static class Constants
    {
        public const string AuthorizationCodeEndpoing = "https://login.microsoftonline.com/common/oauth2/v2.0/authorize";
        public const string AuthorizationTokenEndpoing = "https://login.microsoftonline.com/common/oauth2/v2.0/token";
        public const string AuthorizationCodeEndpointFormat = "{0}?response_type=code&client_id={1}&redirect_uri={2}&scope={3}";
        public const string AuthorizationTokenEndpointFormat = "client_id={0}&client_secret={1}&code={2}&redirect_uri={3}&grant_type={4}&Scope={5}";
        public const string ClientId = "61557339-5d6c-46bf-92ab-97bfa7949f37";
        public const string ClientSecret = "t401ftzeksHkcL8VcwayHah";
        public const string Scope = "openid offline_access mail.read";
        public const string GrantType = "authorization_code";
        public const string RedirectUri = "http://localhost:59824/Auth/Authorize";

        public const string TargetFolder = "https://graph.microsoft.com/v1.0/me/drive/items/7FA126182C1A71C0!156/children";
    }
}