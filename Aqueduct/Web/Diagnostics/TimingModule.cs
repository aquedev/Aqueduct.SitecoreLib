using System;
using System.Diagnostics;
using System.Web;

namespace Aqueduct.Web.Diagnostics
{
    public class TimingModule : IHttpModule
    {
        public static Func<bool> ShouldWriteToPage;

        /// <summary>
        /// Initializes a new instance of the TimingModule class.
        /// </summary>
        static TimingModule()
        {
            ShouldWriteToPage = () => true;
        }
         
        public void Dispose()
        {

        }

        public void Init(HttpApplication context)
        {
            context.BeginRequest += OnBeginRequest;
            context.EndRequest += OnEndRequest;
        }

        void OnBeginRequest(object sender, System.EventArgs e)
        {
            if (HttpContext.Current.IsDebuggingEnabled)
            {
                var stopwatch = new Stopwatch();
                HttpContext.Current.Items["Stopwatch"] = stopwatch;
                stopwatch.Start();
            }
        }

        void OnEndRequest(object sender, System.EventArgs e)
        {
            if (HttpContext.Current.IsDebuggingEnabled)
            {
                Stopwatch stopwatch =
                  (Stopwatch)HttpContext.Current.Items["Stopwatch"];
                if (stopwatch != null)
                {
                    stopwatch.Stop();

                    TimeSpan ts = stopwatch.Elapsed;
                    string elapsedTime = String.Format("{0}ms", ts.TotalMilliseconds);

                    WriteResult(elapsedTime);
                }
            }
        }

        private static void WriteResult(string elapsedTime)
        {
            if (ShouldWriteToPage())
                HttpContext.Current.Response.Write("<p style=\"color: red\"><label>Elapsed Time:</label> " + elapsedTime + "</p>");
        }
    }
}
