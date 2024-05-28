namespace RabbitMQClient.Customers.API.DTO
{
    public class CustomerImputModel
    {
        public CustomerImputModel(int id, string name, string document, string email)
        {
            Id = id;
            Name = name;
            Document = document;
            Email = email;
        }

        public int Id { get; private set; }
        public string Name { get; private set; }
        public string  Document { get; private set; }
        public string Email { get; private set; }

    }
}
