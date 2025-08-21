namespace RCAWebApplication.Models
{
    public class Combination
    {
        public int CombinationId { get; set; }
        public int CategoryId { get; set; }
        public int DenialDescriptionId { get; set; }
        public int RCAId { get; set; }
        public int ActionCodeId { get; set; }
        public string CategoryName { get; set; }
        public string DenialText { get; set; }
        public string RCAName { get; set; }
        public string ActionCodeName { get; set; }
    }
}
