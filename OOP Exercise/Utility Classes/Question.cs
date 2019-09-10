namespace OOP_Exercise
{
    public class Question
    {
        public string contentOfQuestion { get; set; }
        public string optionA { get; set; }
        public string optionB { get; set; }
        public string optionC { get; set; }
        public string optionD { get; set; }
        public byte rightKey { get; set; }
        public Question(string contentQs, string optionA, string optionB, string optionC, string optionD, byte rightKey)
        {
            this.contentOfQuestion = contentQs;
            this.optionA = optionA;
            this.optionB = optionB;
            this.optionC = optionC;
            this.optionD = optionD;
            this.rightKey = rightKey;
        }
    }
}