namespace Simple.Services
{
    public interface IPrintMessages
    {
        string Print();
        string Print(string? message) => message != nulll
    }
}
