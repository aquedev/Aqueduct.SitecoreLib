using System.IO;
using Aqueduct.Extensions;

namespace Aqueduct.Common.Context.ModeLoaders
{
    public class FileApplicationModeLoader : IApplicationModeLoader
    {
        private Stream _stream;
        private readonly string _fileName = "App.mode";

        /// <summary>
        /// Initializes a new instance of the FileApplicationModeLoader class.
        /// </summary>
        public FileApplicationModeLoader ()
        {
        }

        /// <summary>
        /// Initializes a new instance of the FileApplicationModeLoader class.
        /// </summary>
        public FileApplicationModeLoader (string fileName)
        {
            _fileName = fileName;
        }

        /// <summary>
        /// Used for testing
        /// </summary>
        internal FileApplicationModeLoader(Stream stream)
        {
            _stream = stream;
        }

        #region IApplicationModeLoader Members
        public ApplicationMode Load (IContext context)
        {
            if (_stream != null)
                return GetModeFromStream();

            string filePath = context.ResolvePath ("~/" + _fileName);

            if (!File.Exists (filePath))
                return ApplicationMode.Auto;

            using (StreamReader sr = new StreamReader (filePath))
            {
                return sr.ReadLine ().ToEnum(ApplicationMode.Auto);
            }
        }
        private ApplicationMode GetModeFromStream()
        {
            using (StreamReader sr = new StreamReader(_stream))
            {
                return sr.ReadLine().ToEnum(ApplicationMode.Auto);
            }
        }
        #endregion
    }
}