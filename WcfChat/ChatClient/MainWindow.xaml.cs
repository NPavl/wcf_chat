using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ChatClient.ServiceChat;

namespace ChatClient
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml:
    /// клиентская часть (функционал с графическим интерфейсом)
    /// 
    /// </summary>
    public partial class MainWindow : Window, IServiceChatCallback
    // дополнительно ChatClient.ServiceChat.IServiceChatCallback данной строчкой пишем полный путь и 
    // подключаем интерфейс IServiceChatCallback и внизу реализовываем его MsgCallback (данный метод 
    // возвращает сообщение клиентам)
    {
        bool isConnected = false; // отвечает за подлючен в данный момент клиент к сервису или нет

        ServiceChatClient client; // создаем обьект типа хоста в клиенте что бы могли взаимодействовать 
        // с его методами. так как мы его подключили в ссылках значит можем с ним работать.
        int ID;

        // обработаем событие Window_Loaded 
        private void Window_Loaded(object sender, RoutedEventArgs e) // подключение будет 
        // происходить в момент вызова собыия у основного окна Window_Loaded. 
        // тоесть когда клиент уже будет загружен 
        {
            client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this)); // когда клиент 
            // будет загружен будем создавать и выделять 
            // память под обьект ServiceChat.ServiceChatClient. в паремтры передаем обьект   
            // типа instanceContext для того чтобы мы могли вызывать коллбеки. так как наше окно 
            // MainWindow реализовывает интерфейс IServiceChatCallback то в парметре instanceContext 
            // мы используем this тоесь прямо сюда и передаем.
        }

        // подпишемся на еще одно событие , когда окно будет закрыватся мы выполним метод DisconnectUser 
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        // обработка событий нажатия клавиш обьекта в окне
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            // делаем проверку какая клавиша нажата: 

            if (e.Key == Key.Enter) // если нажата enter (значит ввели соощение его нужно отправить)
            {

                if (client != null) // тоесть мы подключились  такиом случае отправляем сообщение 
                {
                    client.SendMsg(tbMessage.Text, ID); // в таком случае вызываем метод SendMsg. в парамерапердаем сообщение кооре находится в textbox massage
                    tbMessage.Text = string.Empty; // поещаем в textbox пустую строку после того как отправили сообщение    
                }
            }
        }

        // данный метод будет создава и проверять подключен ли юзер в данный момент 
        void ConnectUser() // если не подключен то 
        {
            if (!isConnected)
            {
                ID = client.Connect(tbUserName.Text); // в момент коннекта присваеваем полю ID присваивать 
                                                      // значение которое вернул этот метод. в параметры передаем имя клиента который конектится 
                tbUserName.IsEnabled = false; // блокирует возможность изменить имя юзера когда мы подключены 
                ConnectDisconnect.Content = "Disconnect"; // будем присваивать контент (текст) в том случае если мы уже нажали и подключились
                isConnected = true; // подключить его
            }
        }

        // метод отключает юзера 
        void DisconnectUser()

        {
            if (isConnected) // исли мы уже подключились 
            {
                client.Disconnect(Name, ID); // в дисконнект передаем ID который получили из метода коннект  
                client = null; // поле того как выполняем disconnect клиенту присваем нал   
                tbUserName.IsEnabled = true; // наоборот если дисконнект то мы можем поменять имя юзера
                ConnectDisconnect.Content = "Connect"; // то меняем текст кнопки на Connect
                isConnected = false; // отключать юзера
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected) // если мы подключены то 
            {
                DisconnectUser(); // выполняем метод дисконнект 
            }
            else // если не подключены - подключаемся 
            {
                ConnectUser();
            }
        }

        public void MsgCallback(string msg)
        {
            lbChat.Items.Add(msg); // добавлем новый item в котором будут хранится сообщения. 
            // каждый раз когда сервер будет присылать сообщения, в ListBox будет отображатся данное сообщение.
            lbChat.ScrollIntoView(lbChat.Items[lbChat.Items.Count - 1]); // решаем проблему с 
            // прокруткой сообщений, указах в парметрах
            // элимент до которго мы хотим его проручивать [получим 
            // количество элментво в списке и вычтем из него 1] 
        }


        //private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        //{
        //    tbUserName.Text = " ";
        //}

        //private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        //{
        //    tbMessage.Text = " ";
        //}


    }
}
