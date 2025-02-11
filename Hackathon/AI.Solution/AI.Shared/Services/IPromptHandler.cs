using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AI.Shared.Services
{
    public interface IPromptHandler
    {
        Task<string> FormatPromptAsync(string prompt);
    }
}