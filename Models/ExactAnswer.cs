namespace HistoryJeopardy.Models;

public class ExactAnswer : IAnswer
{
    private readonly string _answer;

    public ExactAnswer(string answer)
    {
        _answer = answer;
    }

    public bool Match(string answer)
    {
        return string.Equals(answer, _answer, StringComparison.InvariantCultureIgnoreCase);
    }

    public string Get()
    {
        return _answer;
    }
}