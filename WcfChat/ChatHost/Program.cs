using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;   

// создем хост в общем проекте .

// добавляем в ссылки System service model (данная библиотека отвечает за wcf)
// также добавить в ссылки к хосту проект wcf_chat (коорый явл сервисом) 

namespace ChatHost
{
    class Program
    {
        static void Main(string[] args)
        {
            // исп юзинг коорый реализует dispose у обьекта сервис хост для освобождения ресурсов .
            // запускать хост нужно от имени администратора (или чеез папку Debug ChatHost.exe) или запускать VS от имени администратора 
            using (var host = new ServiceHost(typeof(WcfChat.ServiceChat))) // ServiceHost: Инициализирует 
                                                                             // новый экземпляр класса System.ServiceModel.ServiceHost 
                                                                             // с указанным типом службы и базовыми адресами.
                                                                             // по ошибке здесь в качетве параетра передавал IServiceChat 
                                                                             // typeof(WcfChat.IServiceChat))) тем смым получал 
                                                                             // ошибку при попытке зпустить хост: Директива ServiceHost поддерживает только типы службы классов.
            {
                host.Open(); // открываем хост
                Console.WriteLine("Хост стартовал");
                Console.ReadLine();
                //host.Close();
            }

        }
    }
}
