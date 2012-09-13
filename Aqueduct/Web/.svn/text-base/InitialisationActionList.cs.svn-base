using System;
using System.Collections.Generic;
using System.Web;

namespace Aqueduct.Web
{
    public class InitialisationActionList : List<InitialisationAction>
    {
        public InitialisationActionList()
        {

        }

        public void AddAction(InitialisationStage stage, Action<HttpApplication> initAction)
        {
            Add(new InitialisationAction(stage, initAction));
        }

        public void AddOnStartAction(Action<HttpApplication> initAction)
        {
            AddAction(InitialisationStage.OnStart, initAction);
        }

        public void AddOnFirstRequestAction(Action<HttpApplication> initAction)
        {
            AddAction(InitialisationStage.OnFirstRequest, initAction);
        }
    }
}
