namespace Simple.Services
{
    public interface IPrintMessages
    {
        string Print() => "SinjulMSBH";
        string Print(string? message) => message ?? Print();
    }
}
