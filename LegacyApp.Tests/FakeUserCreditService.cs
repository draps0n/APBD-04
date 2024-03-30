using System;
using System.Collections.Generic;
using System.Threading;

namespace LegacyApp.Tests;

public class FakeUserCreditService : IUserCreditService
{
    private readonly Dictionary<string, int> _database =
        new Dictionary<string, int>()
        {
            {"Kowalski", 200},
            {"Malewski", 20000},
            {"Smith", 10000},
            {"Doe", 3000},
            {"Kwiatkowski", 1000}
        };
    
    public int GetCreditLimit(string lastName, DateTime dateOfBirth)
    {
        if (_database.ContainsKey(lastName))
            return _database[lastName];

        throw new ArgumentException($"Client {lastName} does not exist");
    }
}