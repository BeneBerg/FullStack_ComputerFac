using API.Models;
using ComputerFactory.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ComputerFactory.View.Wnds
{
    /// <summary>
    /// Логика взаимодействия для UpdateClientWnd.xaml
    /// </summary>
    public partial class UpdateClientWnd : Window
    {
        private Client _client;
        public UpdateClientWnd(Client client)
        {
            InitializeComponent();
            _client = client;

            companyName.Text = client.name;
            streetText.Text = client.street;
            houseNum.Text = client.houseNumber;
        }

        private async void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
           _client.name = companyName.Text;
           _client.street = streetText.Text;
           _client.houseNumber = houseNum.Text;

            await ComputerFactoryAPI.UpdatePerson(_client);
            MessageBox.Show("Данные обновлены");
            this.Close();
            
        }
    }
}
