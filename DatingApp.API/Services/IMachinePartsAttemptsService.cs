﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DatingApp.Core.Models;

namespace DatingApp.API.Services
{
    public interface IMachinePartsAttemptsService
    {
        Task<MachinePartAttempt> GetMachinePartAttempt(int id);
        Task<List<MachinePartAttempt>> GetMachinePartsAttempts();   
    }
}
