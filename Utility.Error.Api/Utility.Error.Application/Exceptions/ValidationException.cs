using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;

namespace Utility.Error.Application.Exceptions
{
    /// <summary>
    /// Validation Exception.
    /// </summary>
    public class ValidationException : Exception
    {
        // Public Methods.
        #region PublicMethods

        /// <summary>
        /// Constructor.
        /// </summary>
        public ValidationException()
            : base("One or more validation failures have occurred.")
        {
            Failures = new Dictionary<string, string[]>();
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="failures"></param>
        public ValidationException(List<ValidationFailure> failures) : this()
        {
            var propertyNames = failures.Select(e => e.PropertyName).Distinct();

            foreach (var propertyName in propertyNames)
            {
                var propertyFailures = failures.Where(e => e.PropertyName == propertyName).Select(e => e.ErrorMessage).ToArray();
                Failures.Add(propertyName, propertyFailures);
            }
        }

        #endregion

        // Public Properties.
        #region PublicProperties

        public IDictionary<string, string[]> Failures { get; }

        #endregion
    }
}
