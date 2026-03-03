using API.Controllers;
using API.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace ComputerFactory.Model
{
    internal class ComputerFactoryAPI
    {
        private static readonly HttpClient _httpClient = new HttpClient();
        
        

        public static async Task LoadPerson(ListView ClientList)
        {
            string url = "http://localhost:58502/api/GetClients";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<Client>>(json);
            ClientList.ItemsSource = data;
        }

        public static async Task LoadPersonByName(ListView ClientList, string name)
        {
            if(string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Название должно быть заполнено");
                return;
            }
            string url = $"http://localhost:58502/api/ClientByName?name={name}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            string json = await response.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<Client>>(json);
            ClientList.ItemsSource = data;
        }
        public static async Task AddPerson(Client client)
        {
            string url = "http://localhost:58502/api/Clients";
            string json = JsonConvert.SerializeObject(client);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = await _httpClient.PostAsync(url, content);
            request.EnsureSuccessStatusCode();
            



        }

        public static async Task DeletePerson(int clientId)
        {
            string url = $"http://localhost:58502/api/Clients/?id={clientId}";
            ClientsController controller = new ClientsController();
            
            var response = await _httpClient.DeleteAsync(url);
            response.EnsureSuccessStatusCode();

            
            
        }

        public static async Task UpdatePerson(Client client)
        {
            string url = $"http://localhost:58502/api/PutClients?id={client.client_id}";
         
            

            string json = JsonConvert.SerializeObject(client);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(url, content);
            response.EnsureSuccessStatusCode();

        }

       
    }
}
