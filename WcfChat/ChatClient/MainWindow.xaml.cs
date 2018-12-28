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
    /// 
    /// клиентская часть (функционал с графическим интерфейсом)
    /// 
    /// </summary>
    public partial class MainWindow : Window, IServiceChatCallback
    
    {
        bool isConnected = false; 
        ServiceChatClient client;
        int ID;
    
        private void Window_Loaded(object sender, RoutedEventArgs e) 
       
        
        {
            client = new ServiceChatClient(new System.ServiceModel.InstanceContext(this)); 
        }

               private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DisconnectUser();
        }

        
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            

            if (e.Key == Key.Enter) 
            {

                if (client != null) 
                {
                    client.SendMsg(tbMessage.Text, ID); 
                    tbMessage.Text = string.Empty;   
                }
            }
        }

       
        void ConnectUser() 
        {
            if (!isConnected)
            {
                ID = client.Connect(tbUserName.Text); 
                tbUserName.IsEnabled = false; 
                ConnectDisconnect.Content = "Disconnect"; 
                isConnected = true; 
            }
        }

        
        void DisconnectUser()

        {
            if (isConnected) 
            {
                client.Disconnect(Name, ID); 
                client = null;  
                tbUserName.IsEnabled = true; 
                ConnectDisconnect.Content = "Connect"; 
                isConnected = false; 
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (isConnected)  
            {
                DisconnectUser(); 
            }
            else 
            {
                ConnectUser();
            }
        }

        public void MsgCallback(string msg)
        {
            lbChat.Items.Add(msg);  
            lbChat.ScrollIntoView(lbChat.Items[lbChat.Items.Count - 1]); 
            
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
