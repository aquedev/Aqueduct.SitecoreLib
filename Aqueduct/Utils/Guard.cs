using System;
using System.Linq;
using System.Reflection;
using Aqueduct.Extensions;

namespace Aqueduct.Utils
{
    public static class Guard
    {

        /// <summary>
        /// Throws ArgumentException if the parameter Type doesn't match the required type
        /// </summary>
        /// <typeparam name="TRequiredType">The type to check for</typeparam>
        /// <param name="param">the parameter that should be testerd</param>
        /// <exception cref="ArgumentException"></exception>
        public static void ParameterShouldBeOfType<TRequiredType>(object param)
        {
            if (!(param is TRequiredType))
                throw new ArgumentException("Parameter is not of type: " + typeof(TRequiredType).Name);
        }

        /// <summary>
        /// Throws ArgumentNullException if the parameter is null.
        /// </summary>
        /// <param name="param">Parameter to be tested</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ParameterNotNull(object param)
        {
            if (param == null)
                throw new ArgumentNullException("Paramater cannot be null.");
        }

        /// <summary>
        /// Throws an ArgumentNullException if the parameter is null.
        /// </summary>
        /// <param name="param">Parameter to be tested</param>
        /// <param name="paramName">Parameter name to be included in the exception</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void ParameterNotNull(object param, string paramName)
        {
            if (param == null)
                throw new ArgumentNullException(paramName, "Paramater cannot be null.");
        }

        /// <summary>
        /// Throws the supplied exception if the condition is false
        /// </summary>
        /// <typeparam name="TException">Exception type to throw</typeparam>
        /// <param name="condition">the condition that will be checked</param>
        /// <param name="exception">the exception that will be thrown</param>
        public static void That<TException>(Func<bool> condition) where TException : Exception, new()
        {
            That<TException>(condition, String.Empty);
        }

        /// <summary>
        /// Throws the specified exception type if the condition is false
        /// </summary>
        /// <typeparam name="TException">Exception type to throw</typeparam>
        /// <param name="condition">the condition that will be checked</param>
        /// <param name="format">the formatted message that will be passed to the exception</param>
        /// <param name="args">format arguments</param>
        /// <remarks>if the type of exception doesn't have a constructor with a message parameter it will use the default 
        /// constructor and the message will not be passed</remarks>
        public static void That<TException>(Func<bool> condition, string format, params object[] args) where TException : Exception, new()
        {
            if (condition())
                return;

            if (format.IsNullOrEmpty())
                throw new TException();

            ConstructorInfo messageConstructor = GetConstructurWithSingleStringParameter(typeof(TException));

            if (messageConstructor != null)
                throw messageConstructor.Invoke(new object[] { String.Format(format, args) }) as TException;
            throw new TException();
        }
        
        private static ConstructorInfo GetConstructurWithSingleStringParameter(Type type)
        {
            ConstructorInfo[] constructors = type.GetConstructors();
            ConstructorInfo messageConstructor = constructors.SingleOrDefault(con => con.GetParameters().Length == 1 && con.GetParameters()[0].ParameterType == typeof(string));
            return messageConstructor;
        }

        

        /// <summary>
        /// Throws an exception if the supplied condition is not true
        /// </summary>
        /// <param name="condition">The condition to check</param>
        /// <param name="messageFormat">Format message for exception</param>
        /// <param name="args">Format arguments for exception</param>
        /// <exception cref="AssertionFailedException"></exception>
        /// <remarks>Condition checking only takes place if this assembly has been 
        /// compiled in debug mode. In release mode, nothing happens</remarks>
        public static void AssertThat(bool condition, string messageFormat, params object[] args)
        {
#if DEBUG
            if (!condition)
            {
                throw new AssertionFailedException(string.Format(messageFormat, args));
            }
#endif
        }

        
    }
}
