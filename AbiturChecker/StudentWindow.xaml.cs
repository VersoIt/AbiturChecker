using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using AbiturChecker.Core;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AbiturChecker
{
    /// <summary>
    /// Логика взаимодействия для StudentWindow.xaml
    /// </summary>
    public partial class StudentWindow : Window
    {
        public StudentWindow(List<Student> students, StudyDirection direction)
        {
            InitializeComponent();

            StudyDirection = direction;
            Students = students;

            Start();
        }

        private void Start()
        {
            Header.Text = $"{StudyDirection.UniversityName}\n{StudyDirection}";

            Students = Students.Where(item =>
            {
                if (item.SelectedDirection is not null)
                    return item.SelectedDirection == "" || (item.SelectedDirection.ToLower().Contains(StudyDirection.Name.ToLower()) && 
                    item.SelectedDirection.ToLower().Contains(StudyDirection.UniversityName.ToLower()));

                return false;
            }
            ).ToList();

            Students = Students.OrderByDescending(item => item.Sum).ToList();

            if (Students.Count > StudyDirection.BudgetPlaces)
                Students.RemoveRange(StudyDirection.BudgetPlaces, Students.Count - StudyDirection.BudgetPlaces);

            StudentsTable.ItemsSource = Students;
        }

        public List<Student> Students { get; private set; }

        public StudyDirection StudyDirection { get; private set; }

        private void DataGrid_LoadingRow(object sendler, DataGridRowEventArgs args)
        {
            args.Row.Header = (args.Row.GetIndex() + 1).ToString();
        }

        private void StudentsTable_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {

        }
    }
}
