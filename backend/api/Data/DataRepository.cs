using api.Data;
using System;

public class DataRepository : IDataRepository
{
    public AnswerGetResponse GetAnswer(int answerId) {
        throw new NotImplementedException();
    }

    public QuestionGetSingleResponse GetQuestion(int questionId) {
        throw new NotImplementedException();
    }

    public IEnumerable<QuestionGetManyResponse> GetQuestions() {
        throw new NotImplementedException();
    }

    public IEnumerable<QuestionGetManyResponse> GetQuestionsBySearch(string search) {
        throw new NotImplementedException();
    }

    public IEnumerable<QuestionGetManyResponse> GetUnansweredQuestions() {
        throw new NotImplementedException();
    }

    public bool QuestionExists(int questionId) {
        throw new NotImplementedException();
    }
}
