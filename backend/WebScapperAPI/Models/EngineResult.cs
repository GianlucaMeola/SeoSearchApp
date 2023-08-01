namespace WebScapperAPI.Models
{
    public class EngineResult
    {
        public int Id { get; set; }
        public string KeyWords { get; set; }
        public string Url { get; set; }
        public string EngineName { get; set; }
        public string Ranking { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
