namespace HistoryJeopardy.Models;

public interface IAnswer
{
    bool Match(string answer);

    string Get();
}