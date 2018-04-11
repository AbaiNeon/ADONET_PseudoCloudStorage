using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.IO;
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
using Microsoft.Win32;

namespace PseudoCloudStore
{
    /// <summary>
    /// Логика взаимодействия для UserFilesPage.xaml
    /// </summary>
    public partial class UserFilesPage : Page
    {

        private string Login { get; set; }
        public UserFilesPage()
        {
            InitializeComponent();
        }

        public UserFilesPage(string _login) //:this()
        {
            InitializeComponent();
            try
            {
                Login = _login;
                lblUserName.Content = "User: " + _login;

            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

            string dir = @"C:\CloudStorage\" + Login + @"\";

            if (Directory.Exists(dir))
            {
                listboxUserFilePg.ItemsSource = Directory.GetFiles(dir);
            }

            
        }

        private DbConnection CreateConnection()
        {
            DbProviderFactory providerFactory = DbProviderFactories.GetFactory(ConfigurationManager.ConnectionStrings["ConnectionStringName"].ProviderName);
            DbConnection connection = providerFactory.CreateConnection();

            connection.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionStringName"].ConnectionString;
            return connection;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openfileDialog = new OpenFileDialog();
            openfileDialog.ShowDialog();
            string fullFileName = openfileDialog.FileName;
            string shortFileName = openfileDialog.SafeFileName;

            //using (DbConnection connection = CreateConnection())
            //{
            //    connection.Open();
            //    DbTransaction transaction = connection.BeginTransaction();

            //    DbCommand command = connection.CreateCommand();
            //    command.Transaction = transaction;

            //    try
            //    {
            //        command.CommandText = String.Format("INSERT INTO Users VALUES ('{0}','{1}')", fullFileName, user_id);

            //        command.ExecuteNonQuery();

            //        transaction.Commit();
            //    }
            //    catch (DbException ex)
            //    {
            //        transaction.Rollback();
            //        MessageBox.Show(ex.Message);
            //    }
            //}
            //не успел

            //-----------------------------------------------------------------------------------------------------------------------------
            if (!(Directory.Exists(@"C:\CloudStorage")))
            {
                Directory.CreateDirectory(@"C:\CloudStorage");
            }
            
            //создаем дирректорию Login1
            string dir = @"C:\CloudStorage\" + Login + @"\";

            if (!(Directory.Exists(dir)))
            {
                Directory.CreateDirectory(dir);
            }

            string resultDestPath = dir + shortFileName; //destFileName

            File.Copy(fullFileName, resultDestPath);

            if (Directory.Exists(dir))
            {
                listboxUserFilePg.ItemsSource = Directory.GetFiles(@"C:\CloudStorage\" + Login + @"\");
            }
        }

        private void btnDel_Click(object sender, RoutedEventArgs e)
        {

            //не успел
        }
    }
}
