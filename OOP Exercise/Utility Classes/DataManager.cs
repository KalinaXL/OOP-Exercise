using OOP_Exercise.Fragments;
using System.Collections.Generic;

namespace OOP_Exercise.Utility_Classes
{
    public class DataManager
    {
        public static bool IsReadResult = false;
        public static sbyte IsMidTerm = -1;
        private static List<FragmentQuiz> fragmentQuizList = new List<FragmentQuiz>();

        internal static List<FragmentQuiz> FragmentQuizList { get => fragmentQuizList; set => fragmentQuizList = value; }

        private static List<CurrentQuestion> currQuesList = new List<CurrentQuestion>();
        internal static List<CurrentQuestion> CurrQuesList { get => currQuesList; set => currQuesList = value; }


        public static List<Question> QuestionsList = new List<Question>();
        public static byte[] AnswersChoosed;
        public static int NumOfQuesAnswered;
    }
}