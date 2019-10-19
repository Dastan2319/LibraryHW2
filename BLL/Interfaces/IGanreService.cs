﻿using BLL.DTO;
using DLL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IGanreService
    {
        void MakeGanre(GanreDTO orderDto);
        GanreDTO GetGanre(int? id);
        IEnumerable<GanreDTO> GetGanre();
        void SaveUpdate(Ganre orderDto);
        void Dispose();
    }
}
