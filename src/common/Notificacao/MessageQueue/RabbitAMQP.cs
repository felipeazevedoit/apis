using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace TServices.Comum.Notificacao.MessageQueue
{
    public class RabbitAMQP
    {
        public void Publish(Model.Notificacao.MessageQueue model)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = model.Uri;

            try
            {
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: model.Key,
                            durable: model.Durable,
                            exclusive: model.Exclusive,
                            autoDelete: model.AutoDelete,
                            arguments: model.Arguments);

                        IBasicProperties basicProperties = channel.CreateBasicProperties();
                        basicProperties.Persistent = true;
                        basicProperties.ContentType = "application/json";
                        basicProperties.Type = "Email";

                        var body = Encoding.UTF8.GetBytes(Newtonsoft.Json.JsonConvert.SerializeObject(model.Model));

                        channel.BasicPublish(exchange: "",
                            routingKey: model.Key,
                            basicProperties: basicProperties,
                            body: body);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Consumer(Model.Notificacao.MessageQueue model, EventHandler<BasicDeliverEventArgs> eventHandler)
        {
            ConnectionFactory factory = new ConnectionFactory();
            factory.Uri = model.Uri;

            try
            {
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.QueueDeclare(queue: model.Key,
                        durable: model.Durable,
                        exclusive: model.Exclusive,
                        autoDelete: model.AutoDelete,
                        arguments: model.Arguments);

                        IBasicProperties basicProperties = channel.CreateBasicProperties();
                        basicProperties.Persistent = true;

                        var consumer = new EventingBasicConsumer(channel);
                        consumer.Received += eventHandler;

                        channel.BasicConsume(queue: model.Key,
                         autoAck: true,
                            consumer: consumer);

                        Console.WriteLine("Aguardando mensagens para processamento");
                        Console.WriteLine("Pressione uma tecla para encerrar...");
                        Console.ReadKey();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
