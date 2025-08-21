namespace RCAWebApplication.Models
{
    public class NewItemModel
    {
        public string Type { get; set; } // "Category", "DenialDescription", "RCA", "ActionCode"
        public string Name { get; set; }
        public string Description { get; set; } // For ActionCode
        public string Code { get; set; } // For ActionCode
    }
}
