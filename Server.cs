//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Kursach2
//{
//    using System;
//    using System.Collections.Generic;
//    using System.Net;
//    using System.Net.Sockets;
//    using System.Text;
//    using System.Text.Json.Serialization;
//    using System.Threading.Tasks;
//    using Newtonsoft.Json;

//    namespace Bing
//    {
//        public class Server
//        {
//            private TcpListener _listener;
//            private List<Client> _clients = new List<Client>();
//            private List<Books> _books = new List<Books>();

//            public async Task StartAsync(int port)
//            {
//                _listener = new TcpListener(IPAddress.Any, port);
//                _listener.Start();
//                Console.WriteLine("Server started.");

//                while (true)
//                {
//                    var client = await _listener.AcceptTcpClientAsync();
//                    Console.WriteLine("Client connected.");
//                    HandleClientAsync(client);
//                }
//            }

//            private async void HandleClientAsync(TcpClient client)
//            {
//                var stream = client.GetStream();

//                while (true)
//                {
//                    var buffer = new byte[1024];
//                    var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);

//                    if (bytesRead == 0)
//                    {
//                        Console.WriteLine("Client disconnected.");
//                        break;
//                    }

//                    var request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
//                    Console.WriteLine("Received request: " + request);

//                    Parse JSON request
//                   var jsonRequest = JsonConvert.DeserializeObject<Request>(request);

//                    string response;

//                    switch (jsonRequest.Method)
//                    {
//                        case "AddClient":
//                            var addClientData = JsonConvert.DeserializeObject<AddClientData>(jsonRequest.Data);
//                            AddClient(addClientData.Name, addClientData.Surname, addClientData.Role);
//                            response = "Client added successfully.";
//                            break;
//                        case "AddBook":
//                            var addBookData = JsonConvert.DeserializeObject<AddBookData>(jsonRequest.Data);
//                            AddBook(addBookData.Key, addBookData.Name, addBookData.Author, addBookData.DateOfPublishing, addBookData.NumberOfCopies);
//                            response = "Book added successfully.";
//                            break;
//                        case "DeleteBook":
//                            var deleteBookData = JsonConvert.DeserializeObject<DeleteBookData>(jsonRequest.Data);
//                            DeleteBook(deleteBookData.Book);
//                            response = "Book deleted successfully.";
//                            break;
//                        case "AddBookToClient":
//                            var addBookToClientData = JsonConvert.DeserializeObject<AddBookToClientData>(jsonRequest.Data);
//                            AddBookToClient(addBookToClientData.Login, addBookToClientData.Book);
//                            response = "Book added to client successfully.";
//                            break;
//                        case "TakeBook":
//                            var takeBookData = JsonConvert.DeserializeObject<TakeBookData>(jsonRequest.Data);
//                            TakeBook(takeBookData.Login, takeBookData.Book);
//                            response = "Book taken successfully.";
//                            break;
//                        default:
//                            response = "Unknown method.";
//                            break;
//                    }

//                    var responseBytes = Encoding.UTF8.GetBytes(response);
//                    await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
//                }

//                client.Close();
//            }

//            private void AddClient(string name, string surname, string role)
//            {
//                _clients.Add(new Client { Name = name, Surname = surname, Role = role });
//            }

//            private void AddBook(int key, string name, string author, DateTime dateOfPublishing, int numberOfCopies)
//            {
//            }
