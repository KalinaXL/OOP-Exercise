using Android.Content;
using Android.Support.V4.App;
using Java.Lang;
using OOP_Exercise.Fragments;
using System.Collections.Generic;
using FragmentManager = Android.Support.V4.App.FragmentManager;

namespace OOP_Exercise.Adapters
{
    class QuizFragmentAdapter : FragmentPagerAdapter
    {
        Context context;
        List<FragmentQuiz> fragmentList;
        public QuizFragmentAdapter(FragmentManager fm, Context context, List<FragmentQuiz> fragmentList) : base(fm)
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

            return new Java.Lang.StringBuilder($"Câu hỏi {position + 1}");
        }
    }

}