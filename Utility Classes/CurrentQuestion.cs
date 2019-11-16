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