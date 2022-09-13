using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Html.Parser;
using AbiturChecker.Core.Abstract;
using AngleSharp.Html.Dom;
using AngleSharp.Dom;

namespace AbiturChecker.Core
{
    public class DefaultStudentParser : IParseable<List<Student>>
    {

        private enum FieldProperties
        {
            Id = 3,
            Header = 1,
            FieldsCount = 13,
            Sum = 11,
            Studies = 12
        }

        public Task<List<Student>> Parse(IHtmlDocument document, string baseUrl = "")
        {
            var fieldPredicate = (IElement item) => (item.GetElementsByTagName("td").Count() == Convert.ToInt32(FieldProperties.FieldsCount));
            var tagsList = document.QuerySelectorAll("tr").Where(item => fieldPredicate(item)).ToList();
            var centerHeaderTag = document.QuerySelectorAll("center").ToList()[Convert.ToInt32(FieldProperties.Header)];

            string headerDirection = $"{centerHeaderTag.Children.First().InnerHtml}{centerHeaderTag.InnerHtml}";

            List<Student> students = new List<Student>();

            for (int index = 0; index < tagsList.Count; ++index)
            {
                var innerTags = tagsList[index].GetElementsByTagName("td").ToList().AsReadOnly();
                var selectedDirectionTags = innerTags[Convert.ToInt32(FieldProperties.Studies)].GetElementsByTagName("b");
                bool isSelectedCurrentDirection = innerTags.First().GetElementsByTagName("b").Any();

                string selectedDirection = "";

                if (isSelectedCurrentDirection)
                    selectedDirection = headerDirection;
                else if (selectedDirectionTags.Any())
                    selectedDirection = selectedDirectionTags.First().TextContent;
                
                var student = new
                {
                    Id = innerTags[Convert.ToInt32(FieldProperties.Id)].TextContent,
                    Sum = Convert.ToInt32(innerTags[Convert.ToInt32(FieldProperties.Sum)].TextContent),
                    SelectedDirection = selectedDirection
                };

                students.Add(new Student(student.Id, student.Sum, student.SelectedDirection));
            }

            return Task.FromResult(students);
        }
    }
}
