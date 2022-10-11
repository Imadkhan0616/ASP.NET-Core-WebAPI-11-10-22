namespace ASP.NET_Core_Web_Api_CRUD_Run.Model.DomainModel
{
    public class Note
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Colorhex { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
