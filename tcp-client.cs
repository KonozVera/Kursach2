using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Kursach2
{
    class tcp_client// : IDisposable
    {
        public static tcp_client tcp_Client;
        private string server;
        private int port;
        private TcpClient client;
        private static JsonSerializer serializer;
        private static int encodingLen = 7;
        public tcp_client(String server, int port) {
            this.server = server;
            this.port = port;
            serializer = new JsonSerializer();
            tcp_Client = this;
            // смотри, вы с сервером дожны находится в 1 сети, это разные устройтсва должны быть
            // т.е. вы с алесей подключаетесь к 1 wifi и проверяете
            // либо пользуетесь RadminVPN. Допустим это гргу
            // ГРГУ не подойдёт, там все пользователи анонимные
            // Допустим кто-то раздал вам ( С телефона), так уже можно/
            // Пишешь IPCONFIG на сервере, и берёшь IPv4 адресс от туда. Я сейчас могу так сделать через свой телефон?
            // Т.е. вы должны запустить программу сервера где-то
            // А эту программу пишет Алеся. Сразу можете запустить это? Ага, только на другом компеили я могу через другую вируалку? Не проверял, но по логике - да. Ставишь тип сети NAT и должно получится. Ок, спасибр, сейчас попробую.
            //Что? Я отключусь, будут проблемы/вопросы - пиши. Ок)ахахаха, не читай)
        }
        public void Connect() {
            client = new TcpClient(server, port);
        }
        public static string SendMessage(String message, int len = 20000)
        {
            var stream = tcp_Client.client.GetStream();
            byte[] bytes = Encoding.ASCII.GetBytes(message);
            stream.Write(bytes, 0, bytes.Length);
            return ResieveMessage(stream, len * encodingLen);
        }
        private static string ResieveMessage(Stream stream, int streamLen) {
            byte[] bytes = new byte[streamLen];
            stream.Read(bytes, 0, bytes.Length);
            return Encoding.ASCII.GetString(bytes).Replace("\0", "");
        }
        public static string Send<T>(String command, T obj, int len = 20000)
        {
            var request = new Request<T>(command, obj);
            var stream = tcp_Client.client.GetStream();
            TextWriter writer = new StreamWriter(stream);
            serializer.Serialize(writer, request);
            return ResieveMessage(stream, len * encodingLen);
        }
        //public void Dispose() => client.Close();
    }
    public class Request<T>
    {
        public string Mehtod { get; set; }
        public T Data { get; set; }
        public Request(string method, T  Data){
            this.Mehtod = Mehtod;
            this.Data = Data;
        }
    }
}
