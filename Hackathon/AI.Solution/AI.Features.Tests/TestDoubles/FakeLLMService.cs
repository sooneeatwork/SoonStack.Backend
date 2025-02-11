using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AI.Shared.Services;

namespace AI.Features.Tests.TestDoubles;

public class FakeLLMService : ILLMService
{
    private readonly string _responseToReturn;

    public FakeLLMService(string responseToReturn = "Test analysis result")
    {
        _responseToReturn = responseToReturn;
    }

    public Task<string> GenerateAsync(string prompt)
    {
        return Task.FromResult(_responseToReturn);
    }
}