using System;
using System.Collections.Generic;
using System.Data.Common;
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
using System.Configuration;

namespace PseudoCloudStore
{
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public Users usertmp;
            
        public Login()
        {
            InitializeComponent();

        }

        private DbConnection CreateConnection()
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["ConnectionStringName"].ProviderName);
            DbConnection connection = providerFactory.CreateConnection();

            connection.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionStringName"].ConnectionString;
            return connection;
        }

        private void btnReg_Click(object sender, RoutedEventArgs e)
        {
            RegistrPage registrPage = new RegistrPage();
            this.NavigationService.Navigate(registrPage);
        }

        public void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            List<Users> users = new List<Users>();
            using (DbConnection connection = CreateConnection())
            {
                connection.Open();
                DbCommand command = connection.CreateCommand();
                command.CommandText = "select * from Users";

                DbDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    users.Add(
                        new Users
                        {
                            Id = (int)reader["Id"],
                            Login = reader["Login"].ToString(),
                            Password = reader["Password"].ToString()
                        });
                }
            }

            
            string login = txtboxLoginLgnPg.Text;
            string password = txtboxPswordLgnPg.Text;

            foreach (var item in users)
            {
                if (item.Login == login)
                {
                        
                    UserFilesPage usrFilesPage = new UserFilesPage(login);
                    this.NavigationService.Navigate(usrFilesPage);
                }

            }
            
        }
    }
}
