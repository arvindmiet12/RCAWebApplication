namespace RCAWebApplication.Models
{
    public record LookupItem(int Id, string Text);

    public record RcaAcSetupRow(int SetupId, string CategoryName, string DenialText, string RcaName, string ActionCodeName);

    public class RcaAcSetupDto
    {
        public int CategoryId { get; set; }
        public int DenialId { get; set; }
        public int RcaId { get; set; }
        public int ActionCodeId { get; set; }

    }
}
