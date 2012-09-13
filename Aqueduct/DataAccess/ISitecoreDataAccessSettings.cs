namespace Aqueduct.DataAccess
{
    public interface ISitecoreDataAccessSettings
    {
        string TemplateBase { get; }
        string ValueDelimiter { get; }
        string[] TargetDatabases { get; }
    }
}