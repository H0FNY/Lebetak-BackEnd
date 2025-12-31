//namespace Lebetak.DTOs.Service
//{
//    public class ServicePageToAnswer
//    {
//        public int CategoryId { get; set; }
//        public string CategoryName { get; set; }
//        public List<QuestionToAnswer> Questions { get; set; }
//    }
//}


// company ==> Category ==> Question ==> Option
// get category ==> get questions by categoryId => get options by questionId
/**
 * {
     category: "General",
    questions : [
    {
    questionId: 1,
    questionText: "How satisfied are you with our service?",
    options: [
        { optionId: 1, optionText: "Very Satisfied" },
        { optionId: 2, optionText: "Satisfied" },
        { optionId: 3, optionText: "Neutral" },
        { optionId: 4, optionText: "Dissatisfied" },
        { optionId: 5, optionText: "Very Dissatisfied" }    
}
        ]
}
 **/