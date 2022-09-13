using AbiturChecker.Core;
using System;
using AbiturChecker.Core.Abstract;
using AngleSharp.Dom;
using System.Linq;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;
using System.IO;

namespace AbiturChecker.Core
{
    public class DefaultStudyDirectionsParser : IParseable<List<StudyDirection>>
    {

        private enum FieldProperties
        {
            Name = 0,
            BudgetPlaces = 1,
            Statements = 3,
            FieldsCount = 13,
            UniversityName = 1
        }

        public Task<List<StudyDirection>> Parse(IHtmlDocument document, string baseUrl = "")
        {
            List<StudyDirection>? ignors = JsonConvert.DeserializeObject<List<StudyDirection>>(File.ReadAllText("Resources/Config/IgnoreableDirections.json"));
            var fieldPredicate = (IElement item) => (item.GetElementsByTagName("td").Count() == Convert.ToInt32(FieldProperties.FieldsCount)) && item.Children.First().Children.First().TagName.ToLower() == "a";

            var tagsList = document.QuerySelectorAll("tr").Where(item => fieldPredicate(item)).ToList();

            List<StudyDirection> studyDirections = new List<StudyDirection>();

            for (int index = 0; index < tagsList.Count; ++index)
            {
                var innerTags = tagsList[index].GetElementsByTagName("td").ToList();

                var studyDirection = new
                {
                    Name = innerTags[Convert.ToInt32(FieldProperties.Name)].TextContent,
                    BudgetPlaces = Convert.ToInt32(innerTags[Convert.ToInt32(FieldProperties.BudgetPlaces)].TextContent),
                    Statements = Convert.ToInt32(innerTags[Convert.ToInt32(FieldProperties.Statements)].TextContent),
                    StudentsUrl = $"{baseUrl.Replace("index.html", "")}{innerTags.First().Children.First().Attributes.First().TextContent}",
                    UniversityName = document.QuerySelectorAll("center").ToList()[Convert.ToInt32(FieldProperties.UniversityName)].TextContent
            };

                if (ignors == null || ignors.All(item => item.Name != studyDirection.Name))
                    studyDirections.Add(new StudyDirection(studyDirection.Name, studyDirection.UniversityName, studyDirection.BudgetPlaces, studyDirection.Statements, studyDirection.StudentsUrl));
            }

            return Task.FromResult(studyDirections);
        }

    }
}
