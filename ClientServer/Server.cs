using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace ClientServer
{
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();
        }

        TcpListener listener;
        bool treeOrClient = true;
        bool DirOrDirInfo = true;

        List<Socket> sockets;

        const int localPort = 8080;

        private void Server_Load(object sender, EventArgs e)
        {
            try
            {
                IPEndPoint localPoint = new IPEndPoint(IPAddress.Any, localPort);

                listener = new TcpListener(localPoint);

                // запуск процесса прослушивания нового сокета 
                listener.Start();

                sockets = new List<Socket>();

                timer1.Enabled = true;

                richTextBoxMessages.AppendText("Открыт TCP port\n");
            }
            catch (Exception ex)
            {
                richTextBoxMessages.AppendText(ex.Message + "\n");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                CheckListener(); // проверяем новые подключения
                if (sockets.Count > 0)
                    buttonClear.Enabled = true;
                else
                    buttonClear.Enabled = false;
                // проверяем сокеты на новые данные
                for (int i = sockets.Count - 1; i >= 0;i--)
                {
                    if (i % 2 == 0)
                    {
                        Socket socket = sockets[i];

                        if (socket.Available > 0)
                        {
                            byte[] data = new byte[socket.Available];
                            int data_size = socket.Receive(data);
                            string text_data = Encoding.UTF8.GetString(data, 0, data_size);

                            richTextBoxMessages.AppendText(text_data + "\n");

                            // отправка ответа
                            //SendToClient(text_data);
                            SendToServerTXTFile(text_data);
                        }
                    }
                    else // для директорий
                    {
                        Socket socket = sockets[i];

                        if (socket.Available > 0)
                        {
                            byte[] data = new byte[socket.Available];
                            int data_size = socket.Receive(data);
                            string text_data = Encoding.UTF8.GetString(data, 0, data_size);

                            //richTextBoxMessages.AppendText("Получен запрос на открытие директории\n");
                            //richTextBoxMessages.AppendText(text_data + "\n");

                            // отправка ответа
                            SendDirectoriesToClient(text_data);
                        }
                    }
                }

            }
            catch(Exception ex)
            {
                richTextBoxMessages.AppendText(ex.Message + "\n");
            }
        }

        private void SendToClient(string text_data)
        {
            bool tOrC = true;
            //прохождение всех клиентов
            foreach (Socket socket in sockets)
            {
                if (tOrC)
                {
                    try
                    {
                        // преобразуем текст в байты
                        byte[] data = Encoding.UTF8.GetBytes(text_data);

                        // отправка
                        socket.Send(data);
                    }
                    catch (Exception ex)
                    {
                        richTextBoxMessages.AppendText(ex.Message + "\n");
                    }
                }
                tOrC = !tOrC;
            }
        }


        // Отправка текста с файла пользователю
        private void SendToServerTXTFile(string path)
        {
            string[] text = File.ReadAllLines(path);

            foreach (string str in text)
            {
                // переводим в байты
                byte[] data = Encoding.UTF8.GetBytes(str);
                
                // передаем сообщение клиенту
                for(int i = 0;i<sockets.Count;i++)
                    if(i%2==0)
                        sockets[i].Send(data);
            }
            richTextBoxMessages.AppendText("Всем пользователем отправлен текст файла \'" + path +"\'\n");
        }
        // Проверка подключений
        private void CheckListener()
        {
            if(listener.Pending()) // есть новые соединения
            {
                // сокет для нового пользователя
                Socket newSocket = listener.AcceptSocket();

                sockets.Add(newSocket);
                if (treeOrClient)
                {
                    richTextBoxMessages.AppendText("Пользователь " + newSocket.RemoteEndPoint + " подключен!\n");
                    treeOrClient = !treeOrClient;
                }
                else
                {
                    richTextBoxMessages.AppendText("Второй канал для этого пользователя " + newSocket.RemoteEndPoint + " подключен!\n");
                    treeOrClient = !treeOrClient;
                }
            }
        }

        // Отправка директорий
        private void SendDirectoriesToClient(string path)
        {
            string list = "";
            if (DirOrDirInfo)
            {
                string[] drivesArray = Directory.GetLogicalDrives();

                for (int i = 0; i < drivesArray.Length; i++)
                    list = list + drivesArray[i] + "-";

                list = list.Remove(list.Length - 1, 1);

                DirOrDirInfo = false;
            }
            else
            {
                DirectoryInfo di = new DirectoryInfo(path);
                DirectoryInfo[] diArray;
                FileInfo[] fArray;

                try
                {
                    diArray = di.GetDirectories();
                    fArray = di.GetFiles();
                }
                catch
                {
                    return;
                }

                for(int i=0;i<diArray.Length;i++)
                    list = list + diArray[i].Name + "-";

                for (int i = 0; i < fArray.Length; i++)
                    list = list + fArray[i].Name + "-";

                list = list.Remove(list.Length - 1, 1);
            }

            bool clientOrTree = true;
            //прохождение всех клиентов
            foreach (Socket socket in sockets)
            {
                if (!clientOrTree)
                {
                    try
                    {
                        // преобразуем текст в байты
                        byte[] data = Encoding.UTF8.GetBytes(list);

                        // отправка
                        socket.Send(data);
                    }
                    catch (Exception ex)
                    {
                        richTextBoxMessages.AppendText(ex.Message + "\n");
                    }
                }
                clientOrTree = !clientOrTree;
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            try
            {
                SendToClient("disconnect");
                sockets.Clear();
                treeOrClient = true;
                DirOrDirInfo = true;


                richTextBoxMessages.AppendText("Все клиенты отключены!\n");
                //if (sockets.Count < 0)
                //    buttonClear.Enabled = false;
            }
            catch (Exception ex)
            {
                richTextBoxMessages.AppendText(ex.Message + "\n");
            }
        }
    }
}