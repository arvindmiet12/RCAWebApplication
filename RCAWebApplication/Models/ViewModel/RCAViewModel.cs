namespace RCAWebApplication.Models.ViewModel
{
    public class RCAViewModel
    {
        public int Id { get; set; }
        public List<Category> Categories { get; set; }
        public List<DenialDescription> DenialDescriptions { get; set; }
        public RootCauseAnalysis RCAs { get; set; }
        public ActionCode ActionCodes { get; set; }

        public Combination combinations { get; set; }
    }
}
