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

        public float weight { get; set; }

        public float height { get; set; }

        public float age { get; set; }

        public float activityLevels { get; set; }

        public float bmrValue { get; set; }

        public float bmiValue { get; set; }

        public float FatPercentageValue { get; set; }

        public DateTime Date { get; set; }

        //public string user { get; set; }
    }
}
