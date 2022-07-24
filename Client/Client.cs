using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Client
{
    public partial class Client : Form
    {
        private Socket socket;
        private Socket socketForTree;
        private string host ="0";
        private const int port = 8080;

        public Client()
        {
            InitializeComponent();

            // Недоступные кнопки
            buttonDisconnect.Enabled = false;
            button2.Enabled = false;
            buttonSend.Enabled = false;

        }

        private void DriveTreeInit()
        {
            string[] drivesArray = GetListDirAndFiles("-");
            if (drivesArray.Length == 1 && drivesArray[0] == "-")
                return;

            treeViewFiles.BeginUpdate();
            treeViewFiles.Nodes.Clear();

            foreach (string s in drivesArray)
            {
                TreeNode drive = new TreeNode(s, 0, 0);
                treeViewFiles.Nodes.Add(drive);

                GetDirs(drive);
            }

            treeViewFiles.EndUpdate();
        }
        private void GetDirs(TreeNode node)
        {
            node.Nodes.Clear();

            string fullPath = node.FullPath;

            string[] list = GetListDirAndFiles(fullPath);
            if (list.Length == 1 && list[0] == "-")
                return;

            for(int i = 0;i<list.Length;i++)
            {
                TreeNode dir = new TreeNode(list[i], 0, 0);
                node.Nodes.Add(dir);
            }
        }

        // Процесс отправки на сервер
        private void SendReceiveTreeToServer(string message)
        {
            // переводим в байты
            byte[] data = Encoding.UTF8.GetBytes(message);

            // передаем сообщение на сервер
            socketForTree.Send(data);
        }

        string[] GetListDirAndFiles(string message)
        {
            SendReceiveTreeToServer(message);
            //richTextBoxMessages.AppendText("Ожидание ответа сервера на доступ к каталогу\n");
            Thread.Sleep(1000);
            try
                {
                    if (socketForTree.Available > 0) // есть ли новые данные
                    {
                        // буффер для получения данных 
                        byte[] data = new byte[socketForTree.Available];

                        // получаем данные
                        int data_size = socketForTree.Receive(data);

                        // преобразование данных в текст
                        string text_data = Encoding.UTF8.GetString(data, 0, data_size);

                        string[] list = text_data.Split('-');

                        return list;
                    }
                }
                catch (Exception ex)
                {
                    richTextBoxMessages.AppendText(ex.Message + "\n");

                }
            string[] list_ = { "-" };
            return list_;
        }

        private void treeViewFiles_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            treeViewFiles.BeginUpdate();

            foreach (TreeNode node in e.Node.Nodes)
            {
                GetDirs(node);
            }

            treeViewFiles.EndUpdate();
        }

        // Процесс отправки на сервер
        private void SendToServer(string message)
        {
            // переводим в байты
            byte[] data = Encoding.UTF8.GetBytes(message);

            // передаем сообщение на сервер
            socket.Send(data);
        }

        // Независимая проверка программы
        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
               if(socket.Available > 0) // есть ли новые данные
                {
                    // буффер для получения данных 
                    byte[] data = new byte[socket.Available];

                    // получаем данные
                    int data_size = socket.Receive(data);

                    // преобразование данных в текст
                    string text_data = Encoding.UTF8.GetString(data, 0, data_size);
                    if(text_data == "disconnect")
                    {
                        richTextBoxMessages.AppendText("Вы были отключены сервером\n");

                        buttonDisconnect_Click_1(sender, e);
                    }
                    else
                        richTextBoxMessages.AppendText("Ответ сервера:\n" + text_data + "\n");
                }
            }
            catch (Exception ex)
            {
                richTextBoxMessages.AppendText(ex.Message + "\n");
            }
        }

        // Отправка сообщений серверу
        private void buttonSend_Click(object sender, EventArgs e)
        {
            try
            {
                // посылаем серверу сообщение
                if (treeViewFiles.SelectedNode.Text.EndsWith(".txt"))
                {
                    SendToServer(treeViewFiles.SelectedNode.FullPath);

                    // выводим информацию
                    richTextBoxMessages.AppendText("Запрос на чтение файла отправлен серверу\n");
                }
                else richTextBoxMessages.AppendText("Данный тип файлов не доступен для чтения\n");
            }
            catch (Exception ex)
            {
                richTextBoxMessages.AppendText(ex.Message + "\n");
            }
        }

        // Отключение от сервера
        private void buttonDisconnect_Click_1(object sender, EventArgs e)
        {
            try
            {
                SendToServer("Клиент отключен!");

                // закрываем получение и отправку данных
                socket.Shutdown(SocketShutdown.Both);

                // закрываем сокет
                socket.Close();

                timer1.Enabled = false;

                richTextBoxMessages.AppendText("Отключено\n");

                button1.Enabled = true;
                buttonDisconnect.Enabled = false;
                button2.Enabled = false;
                buttonSend.Enabled = false;
                treeViewFiles.Nodes.Clear();

                con_discon.Text = "Disconnected";
                con_discon.ForeColor = Color.Gray;
            }
            catch (Exception ex)
            {
                richTextBoxMessages.AppendText(ex.Message + "\n");
            }
        }

        // Подключение к серверу
        private void button1_Click(object sender, EventArgs e)
        {
            host = textBoxIP.Text;

            try
            {
                // создаем новый сокет
                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // создаем новый сокет для дерева
                socketForTree = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                // подключаемся к серверу
                socket.Connect(host, port);
                socketForTree.Connect(host, port);

                // таймер для переодической проверки входящих сообщений
                timer1.Enabled = true;

                // вывод текста об успешном подключении
                richTextBoxMessages.AppendText("Подключено к " + host + ":" + port.ToString() + "\n");
                button1.Enabled = false;
                buttonDisconnect.Enabled = true;
                button2.Enabled = true;
                buttonSend.Enabled = true;

                con_discon.Text = "Connected";
                con_discon.ForeColor = Color.Green;
            }
            catch (Exception ex)
            {
                richTextBoxMessages.AppendText(ex.Message + "\n");
            }
        }

        private void button2_MouseClick(object sender, MouseEventArgs e)
        {
            if (socketForTree != null)
                DriveTreeInit();
            richTextBoxMessages.AppendText("Загрузка основных дисков\n");
        }
    }
}
