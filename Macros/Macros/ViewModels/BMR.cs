using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace Macros
{
    public class BMR
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public float Weight { get; set; }

        public float Height { get; set; }

        public float Age { get; set; }

        public float ActivityLevels { get; set; }

        public float BmrValue { get; set; }

        public float BmiValue { get; set; }

        public float FatPercentageValue { get; set; }

        public DateTime Date { get; set; }

        //public string user { get; set; }
    }
}
