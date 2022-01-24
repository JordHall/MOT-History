using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOT_History.Models
{
    public class RfrAndComment
    {
        public string text { get; set; }
        public string type { get; set; }
    }

    public class MotTest
    {
        public string completedDate { get; set; }
        public string testResult { get; set; }
        public string expiryDate { get; set; }
        public string odometerValue { get; set; }
        public string odometerUnit { get; set; }
        public string motTestNumber { get; set; }
        public IList<RfrAndComment> rfrAndComments { get; set; }
    }

    public class Car
    {
        public string registration { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public string firstUsedDate { get; set; }
        public string fuelType { get; set; }
        public string primaryColour { get; set; }
        public IList<MotTest> motTests { get; set; }
    }


}
