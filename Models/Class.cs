using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using WielkaSowa.Helpers;
using WielkaSowa.Helpers.Calculators;
using WielkaSowa.Helpers.Extensions;
using WielkaSowa.ViewModels;

namespace WielkaSowa.Models
{
    public class Class : ViewModelBase
    {
        // Dummy field to difrienciate instances
        private readonly long _classId;
        public long ClassId { get => _classId; }

        private readonly Pair<double, double> _percentRange = new(0, 100);
        private readonly Pair<double, double> _markRange = new(1, 6);
        private readonly Pair<double, double> _peopleRange = new(0, 40);
        private readonly Pair<double, double> _infiniteRange = new(0, Int32.MaxValue);
        private readonly Pair<double, double> _sportsClubsRange = new(0, 100);
        private readonly List<PropertyInfo> _properties = new();

        #region Points multipliers
        // ReSharper disable InconsistentNaming
        // Zachowanie
        private const int MWzor = 5;
        private const int MBdb = 3;
        private const int MDb = 1;
        private const int MPop = 0;
        private const int MNOdp = -2;
        private const int MNag = -15;

        // Olimpiady
        private const int MOSz = 10;
        private const int MOOk = 25;
        private const int MOCt = 75;
        private const int MOMn = 150;

        // Konkursy
        private const int MKSz = 5;
        private const int MKRj = 10;
        private const int MKCt = 50;
        private const int MKMn = 100;

        // Wycieczki Klasowe 1-d
        private const int MWK1 = 20;
        // 2 dniowe i dluzsze
        private const int MWK2 = 50;
        // Przedsiewziecia klasowe
        private const int MPK = 5;

        // Programy artystyczne
        private const int MSzPA = 10;
        // Kiermasze
        private const int MSzKier = 50;
        // Organizacja imprez
        private const int MSzOImp = 50;
        // Pomoc w organnizacji imprez
        private const int MSzPImp = 25;

        // Parlament
        private const int MPU = 40;
        // Poczet sztandarowy
        private const int MPSz = 20;

        // Wolontariaty, harcerstwo itp.
        private const int MPSzWol = 40;
        private const int MPSzMRM = 40;
        private const int MPSzHar = 40;
        private const int MPSzPTH = 40;
        private const int MPSzZbior = 25;
        // ReSharper restore InconsistentNaming
        #endregion

        public ClassData ClassData { get; set; } = new();

        private long _points;
        public long Points { get => _points; set => SetProperty(ref _points, value); }
        public int Place { get; set; } = 1;

        #region Point properties

        #region Obecnosc i srednia ocen
        private int _attPoints = 0;
        public int AttPoints
        {
            get => _attPoints;
            set => Validator.ValidateAndSet(null, value, out _attPoints, this);
        }

        private string _averageAtt = "";
        public string AverageAtt
        {
            get => _averageAtt;
            set
            {
                Validator.ValidateAndSet(true, _percentRange, value, out _averageAtt, this);
                AttendenceCalc.UpdatePoints();
            }
        }

        private int _markPoints;
        public int MarkPoints
        {
            get => _markPoints;
            set
            {
                Validator.ValidateAndSet(null, value, out _markPoints, this);
            }
        }

        private int _markBonusPoints;
        public int MarkBonusPoints
        {
            get => _markBonusPoints;
            set
            {
                Validator.ValidateAndSet(null, value, out _markBonusPoints, this);
            }
        }

        private string _averageMark = "";
        public string AverageMark
        {
            get => _averageMark;
            set
            {
                Validator.ValidateAndSet(true, _markRange, value, out _averageMark, this);
                MarkCalculator.UpdatePoints();
            }
        }
        #endregion
        #region Zachowanie
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
        #region Olimpiady i konkursy
        #region Olimpiady
        private string _schoolOlympic = "";
        public string SchoolOlympic
        {
            get => _schoolOlympic;
            set => Validator.ValidateAndSet(true, _infiniteRange, value, out _schoolOlympic, this);
        }

        private string _regionalOlympic = "";
        public string RegionalOlympic
        {
            get => _regionalOlympic;
            set => Validator.ValidateAndSet(true, _infiniteRange, value, out _regionalOlympic, this);
        }

        private string _centralOlympic = "";
        public string CentralOlympic
        {
            get => _centralOlympic;
            set => Validator.ValidateAndSet(true, _infiniteRange, value, out _centralOlympic, this);
        }

        private string _internationalOlympic = "";
        public string InternationalOlympic
        {
            get => _internationalOlympic;
            set => Validator.ValidateAndSet(true, _infiniteRange, value, out _internationalOlympic, this);
        }
        #endregion
        #region Konkursy
        private string _schoolContest = "";
        public string SchoolContest
        {
            get => _schoolContest;
            set => Validator.ValidateAndSet(true, _infiniteRange, value, out _schoolContest, this);
        }

        private string _regionalContest = "";
        public string RegionalContest
        {
            get => _regionalContest;
            set => Validator.ValidateAndSet(true, _infiniteRange, value, out _regionalContest, this);
        }

        private string _centralContest = "";
        public string CentralContest
        {
            get => _centralContest;
            set => Validator.ValidateAndSet(true, _infiniteRange, value, out _centralContest, this);
        }

        private string _internationalContest = "";
        public string InternationalContest
        {
            get => _internationalContest;
            set => Validator.ValidateAndSet(true, _infiniteRange, value, out _internationalContest, this);
        }
        #endregion
        #endregion
        #region Sport
        private string _peTeacher = "";
        public string PeTeacher
        {
            get => _peTeacher;
            set => Validator.ValidateAndSet(false, _infiniteRange, value, out _peTeacher, this);
        }
        
        private string _sportsClubs = "";
        public string SportsClubs
        {
            get => _sportsClubs;
            set => Validator.ValidateAndSet(false, _sportsClubsRange, value, out _sportsClubs, this);
        }
        #endregion
        #region Aktywnosci klasowe
        private string _class1Day = "";
        public string Class1Day
        {
            get => _class1Day;
            set => Validator.ValidateAndSet(false, _infiniteRange, value, out _class1Day, this);
        }

        private string _class2Day = "";
        public string Class2Day
        {
            get => _class2Day;
            set => Validator.ValidateAndSet(false, _infiniteRange, value, out _class2Day, this);
        }
        
        private string _classActions = "";
        public string ClassActions
        {
            get => _classActions;
            set => Validator.ValidateAndSet(false, _infiniteRange, value, out _classActions, this);
        }
        #endregion
        #region Agendy szkolne
        
        private string _studentParliament = "";
        public string StudentParliament
        {
            get => _studentParliament;
            set => Validator.ValidateAndSet(false, _infiniteRange, value, out _studentParliament, this);
        }
        
        private string _flagGroup = "";
        public string FlagGroup
        {
            get => _flagGroup;
            set => Validator.ValidateAndSet(false, _infiniteRange, value, out _flagGroup, this);
        }
        
        #endregion
        #region Wydarzenia szkolne
        private string _artisticEvents = "";
        public string ArtisticEvents
        {
            get => _artisticEvents;
            set => Validator.ValidateAndSet(false, _infiniteRange, value, out _artisticEvents, this);
        }
        
        private string _fairs = "";
        public string Fairs
        {
            get => _fairs;
            set => Validator.ValidateAndSet(false, _infiniteRange, value, out _fairs, this);
        }
        
        private string _schoolEventsOrganization = "";
        public string SchoolEventsOrganizations
        {
            get => _schoolEventsOrganization;
            set => Validator.ValidateAndSet(false, _infiniteRange, value, out _schoolEventsOrganization, this);
        }
        
        private string _schoolEventsHelp = "";
        public string SchoolEventsHelp
        {
            get => _schoolEventsHelp;
            set => Validator.ValidateAndSet(false, _infiniteRange, value, out _schoolEventsHelp, this);
        }
        #endregion
        #region Wolontariat, harcerstwo
        private string _volunteeringPoints = "";
        public string VolunteeringPoints
        {
            get => _volunteeringPoints;
            set => Validator.ValidateAndSet(false, _infiniteRange, value, out _volunteeringPoints, this);
        }

        private string _mrmPoints = "";
        public string MRMPoints
        {
            get => _mrmPoints;
            set => Validator.ValidateAndSet(false, _infiniteRange, value, out _mrmPoints, this);
        }

        private string _scoutingPoints = "";
        public string ScoutingPoints
        {
            get => _scoutingPoints;
            set => Validator.ValidateAndSet(false, _infiniteRange, value, out _scoutingPoints, this);
        }

        private string _phtPoints = "";
        public string PHTPoints
        {
            get => _phtPoints;
            set => Validator.ValidateAndSet(false, _infiniteRange, value, out _phtPoints, this);
        }

        private string _meetingPoints = "";
        public string MeetingPoints
        {
            get => _meetingPoints;
            set => Validator.ValidateAndSet(false, _peopleRange, value, out _meetingPoints, this);
        }
        #endregion

        #endregion

        public Class()
        {
            _classId = DateTime.Now.ToFileTime();
            Storage.Instance!.Classes.Add(this);
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
            Points += 
                _attPoints + 
                _markPoints + 
                _markBonusPoints;

            Points += 
                _wzor.ToInt() * MWzor + 
                _bdb.ToInt() * MBdb + 
                _db.ToInt() * MDb + 
                _pop.ToInt() * MPop + 
                _nOdp.ToInt() * MNOdp + 
                _nag.ToInt() * MNag;

            Points +=
                _schoolOlympic.ToInt() * MOSz +
                _regionalOlympic.ToInt() * MOOk +
                _centralOlympic.ToInt() * MOCt +
                _internationalOlympic.ToInt() * MOMn;

            Points +=
                _schoolContest.ToInt() * MKSz +
                _regionalContest.ToInt() * MKRj +
                _centralContest.ToInt() * MKCt +
                _internationalContest.ToInt() * MKMn;

            Points += 
                _class1Day.ToInt() * MWK1 + 
                _class2Day.ToInt() * MWK2 +
                _classActions.ToInt() * MPK;

            Points += 
                _flagGroup.ToInt() * MPSz + 
                _studentParliament.ToInt() * MPU;

            Points += 
                _artisticEvents.ToInt() * MSzPA + 
                _fairs.ToInt() * MSzKier + 
                _schoolEventsOrganization.ToInt() * MSzOImp + 
                _schoolEventsHelp.ToInt() * MSzPImp;

            Points += 
                _peTeacher.ToInt() + 
                _sportsClubs.ToInt();

            Points += 
                _volunteeringPoints.ToInt() * MPSzWol +
                _mrmPoints.ToInt() * MPSzMRM +
                _scoutingPoints.ToInt() * MPSzHar +
                _phtPoints.ToInt() * MPSzPTH +
                _meetingPoints.ToInt() * MPSzZbior;
        }

        public override string ToString()
        {
            return $"{Place}: {ClassData} - {Points}";
        }

        public override bool Equals(object? obj)
        {
            return obj is Class rhs &&
                   ClassId == rhs.ClassId &&
                   ClassData.Equals(rhs.ClassData);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ClassId, ClassData);
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

        public override bool Equals(object? obj)
        {
            return obj is ClassData data &&
                   LetterIndex == data.LetterIndex &&
                   LevelIndex == data.LevelIndex &&
                   AfterPrimarySchool == data.AfterPrimarySchool;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(LetterIndex, LevelIndex, AfterPrimarySchool);
        }
    }
}