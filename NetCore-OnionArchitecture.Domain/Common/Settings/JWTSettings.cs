namespace NetCore_OnionArchitecture.Domain.Common.Settings
{
    public class JWTSettings
    {
        public const string SettingKey = "JWTSettings";

        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double DurationInMinutes { get; set; }
    }
}
