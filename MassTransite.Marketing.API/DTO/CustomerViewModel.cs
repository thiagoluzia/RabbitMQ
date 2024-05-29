namespace MassTransite.Marketing.API.DTO
{
    public class CustomerViewModel
    {
        public CustomerViewModel(int id, string? name, string? document, string? email)
        {
            Id = id;
            Name = name;
            Document = document;
            Email = email;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Document { get; set; }
        public string? Email { get; set; }
    }
}
