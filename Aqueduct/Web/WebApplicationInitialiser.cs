using System;
using System.Web;
using System.Configuration;
using System.Web.Configuration;
using Aqueduct.Diagnostics;

namespace Aqueduct.Web
{
    /// <summary>
    /// It is used to make initialision of components easier and remove the clutter from 
    /// Application start. 
    /// <remarks>
    /// If actions need to be initialised on FirstRequest then a call to BeginRequestHandler 
    /// should be inserted in Application_BeginRequest
    /// </remarks>
    /// <example>
    /// WebApplicationInitialiser.Setup(this, initActionList =>
    /// {
    ///     initActionList.Add(new DefaultConfigInitialiser()); // initialises the webconfig
    ///     initActionList.AddOnStartAction(app => /* init logic here */ Logging.Initialise);
    /// });
    /// </example>
    /// </summary>
    public static class WebApplicationInitialiser
    {
        static object m_lock = new Object();
        static bool m_isSetup;
        private static InitialisationActionList m_initialisationActions = new InitialisationActionList();
        static bool m_isFirstRequest = true;
        static ILogger m_logger = AppLogger.GetNamedLogger(typeof(WebApplicationInitialiser));

        /// <summary>
        /// Used to Setup the list of initialisation actions and executes all OnApplicationStart actions
        /// </summary>
        /// <param name="application">Current HttpApplication object</param>
        /// <param name="addActionLists">Delegate with ActionList to which InitialisionActions can be added</param>
        public static void Setup(HttpApplication application, Action<InitialisationActionList> addActionLists)
        {
            var actions = new InitialisationActionList();
            addActionLists.Invoke(actions);
            Setup(application, actions);
        }


        /// <summary>
        /// Used to Setup the list of initialisation actions and executes all OnApplicationStart actions
        /// </summary>
        /// <param name="app">Current HttpApplication object</param>
        /// <param name="actions">A list of initialisation actions</param>
        public static void Setup(HttpApplication app, InitialisationActionList actions)
        {
            lock (m_lock)
            {
                if (!m_isSetup)
                {
                    m_isSetup = true;
                    m_initialisationActions = actions;
                    RunAllActionsWithState(app, InitialisationStage.OnStart);
                    m_logger.LogDebugMessage("Setting up initialisers");
                }
            }
        }

        private static void ValidateTheWebApplicationInitialiserModuleIsIncluded(HttpApplication app)
        {
            object webServerSection = ConfigurationManager.GetSection("system.webServer");
            var sect = webServerSection as IgnoreSection;
            sect.SectionInformation.GetRawXml();
            bool hasModule = false;

            //if not iis 7 integrated mode
                HttpModulesSection httpModulesSection =
                    ConfigurationManager.GetSection("system.web/httpModules") as HttpModulesSection;

                foreach (HttpModuleAction module in httpModulesSection.Modules)
                {
                    if (module.Type.Contains(typeof(WebApplicationInitialiserModule).Name))
                        hasModule = true;
                }
            // if iis 6 or iis 7 classic mode


            // throw exception if the module hasn't been found
            if (hasModule == false)
                throw new Exception("The webinitialiser requires the WebApplicationInitialisationModule to be added to the web.config to operate correctly. Please add the module before any other modules");
        }

        public static void HandleBeginRequest(object sender, EventArgs e)
        {
            if (sender == null)
                throw new ArgumentNullException("Handle request requires a valid HttpApplication");

            HandleBeginRequest(sender as HttpApplication);
        }

        /// <summary>
        /// Used to initialise all OnFirstRequest actions
        /// </summary>
        /// <remarks>
        /// Should be called on Application_BeginRequest inside Global.asax
        /// </remarks>
        /// <param name="app">Current HttpApplication object</param>
        public static void HandleBeginRequest(HttpApplication app)
        {
            if (m_isFirstRequest)
            {
                lock (m_lock)
                    if (m_isFirstRequest)
                    {
                        RunAllActionsWithState(app, InitialisationStage.OnFirstRequest);
                        m_isFirstRequest = false;
                        m_logger.LogDebugMessage("OnFirstRequest action completed");
                    }
            }

            RunAllActionsWithState(app, InitialisationStage.OnEveryRequest);
        }

        private static void RunAllActionsWithState(HttpApplication app, InitialisationStage initState)
        {
            foreach (InitialisationAction initAction in m_initialisationActions)
            {
                if (initAction.Stage == initState)
                {
                    initAction.InitAction.Invoke(app);
                }
            }
        }
    }
}
