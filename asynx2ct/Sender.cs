//using System;
//using System.Diagnostics.Metrics;
//using System.IO.Pipes;
//using System.Net.Sockets;
//using System.Reflection.PortableExecutable;
//using System.Text;
//using System.Threading.Tasks;
//class Sender
//{
//    static async Task Main(string[] args)
//    {
//        using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "testpipe", PipeDirection.Out))
//        {
//            pipeClient.Connect();
//            Console.WriteLine("Connected to pipe.");

//            using (StreamWriter sw = new StreamWriter(pipeClient))
//            {
//                while (true)
//                {
//                    Console.Write("Enter message: ");
//                    string message = Console.ReadLine();

//                    sw.WriteLine(message);
//                    sw.Flush();

//                    Console.WriteLine("Message sent.");
//                }
//            }
//        }
//    }
//}


//class Receiver
//{
//    static async Task Reca(string[] args)
//    {
//        using (NamedPipeServerStream pipeServer = new NamedPipeServerStream("testpipe", PipeDirection.In))
//        {
//            Console.WriteLine("Waiting for connection...");
//            await pipeServer.WaitForConnectionAsync();
//            Console.WriteLine("Client connected.");

//            using (StreamReader sr = new StreamReader(pipeServer))
//            {
//                //string line;
//                //while ((line = await reader.ReadLineAsync()) != null)
//                //{
//                //    string message = sr.ReadLine();
//                //    Console.WriteLine("Received message: " + message);
//                //}
//                while (true)
//                {
//                    string message = sr.ReadLine();
//                    Console.WriteLine("Received message: " + message);
//                }
//            }
//        }
//    }
//}

using System;
using System.IO;
using System.IO.Pipes;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

public class Sender
{
    static void Main(string[] args)
    {
        
    }
    public async Task Run(string filePath)
    {
        using (NamedPipeClientStream pipeClient = new NamedPipeClientStream(".", "TestPipe", PipeDirection.Out))
        {
            Console.WriteLine("Подключение к серверу");
            await pipeClient.ConnectAsync();
            Console.WriteLine("Подключено к серверу");

            using (FileStream fileStream = File.OpenRead(filePath))
            {
                await fileStream.CopyToAsync(pipeClient);
            }

            Console.WriteLine("Файл отправлен");
        }
    }
}

