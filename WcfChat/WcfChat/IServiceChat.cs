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
        int Connect(string name); 

        [OperationContract] 
        void Disconnect(string msg, int id); 
        [OperationContract(IsOneWay =true)]
        void SendMsg(string msg, int id);  


    }

    public interface IServerChatCallback
    {
        [OperationContract(IsOneWay =true)] 
        void MsgCallback(string msg); 
            
    }
}
