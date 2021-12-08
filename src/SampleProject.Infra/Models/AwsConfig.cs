namespace SampleProject.Infra.Models
{
    public class AwsConfig
    {
        public string AccessKeyId { get; set; }
        public string SecretAccessKey { get; set; }
        public string ServiceUrl { get; set; }
        public string Region { get; set; }
        public bool IsLocalMode { get; set; } = true;
    }
}