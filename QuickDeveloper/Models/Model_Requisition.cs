namespace QuickDeveloper.Models
{
    public class Model_Requisition
    {
        public int idRequisition { get; set; }
        public string description { get; set; }
        public string developer { get; set; }
        public string requester { get; set; }
        public DateTime dateRequisition { get; set; }
        public DateTime dateRequisitionExp { get; set; }
        public string email { get; set; }
    }
}
