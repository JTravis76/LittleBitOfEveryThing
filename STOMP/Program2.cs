//using System;

//namespace ConsoleApp1
//{
//    internal class Program
//    {
//        public const string hostname = "b-b8149fef-933a-4642-8b68-606c8a0f3c12-1.mq.eu-west-1.amazonaws.com";
//        public const int port = 61614;
//        public const string user = "amq";
//        public const string pwd = "kbp9WprK{]}rCMy1";
//        public const string topic = "/topic/alarm-queue-ecs";

//        static void Main3(string[] args)
//        {
//            // https://stomp.github.io/stomp-specification-1.2.html
//            // -------------------------------- //
//            // <package id="Apache.NMS" version="1.5.1" targetFramework="net48" />
//            // <package id="Apache.NMS.Stomp" version="1.5.4" targetFramework="net48" /> 

//            var factory = new Apache.NMS.Stomp.ConnectionFactory(new Uri($"stomp:tcp://{hostname}:{port}"));
//            using (var conn = factory.CreateConnection(user, pwd))
//            {
//                //conn.Start();
//                using (var session = conn.CreateSession(Apache.NMS.AcknowledgementMode.AutoAcknowledge))
//                {
//                    var dest = session.GetTopic("alarm-queue-ecs");

//                    conn.Start();

//                    //using (var producer = session.CreateProducer(dest))
//                    //{
//                    //    var msg = session.CreateTextMessage("Hello, ActiveMQ");
//                    //    producer.Send(msg);
//                    //}

//                    using (var consumer = session.CreateConsumer(dest))
//                    {
//                        consumer.Listener += OnMessageReceived;
//                    }
//                }
//            }
//        }

//        private static void OnMessageReceived(Apache.NMS.IMessage message)
//        {
//            var txtMessage = message as Apache.NMS.ITextMessage;
//            Console.WriteLine("Message received: " + txtMessage.Text);
//        }
//    }
//}
