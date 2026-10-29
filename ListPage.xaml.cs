using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
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

namespace pract5_trpo
{
    /// <summary>
    /// Логика взаимодействия для ListPage.xaml
    /// </summary>
    public partial class ListPage : Page
    {
        public ObservableCollection<ExamWork> Works { get; set; } = new();
        public ExamWork? SelectedWork { get; set; }

        public ListPage()
        {
            InitializeComponent();
            LoadAll();
            DataContext = this;
        }
        private void LoadAll()
        {
            foreach (var file in Directory.GetFiles(".", "*.json"))
            {
                string json = File.ReadAllText(file);
                var work = JsonSerializer.Deserialize<ExamWork>(json);
                if (work != null) Works.Add(work);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ExamWorkFormPage(Works));
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (SelectedWork == null)
            {
                MessageBox.Show("Запись не выбрана");
                return;
            }

            var result = MessageBox.Show("Вы точно хотите удалить запись?", "Подтверждение", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                File.Delete($"{SelectedWork.Id}.json");
                Works.Remove(SelectedWork);
            }
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedWork == null)
            {
                MessageBox.Show("Не выбран элемент списка");
                return;
            }
            NavigationService.Navigate(new ExamWorkFormPage(Works, SelectedWork));
        }
    }
    
}
