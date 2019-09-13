using System;
using System.Collections.Generic;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using FragmentManager = Android.Support.V4.App.FragmentManager;
using OOP_Exercise.Fragments;
using Java.Lang;

namespace OOP_Exercise.Adapters
{
    class QuizFragmentAdapter : FragmentPagerAdapter
    {
        Context context;
        List<FragmentQuiz> fragmentList;
        public QuizFragmentAdapter(FragmentManager fm,Context context, List<FragmentQuiz> fragmentList) : base(fm)
        {
            this.context = context;
            this.fragmentList = fragmentList;
        }

        public override int Count
        {
            get => fragmentList.Count;
        }

        public override Fragment GetItem(int position)
        {
            return fragmentList[position];
        }
        public override ICharSequence GetPageTitleFormatted(int position)
        {
            return new Java.Lang.StringBuilder($"Câu hỏi {position+1}");
        }
    }

}