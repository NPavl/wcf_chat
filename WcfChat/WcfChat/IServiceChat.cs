using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfChat
{
   
    [ServiceContract(CallbackContract = typeof(IServerChatCallback))]
    public interface IServiceChat
    {
        [OperationContract] 
        int Connect(string name); // с помощью этого метода будем подключаться к сервису.

        [OperationContract] 
        void Disconnect(string msg, int id); // будет вызыватся в момент когда клиент покидает чат (параметром принимает id)

        [OperationContract(IsOneWay =true)]
        void SendMsg(string msg, int id); //  


    }

    public interface IServerChatCallback
    {
        [OperationContract(IsOneWay =true)] // также IsOneWay true иначе сервер будет ожидать ответа от клиента получил оли он callback. сервер просто должен делать рассылку.
        void MsgCallback(string msg); // данный метод возвращает сообщение клиентам 
            
    }
}
