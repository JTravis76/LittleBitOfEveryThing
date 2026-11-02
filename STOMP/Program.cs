using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DotNetScript
{
    public class Program
    {
        private const string host = "b-b8149fef-933a-4642-8b68-606c8a0f3c12-1.mq.eu-west-1.amazonaws.com";
        private const int port = 61614;
        private const string username = "amq";
        private const string password = "kbp9WprK{]}rCMy1";
        //private const string topic = "/topic/alarm-queue-ecs";
        private const string topic = "/queue/Consumer.A.VirtualTopic.alarm-queue-ecs";

        /*== Virtual Topic
         * First, create the virtual topic under the Topic section. "VirtualTopic.alarm-queue-ecs"
         * Second, Connect the STOMP client to a queue, not topic. "/queue/Consumer.A.VirtualTopic.alarm-queue-ecs"
         * - The "A" can be any group value.
         * Third, send a message to the "topic/VirtualTopic.alarm-queue-ecs". 
         * That message is forward to the connecting queues with the proper name spacing.
         */

        public static void Main()
        {
            using var tcpClient = new TcpClient(host, port);
            using var sslStream = new SslStream(
                tcpClient.GetStream(),
                false,
                (sender, cert, chain, sslPolicyErrors) => true // Accept all certs (dev only)
            );
            sslStream.AuthenticateAsClientAsync(
                host,
                new X509CertificateCollection(),
                SslProtocols.Tls12,
                false
            ).Wait();
            Console.WriteLine("[INFO] Connected and SSL established.");
            var reader = new StreamReader(sslStream, Encoding.UTF8);
            // Build and send CONNECT frame
            var connectFrame =
                $"CONNECT\n" +
                $"accept-version:1.2\n" +
                $"host:{host}\n" +
                $"login:{username}\n" +
                $"passcode:{password}\n\n\u0000";
            byte[] connectBytes = Encoding.UTF8.GetBytes(connectFrame);
            sslStream.WriteAsync(connectBytes);
            sslStream.FlushAsync().Wait();
            Console.WriteLine("[INFO] Sent CONNECT frame.");
            // Read CONNECTED response
            string line;
            while (!string.IsNullOrEmpty(line = reader.ReadLineAsync().Result))
            {
                Console.WriteLine($"[RECV] {line}");
                if (line.StartsWith("CONNECTED"))
                    break;
            }
            // Build and send SUBSCRIBE frame
            var subscribeFrame =
                $"SUBSCRIBE\n" +
                $"id:{Guid.NewGuid()}\n" +
                $"destination:{topic}\n" +
                $"ack:auto\n\n\u0000";
            byte[] subscribeBytes = Encoding.UTF8.GetBytes(subscribeFrame);
            sslStream.WriteAsync(subscribeBytes);
            sslStream.FlushAsync().Wait();
            Console.WriteLine($"[INFO] Subscribed to: {topic}");
            // Message receive loop
            var buffer = new char[1];
            var messageBuilder = new StringBuilder();
            while (true)
            {
                int bytesRead = reader.ReadAsync(buffer, 0, 1).Result;
                if (bytesRead == 0)
                {
                    Console.WriteLine("[WARN] Connection closed.");
                    break;
                }
                char ch = buffer[0];
                if (ch == '\0') // End of STOMP frame
                {
                    var message = messageBuilder.ToString();
                    messageBuilder.Clear();
                    if (!string.IsNullOrWhiteSpace(message))
                    {
                        Console.WriteLine($"\n📨 Received Message:\n{message.Trim()}");
                    }
                }
                else
                {
                    messageBuilder.Append(ch);
                }
            }
        }
    }
}
