namespace Aqueduct.Common.Context.ModeLoaders
{
    public class AutoApplicationModeLoader : IApplicationModeLoader
    {
        #region IApplicationModeLoader Members

        public ApplicationMode Load (IContext context)
        {
            return ApplicationMode.Auto;
        }

        #endregion
    }
}