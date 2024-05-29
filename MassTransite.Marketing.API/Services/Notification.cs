namespace MassTransite.Marketing.API.Services
{
    public class Notification : INotification
    {

        public Notification()
        {
            
        }
        public async Task Text(string text)
        {
            Console.WriteLine($"Email: {text}");

        }
    }
}
