using API.Models;
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
using ComputerFactory.Model;

namespace ComputerFactory.View.Wnds
{
    /// <summary>
    /// Логика взаимодействия для AddClientWnd.xaml
    /// </summary>
    public partial class AddClientWnd : Window
    {

        
        public AddClientWnd()
        {
            InitializeComponent();
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            string nameT = companyName.Text;
            string streetT = streetText.Text;
            string houseT = houseNum.Text;

            Client client = new Client
            {
                name = nameT,
                street = streetT,
                houseNumber = houseT
            };
            await ComputerFactoryAPI.AddPerson(client);
            this.Close();
            
        }

       
    }
}
