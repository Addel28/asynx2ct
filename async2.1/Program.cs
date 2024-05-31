using System.IO.Pipes;
using System.Reflection;
public class Receiver
{
    public async Task Run()
    {
        using (NamedPipeServerStream pipeServer = new NamedPipeServerStream("TestPipe", PipeDirection.In))
        {
            Console.WriteLine("Ожидание подключения клиента");
            await pipeServer.WaitForConnectionAsync();
            Console.WriteLine("Клиент подключен");

            using (StreamReader reader = new StreamReader(pipeServer))
            {
                string line;
                while ((line = await reader.ReadLineAsync()) != null)
                {
                    Console.WriteLine("Полученый файл содержит:");
                    Console.WriteLine(line);
                }
            }

            Console.WriteLine("Файл получен и выведен.");
        }
    }
}

public class Program
{
    static async Task Main(string[] args)
    {
        string filePath = "C:\\Users\\User\\source\\repos\\asynx2ct\\asynx2ct\\bin\\Debug\\net8.0\\test.txt";

        Sender sender = new Sender();
        Receiver receiver = new Receiver();

        Task senderTask = sender.Run(filePath);
        Task receiverTask = receiver.Run();

        await Task.WhenAll(senderTask, receiverTask);
    }
}