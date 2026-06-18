using FGC.Contracts.Events;
using MassTransit;

namespace FGC.Notifications.Consumers
{

    public class UserCreatedConsumer : IConsumer<UserCreatedEvent>
    {
        public Task Consume(ConsumeContext<UserCreatedEvent> context)
        {
            Console.WriteLine($"E-mail de boas-vindas enviado para {context.Message.Email}");

            return Task.CompletedTask;
        }
    }
}
