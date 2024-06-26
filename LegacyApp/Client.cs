﻿namespace LegacyApp
{
    public class Client
    {
        public string Name { get; internal set; }
        public int ClientId { get; internal set; }
        public string Email { get; internal set; }
        public string Address { get; internal set; }
        public string Type { get; set; }
        
        public Client(){}

        public Client(int clientId, string name, string address, string email, string type)
        {
            Name = name;
            ClientId = clientId;
            Email = email;
            Address = address;
            Type = type;
        }
    }
}