using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfChat
{

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)] // Single - все клиенты которые буду подлюч к хосту бдут рабоать с данным сервисом
    public class ServiceChat : IServiceChat
    {
        List<ServerUser> users = new List<ServerUser>();  // создем список объектов ServerUser
        int nextId = 1; // переменная для генерации id 

        #region метод Connect
        public int Connect(string name) // входящим параметром юзер коорго подключаем
        {
            ServerUser user = new ServerUser()
            {
                ID = nextId,  // создаем нового юзера с полем ID созначением перменной nextId
                Name = name, // присваеваем имя 
                operationContext = OperationContext.Current // информацию для этого поля мы будем брат из тех данных которые будут поступать вместе с коннеком юзера.
            };
            // после того как присвоен ID юзеру ножно увеличить поле на nextId на 1 (значение поля id слудущего юзера должно быть другое)
            nextId++;
            SendMsg(": " + user.Name + " " + " user connect to chat ", 0); // оповещение что новый юзер подключен в наш чат . ворым параметом 0,  генерация начинается с 1 int nextId = 1;
            users.Add(user); // после зоздания юзера , нужно добавить его в список юзеров (чобы сервис знал какие у нс есть юзеры)
            return user.ID; // возвашщает коннект сгенерированный айдишник юзера .
            #endregion

        }

        #region Disconnect
        public void Disconnect(string msg, int id) // параметром прнимает id того юзера коорго нужно отключить. потому что клиент будет знать свой id после подключения етод conect мы возвращаем ему id коорый ем сгенерировали.
        {
            // для того чтобы убат юзера из спика его нужно сначало найти: 
            var user = users.FirstOrDefault(i => i.ID == id); // находим в юзерах элемент i=> у коорго поле ID равно тому id коорое мы полули входящим параметом в етод disconnect
            if (user != null) // еесли ам такого юзера нет 
            {
                users.Remove(user); // удалить найденного юзера
                SendMsg(": "+ user.Name + " " + "user out of chat ", 0); // после удаления шлем сообщение всем юзерам что юзер удален
            }
        }
        #endregion

        public void SendMsg(string msg, int id)
        {
            // переберем в цикле всех юзеров : 

            foreach (var item in users) // сообщение от сервера для всех юзеров
            {
                string answer = DateTime.Now.ToShortTimeString(); // когда было послано сообщение текущее время.
                var user = users.FirstOrDefault(i => i.ID == id); // находим в юзерах элемент i=> у коорго поле ID равно тому id коорое мы полули входящим параметом в етод disconnect
                if (user != null) // если мы нашли юзера  
                {
                    answer += ": " + user.Name + " "; // то дабавим в переменную answer имя данного юзера
                }
                answer += msg; // добавим сообщение коорое пришло входящим параметром.

                // поле того ак мы сформировали сообщение нам нужно оправить это сообщение екущеу юзеру с коорым мы работаем
                // в нашем цикле. для того бы оправить сообщене у нас есть метод Callback - реализаци буде на сороне клиента 
                // здсь же его нжно прсто вызвать.

                // обращаемся к полю operationContext вызываем метод GetCallbackChannel передаем ему интерфейс 
                // IServerChatCallback коорый должен реализовывать Callback, вызывать меотод коорый есть в этом инерфейсе
                // MsgCallback с параметром получит сооббщение на стороне клинета кооре мы ему сформировали (answer) наш ответ
                item.operationContext.GetCallbackChannel<IServerChatCallback>().MsgCallback(answer);

            }

        }

    }

}
