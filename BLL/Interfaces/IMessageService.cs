using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IMessageService
    {
        void MakeMessage(MessageDTO orderDto);
        MessageDTO GetMessage(int? id);
        IEnumerable<MessageDTO> GetMessage();
        void SaveUpdate(MessageDTO orderDto);
        void Dispose();
    }
}
