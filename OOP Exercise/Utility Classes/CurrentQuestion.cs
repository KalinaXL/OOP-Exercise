using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace OOP_Exercise
{
    class CurrentQuestion
    {
        public int QuestionIndex { get; set; }
        public bool IsAnswered { get; set; }
        public CurrentQuestion(int questionIdx, bool isAnswered)
        {
            this.QuestionIndex = questionIdx;
            this.IsAnswered = isAnswered;
        }

    }
}