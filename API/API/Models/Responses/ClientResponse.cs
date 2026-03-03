using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models.Responses
{
    public class ClientResponse
    {
        

        public ClientResponse(string name, string street, string houseNumber)
        {
            this.name = name;
            this.street = street;
            this.houseNumber = houseNumber;
        }

        public string name { get; set; }
        public string street { get; set; }
        public string houseNumber { get; set; }
    }
}