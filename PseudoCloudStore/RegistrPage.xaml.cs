using System;
using System.Windows;
using System.Collections.Generic;
using System.Configuration;
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

namespace PseudoCloudStore
{
    /// <summary>
    /// Логика взаимодействия для RegistrPage.xaml
    /// </summary>
    public partial class RegistrPage : Page
    {
        public RegistrPage()
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
            string login = txtboxLoginRegPg.Text;
            string password = txtboxPswordRegPg.Text;

            using (DbConnection connection = CreateConnection())
            {
                connection.Open();
                DbTransaction transaction = connection.BeginTransaction();

                DbCommand command = connection.CreateCommand();
                command.Transaction = transaction;

                try
                {
                    command.CommandText = String.Format("INSERT INTO Users VALUES ('{0}','{1}')", login, password);

                    command.ExecuteNonQuery();

                    transaction.Commit();
                }
                catch (DbException ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(ex.Message);
                }
            }
            Login loginPage = new Login();
            this.NavigationService.Navigate(loginPage);
        }
    }
}
