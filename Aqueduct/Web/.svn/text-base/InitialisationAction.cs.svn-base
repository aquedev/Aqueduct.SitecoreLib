using System;
using System.Web;

namespace Aqueduct.Web
{
    public class InitialisationAction
    {
        public InitialisationStage Stage { get; private set; }
        public Action<HttpApplication> InitAction { get; protected set; }

        public InitialisationAction(InitialisationStage stage)
        {
            Stage = stage;
            InitAction = app => { };
        }

        public InitialisationAction(InitialisationStage stage, Action<HttpApplication> initAction)
        {
            Stage = stage;
            InitAction = initAction;
        }
    }
}
