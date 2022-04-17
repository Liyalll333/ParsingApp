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
using System.Windows.Navigation;
using System.Windows.Shapes;

//Microsoft.Office.Interop.Excel;

namespace ParsingApp
{
    enum TypeOfTable
    {
        full,
        minimize
    }
    public partial class MainWindow : Window
    {
        ExcelParser parser = new ExcelParser();
        Data[] data;
        public MainWindow()
        {
            InitializeComponent();
            if (!parser.CheckFileExist())
            {
                if (!ShowInitMessage())
                    Environment.Exit(1);

                var status = parser.LoadFromSite();
                if (status.status == ExcelParser.LoadStatus.error)
                {
                    MessageBox.Show("Ошибка при первоначальной загрузк файла: " + status.error, "Ошибка загрузки", MessageBoxButton.OK);
                    Environment.Exit(1);
                }
                MessageBox.Show("Загрузка файла успешно завершена", "Загрузка успешно завершена", MessageBoxButton.OK);

            }
            data = parser.FromExcel();
            LoadToListView();

        }



        private void Clicker(object sender, RoutedEventArgs e)
        {

        }
        private bool ShowInitMessage()
        {
            MessageBoxResult result = MessageBox.Show("Файл не загружен или повреждён. Выполнить загрузку файла?", "Первоначальная загрузка файла", MessageBoxButton.YesNo);
            switch (result)
            {
                case MessageBoxResult.Yes:
                    return true;

                case MessageBoxResult.No:
                    MessageBox.Show("Продолжение работы невозможно", "Ошибка", MessageBoxButton.OK);
                    return false;
            }
            return false;
        }
        private void LoadToListView()
        {
            if (data == null)
            {
                MessageBox.Show("Продолжение работы невозможно: ошибка при работе с файлом", "Ошибка", MessageBoxButton.OK);
                Environment.Exit(1);
            }
            foreach (var element in data)
                listView1.Items.Add(element);

        }

        private void listView1_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var index = listView1.SelectedIndex;
            if (index == -1)
                return;
            var a = new Window1();
            a.ShowData(data[index]);
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var status = parser.LoadFromSite();
            if (status.status == ExcelParser.LoadStatus.error)
            {
                MessageBox.Show("Ошибка при загрузки файла: " + status.error, "Ошибка загрузки", MessageBoxButton.OK);
                return;
            }
            data = parser.FromExcel();
            LoadToListView();
            MessageBox.Show("Загрузка файла успешно завершена", "Загрузка успешно завершена", MessageBoxButton.OK);
        }
    }


}

