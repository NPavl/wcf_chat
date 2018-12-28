using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

// создаем класс в котором будем храниить id текущего юзера , его имя и поле oprationcontext - поле 
// в коорм будет содерж инф о подключении клиента к нашему сервису


namespace WcfChat
{
    public class ServerUser
    {
        public int ID { get; set; }
        public string Name { get; set; }

        public OperationContext operationContext { get; set; }
     

    }
}
