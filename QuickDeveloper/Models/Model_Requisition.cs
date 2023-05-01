namespace QuickDeveloper.Models
{
    public class Model_Requisition
    {
        public int idRequisition { get; set; } = 0;
        public int idUser { get; set; }
        public int idDeveloper { get; set; }
        public string description { get; set; }
        public string developer { get; set; }
        public string requester { get; set; }
    }
}
