using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kursach2
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            //using (new tcp_client("192.168.40.1", 80))
            //{
               //tcp_client.tcp_Client.Connect();
                var data = Serialization.Deserialize();
                //var dict = data.Item1;
                //var element = dict[3];
                //for (int i1 = 0; i1 < element.Carta.Recordings.Count; i1++)
                //{
                //    Recordings i = element.Carta.Recordings[i1];
                //    if (i.Books.Number_of_copies <= 0)
                //        element.Carta.Recordings.Remove(i);
                //}

                //dict[3] = element; УДАЛЯЕМ В СЛУЧАЕ НЕПОЛАДОК
                Library library = new Library(data.Item1, data.Item2);
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Menu());
            }
        //}
    }
}
