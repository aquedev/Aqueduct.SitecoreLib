namespace Aqueduct.Common.Context
{
    public interface IApplicationModeLoader
    {
        ApplicationMode Load (IContext context);
    }
}