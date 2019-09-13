using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Support.V4.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using OOP_Exercise.Utility_Classes;

namespace OOP_Exercise.Fragments
{
   
    class FragmentQuiz:Fragment
    {
        TextView txtQuestionContent;
        CheckBox checkBoxA, checkBoxB, checkBoxC, checkBoxD;
        Question question;
        int indexOfQuestion;
        public override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            

            // Create your fragment here
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            // Use this to return your custom view for this Fragment
            // return inflater.Inflate(Resource.Layout.YourFragment, container, false);
            View view = inflater.Inflate(Resource.Layout.fragment_question_item, container, false);
         
            indexOfQuestion = Arguments.GetInt("index", -1);
            question = Manager.questionList[indexOfQuestion];
            FindIdOfWidget(view);
            LoadContent();

            return view;
        }
        void FindIdOfWidget(View view)
        {
            txtQuestionContent = view.FindViewById<TextView>(Resource.Id.txt_question_content);
            checkBoxA = view.FindViewById<CheckBox>(Resource.Id.checkBoxA);
            checkBoxB = view.FindViewById<CheckBox>(Resource.Id.checkBoxB);
            checkBoxC = view.FindViewById<CheckBox>(Resource.Id.checkBoxC);
            checkBoxD = view.FindViewById<CheckBox>(Resource.Id.checkBoxD);
        }

        void LoadContent()
        {
            txtQuestionContent.Text = question.contentOfQuestion;
            checkBoxA.Text = question.optionA;
            checkBoxB.Text = question.optionB;
            checkBoxC.Text = question.optionC;
            checkBoxD.Text = question.optionD;

        }
    }
}