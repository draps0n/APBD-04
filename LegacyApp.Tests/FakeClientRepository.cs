using System;
using System.Collections.Generic;

namespace LegacyApp.Tests;

public class FakeClientRepository : IClientRepository
{
    
    public static readonly Dictionary<int, Client> Database = new Dictionary<int, Client>()
    {
        {1, new Client(1, "Kowalski", "Warszawa, Złota 12", "kowalski@wp.pl", "NormalClient")},
        {2, new Client(2, "Malewski", "Warszawa, Koszykowa 86", "malewski@gmail.pl", "VeryImportantClient")},
        {3, new Client(3, "Smith", "Warszawa, Kolorowa 22", "smith@gmail.pl", "ImportantClient")},
        {4, new Client(4, "Doe", "Warszawa, Koszykowa 32", "doe@gmail.pl", "ImportantClient")},
        {5, new Client(5, "Kwiatkowski", "Warszawa, Złota 52", "kwiatkowski@wp.pl", "NormalClient")},
        {6, new Client(6, "Andrzejewicz", "Warszawa, Koszykowa 52", "andrzejewicz@wp.pl", "NormalClient")}
    };
    
    public Client GetById(int clientId)
    {
        if (Database.ContainsKey(clientId))
            return Database[clientId];

        throw new ArgumentException($"User with id {clientId} does not exist in database");
    }
}