using System;
using System.Collections.Generic;
using WielkaSowa.Helpers.Calculators;
using WielkaSowa.Models;

namespace WielkaSowa.Helpers
{
    public class Storage
    {
        public static Storage? Instance = null;

        private List<Class> _classes;
        public List<Class> Classes
        {
            get => _classes;
        }

        private Storage()
        {
            _classes = new List<Class>();
        }
        
        public void UpdateCalcs()
        {
            AttendenceCalc.UpdatePoints();
        }

        public static void Init()
        {
            Instance = new Storage();
        }

        
    }
}
