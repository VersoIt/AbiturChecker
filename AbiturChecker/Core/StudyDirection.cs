using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbiturChecker.Core
{
    public class StudyDirection
    {

        private static List<StudyDirection> _directions = new List<StudyDirection>();

        public StudyDirection(string name = "", string universityName = "", int budgetPlaces = 0, int statements = 0, string studentsUrl = "") : this()
        {
            Name = name;
            UniversityName = universityName;
            BudgetPlaces = budgetPlaces;
            Statements = statements;
            StudentsUrl = studentsUrl;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;

            if (GetType() != obj.GetType())
                return false;

            if (obj is StudyDirection other)
            {
                return Name == other.Name &&
                    UniversityName == other.UniversityName &&
                    BudgetPlaces == other.BudgetPlaces &&
                    Statements == other.Statements &&
                    StudentsUrl == other.StudentsUrl;
            }

            return false;
        }

        public static bool operator ==(StudyDirection first, StudyDirection second) => first.Equals(second);

        public static bool operator !=(StudyDirection first, StudyDirection second) => first.Equals(second);

        public string Name { get; init; } = "";
        public string StudentsUrl { get; init; } = "";
        public string UniversityName { get; init; } = "";

        public int BudgetPlaces { get; init; }
        public int Statements { get; init; }

        public StudyDirection()
        {
            _directions.Add(this);
        }

        //public static bool operator ==(StudyDirection? first, StudyDirection? second)
        //{
        //    if (first is null || second is null)
        //        return false;

        //    return first.Name == second.Name &&
        //        first.StudentsUrl == second.StudentsUrl &&
        //        first.UniversityName == second.UniversityName &&
        //        first.Statements == second.Statements &&
        //        first.BudgetPlaces == second.BudgetPlaces;
        //}

        //public override bool Equals(object? obj)
        //{
        //    var other = obj as StudyDirection;

        //    if (other is null)
        //        return false;

        //    return this.Name == other.Name &&
        //        this.StudentsUrl == other.StudentsUrl &&
        //        this.UniversityName == other.UniversityName &&
        //        this.Statements == other.Statements &&
        //        this.BudgetPlaces == other.BudgetPlaces;
        //}

        //public static bool operator !=(StudyDirection? first, StudyDirection? second)
        //{
        //    return !(first == second);
        //}

        public static StudyDirection? Parse(string str)
        {
            string a = _directions.First().UniversityName.ToLower();
            string b = _directions.First().Name.ToLower();
            var direction = _directions.Where(item => str.ToLower().Contains(item.UniversityName.ToLower()) && str.ToLower().Contains(item.Name.ToLower()));
            if (direction.Any())
                return direction.FirstOrDefault();
            return null;
        }

        public override string ToString() => $"{Name} | Бюджетные места: {BudgetPlaces} | Количество заявлений: {Statements}";

        public override int GetHashCode() => base.GetHashCode();
    }
}
