using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI.Shared.Services
{
    public interface ILLMService
    {
        Task<string> GenerateAsync(string prompt);
    }
}