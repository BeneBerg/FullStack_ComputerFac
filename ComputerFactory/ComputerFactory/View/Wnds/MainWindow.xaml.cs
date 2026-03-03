using API.Models;
using ComputerFactory.Model;
using ComputerFactory.View.Wnds;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ComputerFactory
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var loadingWnd = new LoadingWnd();
            loadingWnd.Show();

            try
            {
                
                    await ComputerFactoryAPI.LoadPerson(ClientList);
                   
                


            }
            finally
            {
                loadingWnd.Close(); 
            }

        }

        private async void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            
            await ComputerFactoryAPI.LoadPersonByName(ClientList, SearchClient.Text);
            if(ClientList.Items.Count == 0)
            {
                NothingFound.Visibility = Visibility.Visible;

            }
            else NothingFound.Visibility = Visibility.Collapsed;

        }

        private async void BtnAddClient_Click(object sender, RoutedEventArgs e)
        {
            var addClientWnd = new AddClientWnd();
            addClientWnd.ShowDialog();
            await ComputerFactoryAPI.LoadPerson(ClientList);
        }

        private async void BtnDeleteClient_Click(object sender, RoutedEventArgs e)
        {
            var selected = ClientList.SelectedItem as Client;
            
            if (selected == null)
            {
                MessageBox.Show("Выберите клиента, которого хотите удалить.");
                return;   
            }
            if(MessageBox.Show("Вы уверены, что хотите удалить клиента?", "Подтверждение",                          
                MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                await ComputerFactoryAPI.DeletePerson(selected.client_id);
                await ComputerFactoryAPI.LoadPerson(ClientList);
            }
            
            
        }

        private async void BtnUpdateClient_Click(object sender, RoutedEventArgs e)
        {
            var selected = ClientList.SelectedItem as Client;
            if (selected == null)
            {
                MessageBox.Show("Для редактирования клиента необходимо его выбрать");
                return;
            }
            var UpdateClientWnd = new UpdateClientWnd(selected);
            UpdateClientWnd.ShowDialog();
            await ComputerFactoryAPI.LoadPerson(ClientList);



        }


        private async void BtnAllClients_Click(object sender, RoutedEventArgs e)
        {

            await ComputerFactoryAPI.LoadPerson(ClientList);
            NothingFound.Visibility = Visibility.Collapsed;

        }
    }
}
