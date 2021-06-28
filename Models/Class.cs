using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using WielkaSowa.Helpers;
using WielkaSowa.ViewModels;

namespace WielkaSowa.Models
{
    public class Class : ViewModelBase
    {
        private readonly Pair<double, double> _percentRange = new(0, 100);
        private readonly Pair<double, double> _markRange = new(0, 6);
        private readonly Pair<double, double> _peopleRange = new(0, 40);
        private readonly List<PropertyInfo> _properties = new();
        
        #region Points multiplayers

        private const int MWzor = 5;
        private const int MBdb = 3;
        private const int MDb = 1;
        private const int MPop = 0;
        private const int MNOdp = -2;
        private const int MNag = -15;
        
        #endregion

        public ClassData ClassData { get; set; } = new();

        private int _points;
        public int Points { get => _points; set => SetProperty(ref _points, value); }
        public int Place { get; set; } = 1;

        #region Point properties
        
        #region Attandance and marks
        private string _averageAtt = "";
        public string AverageAtt
        {
            get => _averageAtt;
            set => Validator.ValidateAndSet(true, _percentRange, value, out _averageAtt, this);
        }

        private string _averageMark = "";
        public string AverageMark
        {
            get => _averageMark;
            set => Validator.ValidateAndSet(true, _markRange, value, out _averageMark, this);
        }
        #endregion
        #region Behaviour grades
        private string _wzor = "";
        public string Wzor
        {
            get => _wzor;
            set => Validator.ValidateAndSet(false, _peopleRange, value, out _wzor, this);
        }

        private string _bdb = "";
        public string Bdb
        {
            get => _bdb;
            set => Validator.ValidateAndSet(false, _peopleRange, value, out _bdb, this);
        }
        
        private string _db = "";
        public string Db
        {
            get => _db;
            set => Validator.ValidateAndSet(false, _peopleRange, value, out _db, this);
        }
        
        private string _pop = "";
        public string Pop
        {
            get => _pop;
            set => Validator.ValidateAndSet(false, _peopleRange, value, out _pop, this);
        }
        
        private string _nOdp = "";
        public string NOdp
        {
            get => _nOdp;
            set => Validator.ValidateAndSet(false, _peopleRange, value, out _nOdp, this);
        }
        
        private string _nag = "";
        public string Nag
        {
            get => _nag;
            set => Validator.ValidateAndSet(false, _peopleRange, value, out _nag, this);
        }
        #endregion

        #endregion

        public Class()
        {
            var properties = typeof(Class).GetProperties();
            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(string))
                {
                    _properties.Add(property);
                }
            }
        }

        public void RecalculatePoints()
        {
            Points = 0;
            // Behaviour points
            // ReSharper disable once UselessBinaryOperation, -> multiplayer may change in future
            Points += (_wzor.ToInt() * MWzor + _bdb.ToInt() * MBdb + _db.ToInt() * MDb + _pop.ToInt() * MPop +
                       _nOdp.ToInt() * MNOdp + _nag.ToInt() * MNag);
        }

        public override string ToString()
        {
            return $"{Place}: {ClassData} - {Points}";
        }
    }

    public class ClassData
    {
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