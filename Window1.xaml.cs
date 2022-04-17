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

namespace ParsingApp
{
    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        private struct LVData
        {
            public string Key { get; set; }
            public string Value { get;set; }
        }
        public Window1()
        {
            InitializeComponent();
        }
        public void ShowData(ParsingApp.Data data)
        {
            LVData item = new LVData();
            this.Visibility = Visibility.Visible;
            item.Key = "Идентификатор УБИ";
            item.Value = data.Indicator;
            listView1.Items.Add(item);
            item.Key = "Наименование УБИ";
            item.Value = data.NameOfUBI;
            listView1.Items.Add(item);
            item.Key = "Описание";
            item.Value = data.Description;
            listView1.Items.Add(item);
            item.Key = "Источник угрозы (характеристика и потенциал нарушителя)";
            item.Value = data.ThreatSource;
            listView1.Items.Add(item);
            item.Key = "Объект воздействия";
            item.Value = data.ImpactObject;
            listView1.Items.Add(item);
            item.Key = "Нарушение конфиденциальности";
            item.Value = data.ConfederationViolation;
            listView1.Items.Add(item);
            item.Key = "Нарушение целостности";
            item.Value = data.IntegrityViolation;
            listView1.Items.Add(item);
            item.Key = "Нарушение доступности";
            item.Value = data.AccessViolation;
            listView1.Items.Add(item);
            item.Key = "Дата включения угрозы в БнД УБИ";
            item.Value = data.InclusionDate;
            listView1.Items.Add(item);
            item.Key = "Дата последнего изменения данных";
            item.Value = data.ChangeDate;
            listView1.Items.Add(item);
        }
    }
}
