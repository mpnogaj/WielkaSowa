using System.Collections.Generic;
using System.Text;
using WielkaSowa.Helpers;

namespace WielkaSowa.Models
{
    public class Class
    {
        private readonly Pair<double, double> _percentRange = new(0, 100);
        private readonly Pair<double, double> _markRange = new(0, 6);
        private readonly Pair<double, double> _peopleRange = new(0, 40);
        
        public ClassData ClassData { get; set; } = new();
        public int Points { get; set; } = 0;
        public int Place { get; set; } = 1;

        private string _averageAtt = "";
        public string AverageAtt
        {
            get => _averageAtt;
            set
            {
                Validator.ValidateRealNumber(value);
                if(value != string.Empty)
                    Validator.ValidateRange(value.ToDouble(), _percentRange);
                _averageAtt = value;
            }
        }

        private string _averageMark = "";
        public string AverageMark
        {
            get => _averageMark;
            set
            {
                Validator.ValidateRealNumber(value);
                if(value != string.Empty)
                    Validator.ValidateRange(value.ToDouble(), _markRange);
                _averageMark = value;
            }
        }

        public override string ToString()
        {
            return $"{Place}: {ClassData} - {Points}";
        }
    }

    public class ClassData
    {
        // ReSharper disable MemberCanBePrivate.Global
        public int LetterIndex { get; set; }
        public int LevelIndex { get; set; }
        public bool AfterPrimarySchool { get; set; }

        public List<char> AvailableLetters { get; } = new()
        {
            'A',
            'B',
            'C',
            'D',
            'E'
        };

        public List<string> AvailableLevels { get; } = new()
        {
            "I",
            "II",
            "III",
            "IV"
        };
        
        // ReSharper restore MemberCanBePrivate.Global
        
        public ClassData()
        {
            LetterIndex = 0;
            LevelIndex = 0;
            AfterPrimarySchool = true;
        }

        public ClassData(char letter, string level, bool afterPrimarySchool)
        {
            var letterCandidate = AvailableLetters.Find(x => x == letter);
            LetterIndex = letter == '\0' ? 0 : AvailableLetters.IndexOf(letterCandidate);
            var levelCandidate = AvailableLevels.Find(x => x == level);
            LevelIndex = levelCandidate == null ? 0 : AvailableLevels.IndexOf(levelCandidate!);
            AfterPrimarySchool = afterPrimarySchool;
        }

        public override string ToString()
        {
            StringBuilder sr = new();
            sr.Append($"{AvailableLevels[LevelIndex]} {AvailableLetters[LetterIndex]}");
            if (!AfterPrimarySchool) sr.Append("(g)");
            return sr.ToString();
        }
    }
}