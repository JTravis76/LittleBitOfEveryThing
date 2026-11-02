//using Shtomper.Frame.Impl.Client;
//using System;
//using System.Collections.Generic;
//using System.Net;
//using System.Net.Sockets;
//using System.Threading.Tasks;

//namespace DotNetScript
//{
//    //https://activemq.apache.org/components/nms/documentation

//    /* PYTHON print(s)
//     b'STOMP\naccept-version:1.1\nlogin:amq\npasscode:kbp9WprK{]}rCMy1\n\n\x00'
//     b'CONNECTED\nserver:ActiveMQ/5.18.7\nheart-beat:0,0\nsession:ID:b-b8149fef-933a-4642-8b68-606c8a0f3c12-1-34873-1751488293812-3:21890\nversion:1.1\n\n\x00\n'
//     b'SUBSCRIBE\nack:auto\ndestination:/topic/alarm-queue-ecs\nid:a_client_id\n\n\x00'
//     b'DISCONNECT\nreceipt:9494ac6d-a068-4c4f-bde4-d94acdff77c5\n\n\x00'
//     */

//    public class Program1
//    {
//        public const string hostname = "b-b8149fef-933a-4642-8b68-606c8a0f3c12-1.mq.eu-west-1.amazonaws.com";
//        public const string ip = "54.220.6.3";
//        public const int port = 61614;
//        public const string user = "amq";
//        public const string pwd = "kbp9WprK{]}rCMy1";
//        public const string topic = "/topic/alarm-queue-ecs";

//        public static void Main1()
//        {
//            //StompNetTest();
//            //ArtemisTest();
//            //ActiveAMQPTest();
//            //ActiveMQTest();
//            //ActiveNmsTest();
//            //ShtomperTest();

//            // https://stomp.github.io/stomp-specification-1.2.html

//            ////IPHostEntry hostEntry = Dns.GetHostEntry(hostname);
//            ////IPAddress ipAddress = hostEntry.AddressList[0];

//            ////var tcpListener = new TcpListener(IPAddress.Any, port);
//            ////tcpListener.Start();

//            //byte[] buffer = new byte[256];

//            //using (TcpClient client = new TcpClient())//tcpListener.AcceptTcpClient()
//            //{
//            //    client.Connect(hostname, port);

//            //    Console.WriteLine("Connected to server!");
//            //    NetworkStream stream = client.GetStream();

//            //    string CONNECT = "STOMP\naccept-version:1.1\nlogin:amq\npasscode:kbp9WprK{]}rCMy1\n\n\x00";
//            //    byte[] frame = System.Text.Encoding.ASCII.GetBytes(CONNECT);//.Replace(":", "\\c")
//            //    stream.Write(frame, 0, frame.Length);

//            //    int readTotal;
//            //    while ((readTotal = stream.Read(buffer, 0, buffer.Length)) > 0)
//            //    {
//            //        string incoming = System.Text.Encoding.ASCII.GetString(buffer, 0, buffer.Length);
//            //        Console.WriteLine(incoming);
//            //    }

//            //    ////string CONNECT = $"STOMP\naccept-version:1.1\nlogin:{user}\npasscode:{pwd}\n\n\0\n";
//            //    //string CONNECT = "STOMP\naccept-version:1.1\nlogin:amq\npasscode:kbp9WprK{]}rCMy1\n\n\x00";
//            //    ////string SUBSCRIBE = $"SUBSCRIBE\r\nid:{Guid.NewGuid()}\ndestination:{topic}\nack:auto\n\n\0";

//            //    //byte[] frame = System.Text.Encoding.UTF8.GetBytes(CONNECT);//.Replace(":", "\\c")
//            //    //stream.Write(frame, 0, frame.Length);
//            //    //Console.WriteLine("CONNECT frame sent");

//            //    //// Receive data from the server
//            //    //byte[] buffer = new byte[256];
//            //    //int bytesRead = stream.Read(buffer, 0, buffer.Length);
//            //    //string response = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead);
//            //    //Console.WriteLine($"Received: {response}");

//            //    //frame = System.Text.Encoding.UTF8.GetBytes(SUBSCRIBE);
//            //    //stream.Write(frame, 0, frame.Length);
//            //    //Console.WriteLine("SUBSCRIBE frame sent");

//            //    //bytesRead = stream.Read(buffer, 0, buffer.Length);
//            //    //response = System.Text.Encoding.UTF8.GetString(buffer, 0, bytesRead);
//            //    //Console.WriteLine($"Received: {response}");
//            //}

//            //using (var client = new StompSharp.StompClient(hostname, port))
//            //{
//            //    using (var dest = client.GetDestination(topic, client.SubscriptionBehaviors.AutoAcknowledge))
//            //    {
//            //        using (dest.IncommingMessages.Subscribe(Console.WriteLine))
//            //        {
//            //            Console.WriteLine("Messages are written to console. Press any key to unsubscribe and exit.");
//            //            Console.ReadKey();
//            //        }
//            //    }
//            //}
//        }

//        //private static void ShtomperTest()
//        //{
//        //    var waitHandle = new System.Threading.ManualResetEvent(false);

//        //    var client = new Shtomper.Client.Builder.StompClientBuilder()
//        //        .SetMessageConverter(new Shtomper_Converter_NewtonsoftJson.NewtonsoftJsonMessageConverter())
//        //        .SetUsername(user)
//        //        .SetPasscode(pwd)
//        //        .SetHeartbeatCapable(1000)
//        //        .SetHeartbeatDesired(1000)
//        //        .WithBuilder(new Shtomper_Client_WebsocketClient.WebSocketStompClientFactoryBuilder())
//        //        .SetPort(port)
//        //        .SetHost(hostname)
//        //        .SetPath("ws")
//        //        .Build()
//        //        .Create();

//        //    Action<Dictionary<string, string>> callback = Console.WriteLine;
//        //    client.Subscribe(topic, callback);

//        //    waitHandle.WaitOne(TimeSpan.FromSeconds(5));
//        //}

//        //private static void ActiveNmsTest()
//        //{
//        //    // https://activemq.apache.org/components/nms/examples/nms-simple-synchronous-consumer-example
//        //    // Example connection strings:
//        //    //    activemq:tcp://activemqhost:61616
//        //    //    stomp:tcp://activemqhost:61613
//        //    //    ems:tcp://tibcohost:7222
//        //    //    msmq://localhost amqp
//        //    //    amqp:tcp://
//        //    var brokerUri = new Uri($"stomp:tcp://{hostname}:{port}");

//        //    var factory = new Apache.NMS.NMSConnectionFactory(brokerUri);
//        //    using (var conn = factory.CreateConnection())
//        //    using (var session = conn.CreateSession()) 
//        //    {
//        //        Apache.NMS.IDestination destination = Apache.NMS.Util.SessionUtil.GetDestination(session, "alarm-queue-ecs");

//        //        using (Apache.NMS.IMessageConsumer consumer = session.CreateConsumer(destination))
//        //        {
//        //            conn.Start();

//        //            Apache.NMS.IMessage message = consumer.Receive();
//        //            if (message == null)
//        //            {
//        //                Console.WriteLine("No message received!");
//        //            }
//        //            else
//        //            {
//        //                Console.WriteLine("Received message ID " + message.NMSMessageId);
//        //            }
//        //        }
//        //    }
//        //}

//        //private static void StompNetTest()
//        //{
//        //    using (TcpClient client = new TcpClient())
//        //    {
//        //        client.Connect(hostname, port);

//        //        StompNet.IStompConnector connector = new StompNet.Stomp12Connector(
//        //            client.GetStream(), hostname, user, pwd);
//        //        StompNet.IStompConnection connection = connector.ConnectAsync().Result;
//        //        _ = connection.SubscribeAsync(new ConsoleWriterObserver(), topic).Result;

//        //        Task.Delay(250).Wait();

//        //        connection.DisconnectAsync().Wait();

//        //        //Console.WriteLine("Connected to server!");
//        //        //NetworkStream stream = client.GetStream();

//        //        //// Receive data from the server
//        //        //byte[] dataReceived = new byte[256];
//        //        //int bytesRead = stream.Read(dataReceived, 0, dataReceived.Length);
//        //        //string response = Encoding.UTF8.GetString(dataReceived, 0, bytesRead);
//        //        //Console.WriteLine($"Received: {response}");
//        //    }
//        //}

//        //private static void ArtemisTest()
//        //{
//        //    //var factory = new ActiveMQ.Artemis.Client.ConnectionFactory();
//        //    //var endpoints = new System.Collections.Generic.List<ActiveMQ.Artemis.Client.Endpoint>()
//        //    //{
//        //    //    ActiveMQ.Artemis.Client.Endpoint.Create(hostname, port, user ,pwd)
//        //    //};
//        //    ////Task.Run(() => await factory.CreateAsync(endpoint));
//        //    //var connection = factory.CreateAsync(endpoints, new System.Threading.CancellationToken()).Result;
//        //    //var cunsumer = connection.CreateConsumerAsync();
//        //}

//        //private static void ActiveAMQPTest()
//        //{
//        //    // https://activemq.apache.org/components/nms/providers/amqp/
//        //    // https://github.com/apache/activemq-nms-amqp/

//        //    //tcp://
//        //    //amqp://
//        //    //activemq:tcp://
//        //    var brokerUri = $"stomp:tcp://{hostname}:{port}";
//        //    var factory = new Apache.NMS.AMQP.NmsConnectionFactory(user, pwd, brokerUri);
//        //    factory.SendTimeout = 60;
//        //    var conn = factory.CreateConnection();
//        //    var session = conn.CreateSession(Apache.NMS.AcknowledgementMode.AutoAcknowledge);
//        //    var destination = session.GetTopic(topic);
//        //    var consumer = session.CreateConsumer(destination);
//        //    consumer.Listener += OnMessage;
//        //    //var content = consumer.Receive(TimeSpan.FromMilliseconds(30000));
//        //    //System.Diagnostics.Debug.WriteLine(content);    

//        //    conn.Start();
//        //}

//        //private static void ActiveMQTest()
//        //{
//        //    // Define the broker URI (e.g., "tcp://localhost:61616")
//        //    var brokerUri = new Uri($"tcp://{hostname}:{port}");

//        //    // Create a connection factory
//        //    Apache.NMS.IConnectionFactory factory = new Apache.NMS.ActiveMQ.ConnectionFactory(brokerUri);

//        //    // Establish a connection
//        //    using (Apache.NMS.IConnection connection = factory.CreateConnection(user, pwd))
//        //    {
//        //        connection.Start();

//        //        // Create a session
//        //        using (Apache.NMS.ISession session = connection.CreateSession(Apache.NMS.AcknowledgementMode.AutoAcknowledge))
//        //        {
//        //            // Define the destination (queue or topic)
//        //            Apache.NMS.IDestination destination = session.GetQueue(topic);
//        //            //IDestination destination = session.GetTopic(topic);

//        //            // Create a producer and send a message
//        //            using (Apache.NMS.IMessageProducer producer = session.CreateProducer(destination))
//        //            {
//        //                producer.DeliveryMode = Apache.NMS.MsgDeliveryMode.NonPersistent;

//        //                Apache.NMS.ITextMessage message = producer.CreateTextMessage("Hello, ActiveMQ!");
//        //                producer.Send(message);
//        //                Console.WriteLine("Message sent: " + message.Text);
//        //            }

//        //            // Create a consumer and receive a message
//        //            using (Apache.NMS.IMessageConsumer consumer = session.CreateConsumer(destination))
//        //            {
//        //                consumer.Listener += OnMessageReceived;
//        //                //ITextMessage receivedMessage = consumer.Receive() as ITextMessage;
//        //                //if (receivedMessage != null)
//        //                //{
//        //                //    Console.WriteLine("Message received: " + receivedMessage.Text);
//        //                //}

//        //                consumer.Close();
//        //            }
//        //            session.Close();
//        //        }
//        //        connection.Close();
//        //    }
//        //}

//        //// ----------------------------------------
//        //class ConsoleWriterObserver : IObserver<StompNet.IStompMessage>
//        //{
//        //    public void OnNext(StompNet.IStompMessage message)
//        //    {
//        //        Console.WriteLine("MESSAGE: " + message.GetContentAsString());

//        //        if (message.IsAcknowledgeable)
//        //            message.Acknowledge();
//        //    }

//        //    // ERROR frames come through here.
//        //    public void OnError(Exception error)
//        //    {
//        //        Console.WriteLine("ERROR: " + error.Message);
//        //    }

//        //    public void OnCompleted()
//        //    {
//        //        Console.WriteLine("UNSUBSCRIBED!");
//        //    }
//        //}

//        private static void OnMessage(Apache.NMS.IMessage message)
//        {
//            System.Diagnostics.Debug.WriteLine("");
//        }

//        private static void OnMessageReceived(Apache.NMS.IMessage message)
//        {
//            var txtMessage = message as Apache.NMS.ITextMessage;
//            Console.WriteLine("Message received: " + txtMessage.Text);
//        }
//    }
//}
