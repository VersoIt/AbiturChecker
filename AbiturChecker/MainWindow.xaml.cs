using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.IO;
using System.Windows.Media.Imaging;
using Newtonsoft.Json;
using AbiturChecker.Core;

namespace AbiturChecker
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public const string LoadingText = "Загрузка...";
        public const string AcceptText = "Готово";
        public const string StudentIdRequestText = "Ваш снилс: ";
        public const string StudentDirectionRequestText = "Выберите направление: ";
        public const string UniversityConfigPath = "Resources/Config/PopularUniversities.json";

        private List<Student> _students;
        private StudyDirection _selectedDirection;

        public MainWindow()
        {
            InitializeComponent();
            Start();

            _selectedDirection = new StudyDirection();
            _students = new List<Student>();
        }

        private void Button_Accept(object sendler, RoutedEventArgs args)
        {
            StudentWindow table = new StudentWindow(_students, _selectedDirection);
            table.Owner = this;
            table.Closed += Show;
            this.Hide();
            table.Show();
        }


        private async void ComboBox_SelectUniversity(object sendler, RoutedEventArgs args)
        {
            DirectionText.Text = LoadingText;
            DirectionSelect.IsEnabled = false;
            if (sendler is ComboBox comboBox)
            {
                if (comboBox.SelectedItem is University university)
                {
                    var studyDirections = await DefaultParseLinker<List<StudyDirection>>.LoadRecordAsync(new DefaultParseLinker<List<StudyDirection>>(new ParserSettings(university.StudyDirectionsUrl), new DefaultStudyDirectionsParser()));
                    Logo.Source = new BitmapImage(new Uri(university.Image, UriKind.Relative));
                    DirectionSelect.ItemsSource = studyDirections.First();
                }
            }
            DirectionText.Text = StudentDirectionRequestText;

            DirectionSelect.IsEnabled = true;
            Input.IsEnabled = false;
            Accept.IsEnabled = false;
        }

        private void ComboBox_SelectStudent(object sendler, RoutedEventArgs args)
        {
        }

        private void Show(object? sendler, EventArgs args)
        {
            this.Show();
        }

        private async void ComboBox_SelectDirection(object sendler, RoutedEventArgs args)
        {
            Input.IsEnabled = false;
            Accept.IsEnabled = false;

            IdText.Text = LoadingText;
            Accept.Content = LoadingText;

            if (sendler is ComboBox comboBox)
            {
                if (comboBox.SelectedItem is StudyDirection direction)
                {
                    var students = await DefaultParseLinker<List<Student>>.LoadRecordAsync(new DefaultParseLinker<List<Student>>(new ParserSettings(direction.StudentsUrl), new DefaultStudentParser()));
                    _students = students.First();
                    _selectedDirection = direction;
                }
            }

            DirectionSelect.IsEnabled = true;
            Input.IsEnabled = true;
            Accept.IsEnabled = true;

            IdText.Text = StudentIdRequestText;
            Accept.Content = AcceptText;
        }

        private void Application_Shutdown(object sendler, RoutedEventArgs args) => Application.Current.Shutdown();

        private void Start()
        {
            UniversitySelect.SelectedIndex = 0;
            UniversitySelect.IsEnabled = true;
            var universities = JsonConvert.DeserializeObject<List<University>>(File.ReadAllText(UniversityConfigPath));
            if (universities != null)
                UniversitySelect.ItemsSource = universities.OrderBy(item => item.Rating);
        }
    }
}
