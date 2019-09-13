using OOP_Exercise.Fragments;
using System.Collections.Generic;

namespace OOP_Exercise.Utility_Classes
{
    public class Manager
    {
        private static List<FragmentQuiz> fragmentQuizList = new List<FragmentQuiz>();

        internal static List<FragmentQuiz> FragmentQuizList { get => fragmentQuizList; set => fragmentQuizList = value; }

        private static List<CurrentQuestion> currQuesList = new List<CurrentQuestion>();
        internal static List<CurrentQuestion> CurrQuesList { get => currQuesList; set => currQuesList = value; }

       
        public static List<Question> questionList = new List<Question>();
    }
}