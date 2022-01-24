using System;
using System.Collections.Generic;
using System.Text;
using WielkaSowa.Helpers;
using WielkaSowa.Helpers.Calculators;
using WielkaSowa.Helpers.Extensions;
using WielkaSowa.Services;
using WielkaSowa.ViewModels;

namespace WielkaSowa.Models
{
    public class Class : ViewModelBase
    {
        // Dummy field to differentiate instances
        private readonly long _classId;
        public long ClassId { get => _classId; }

        private readonly Pair<double, double> _percentRange = new(0, 100);
        private readonly Pair<double, double> _markRange = new(1, 6);
        private readonly Pair<double, double> _peopleRange = new(0, 40);
        private readonly Pair<double, double> _infiniteRange = new(0, int.MaxValue);

        public ClassData ClassData { get; } = new();

        private long _points;
        public long Points { get => _points; set => SetProperty(ref _points, value); }
        public int Place { get; set; } = 1;

        #region Point properties

        #region Obecnosc i srednia ocen
        private int _attPoints;
        public int AttPoints
        {
            get => _attPoints;
            set
            {
                Validator.ValidateAndSet(null, value, out _attPoints, this);
                OnPropertyChanged();
            }
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
                OnPropertyChanged();
            }
        }

        private int _markBonusPoints;
        public int MarkBonusPoints
        {
            get => _markBonusPoints;
            set
            {
                Validator.ValidateAndSet(null, value, out _markBonusPoints, this);
                OnPropertyChanged();
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
        private int _behaviourPoints;
        public int BehaviourPoints
        {
            get => _behaviourPoints;
            set => SetProperty(ref _behaviourPoints, value);
        }

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
        private int _olympicPoints;
        public int OlympicPoints
        {
            get => _olympicPoints;
            set => SetProperty(ref _olympicPoints, value);
        }

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
        private int _contestPoints;
        public int ContestPoints
        {
            get => _contestPoints;
            set => SetProperty(ref _contestPoints, value);
        }

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
        private int _pePoints;
        public int PePoints
        {
            get => _pePoints;
            set => SetProperty(ref _pePoints, value);
        }

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
            set => Validator.ValidateAndSet(false, _infiniteRange, value, out _sportsClubs, this);
        }
        #endregion
        #region Aktywnosci klasowe
        private int _classActivitiesPoints;
        public int ClassActivitiesPoints
        {
            get => _classActivitiesPoints;
            set => SetProperty(ref _classActivitiesPoints, value);
        }

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
        private int _agendasPoints;
        public int AgendasPoints
        {
            get => _agendasPoints;
            set => SetProperty(ref _agendasPoints, value);
        }

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
        private int _schoolEventsPoints;
        public int SchoolEventsPoints
        {
            get => _schoolEventsPoints;
            set => SetProperty(ref _schoolEventsPoints, value);
        }

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
        private int _volunteeringActionsPoints;
        public int VolunteeringActionsPoints
        {
            get => _volunteeringActionsPoints;
            set => SetProperty(ref _volunteeringActionsPoints, value);
        }

        private string _volunteeringPoints = "";
        public string VolunteeringPoints
        {
            get => _volunteeringPoints;
            set => Validator.ValidateAndSet(false, _infiniteRange, value, out _volunteeringPoints, this);
        }

        private string _mrmPoints = "";
        public string MrmPoints
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
        public string PhtPoints
        {
            get => _phtPoints;
            set => Validator.ValidateAndSet(false, _infiniteRange, value, out _phtPoints, this);
        }

        private string _meetingPoints = "";
        public string MeetingPoints
        {
            get => _meetingPoints;
            set => Validator.ValidateAndSet(false, _infiniteRange, value, out _meetingPoints, this);
        }
        #endregion

        #endregion

        public Class()
        {
            _classId = DateTime.Now.ToFileTime();
            Storage.Instance.Classes.Add(this);
        }

        public void RecalculatePoints()
        {
            Multipliers m = Settings.Instance.Current.Multipliers;
            BehaviourPoints = 
                _wzor.ToInt() * m.MWzor + 
                _bdb.ToInt() * m.MBdb + 
                _db.ToInt() * m.MDb + 
                _pop.ToInt() * m.MPop + 
                _nOdp.ToInt() * m.MNOdp + 
                _nag.ToInt() * m.MNag;

            OlympicPoints =
                _schoolOlympic.ToInt() * m.MOSz +
                _regionalOlympic.ToInt() * m.MOOk +
                _centralOlympic.ToInt() * m.MOCt +
                _internationalOlympic.ToInt() * m.MOMn;

            ContestPoints =
                _schoolContest.ToInt() * m.MKSz +
                _regionalContest.ToInt() * m.MKRj +
                _centralContest.ToInt() * m.MKCt +
                _internationalContest.ToInt() * m.MKMn;

            PePoints =
                _peTeacher.ToInt() +
                _sportsClubs.ToInt();

            ClassActivitiesPoints =
                _class1Day.ToInt() * m.MWK1 + 
                _class2Day.ToInt() * m.MWK2 +
                _classActions.ToInt() * m.MPK;

            AgendasPoints = 
                _flagGroup.ToInt() * m.MPSz + 
                _studentParliament.ToInt() * m.MPU;

            SchoolEventsPoints = 
                _artisticEvents.ToInt() * m.MSzPA + 
                _fairs.ToInt() * m.MSzKier + 
                _schoolEventsOrganization.ToInt() * m.MSzOImp + 
                _schoolEventsHelp.ToInt() * m.MSzPImp;

            VolunteeringActionsPoints = 
                _volunteeringPoints.ToInt() * m.MPSzWol +
                _mrmPoints.ToInt() * m.MPSzMRM +
                _scoutingPoints.ToInt() * m.MPSzHar +
                _phtPoints.ToInt() * m.MPSzPTH +
                _meetingPoints.ToInt() * m.MPSzZbior;

            Points = 0;

            Points += 
                BehaviourPoints + 
                OlympicPoints +
                ContestPoints +
                PePoints +
                ClassActivitiesPoints +
                AgendasPoints +
                SchoolEventsPoints +
                VolunteeringActionsPoints;

            Points +=
                _attPoints +
                _markPoints +
                _markBonusPoints;
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
        public int LetterIndex { get; }
        public int LevelIndex { get; }
        public bool AfterPrimarySchool { get; }

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
            LevelIndex = levelCandidate == null ? 0 : AvailableLevels.IndexOf(levelCandidate);
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