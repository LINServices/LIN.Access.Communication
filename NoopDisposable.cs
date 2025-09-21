namespace LIN.Access.Communication;

public sealed class NoopDisposable : IDisposable
{
    public static readonly NoopDisposable Instance = new();
    private NoopDisposable() { }
    public void Dispose() { /* no-op */ }
}