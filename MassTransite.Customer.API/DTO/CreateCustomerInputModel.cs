namespace MassTransite.Customer.API.DTO
{
    public class CreateCustomerInputModel
    {
        public CreateCustomerInputModel(int id, string? name, string? document, string? email)
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
