using System;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Utility.Error.Application.Error.Models
{
    /// <summary>
    /// Error Detail Model.
    /// 
    /// Defines the data contract exposed by the API.
    /// </summary>
    [Serializable]
    [DataContract]
    public class ErrorDetailModel
    {
        // Public Properties.
        #region PublicProperties

        [DataMember(Name = "identifier", EmitDefaultValue = false)]
        public string Identifier { get; set; }

        [DataMember(Name = "datetimecreated", EmitDefaultValue = false)]
        public DateTime DateTimeCreated { get; set; }

        [DataMember(Name = "category", EmitDefaultValue = false)]
        public string Category { get; set; }

        [DataMember(Name = "severity", EmitDefaultValue = false)]
        public string Severity { get; set; }

        [DataMember(Name = "details", EmitDefaultValue = false)]
        public string Details { get; set; }

        [DataMember(Name = "errorcode", EmitDefaultValue = false)]
        public string ErrorCode { get; set; }

        [DataMember(Name = "source", EmitDefaultValue = false)]
        public string Source { get; set; }

        [DataMember(Name = "stacktrace", EmitDefaultValue = false)]
        public string StackTrace { get; set; }

        [DataMember(Name = "payload", EmitDefaultValue = false)]
        public string Payload { get; set; }

        #endregion

        // Public Methods.
        #region PublicMethods

        /// <summary>
        /// Create.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public static ErrorDetailModel Create(Domain.Entities.Error error)
        {
            return ProjectionOut.Compile().Invoke(error);
        }

        /// <summary>
        /// Persist.
        /// </summary>
        /// <param name="error"></param>
        /// <returns></returns>
        public static Domain.Entities.Error Persist(ErrorDetailModel error)
        {
            return ProjectionIn.Compile().Invoke(error);
        }

        #endregion

        // Private Methods.
        #region PrivateMethods

        /// <summary>
        /// Projection Out to ErrorDataModel.
        /// </summary>
        private static Expression<Func<Domain.Entities.Error, ErrorDetailModel>> ProjectionOut
        {
            get
            {
                return error => new ErrorDetailModel
                {
                    Identifier = error.Identifier,
                    DateTimeCreated = error.DateTimeCreated,
                    Category = error.Category,
                    Severity = error.Severity,
                    Details = error.Details,
                    ErrorCode = error.ErrorCode,
                    Source = error.Source,
                    StackTrace = error.StackTrace
                };
            }
        }

        /// <summary>
        /// Projection In to Domain Entity Error.
        /// </summary>
        private static Expression<Func<ErrorDetailModel, Domain.Entities.Error>> ProjectionIn
        {
            get
            {
                var dateTimeNow = DateTime.Now;

                return error => new Domain.Entities.Error
                {
                    Identifier = string.IsNullOrEmpty(error.Identifier) ? Guid.NewGuid().ToString("D") : error.Identifier,
                    Acknowledged = false,
                    Actioned = false,
                    ErrorChannel = string.Empty,
                    DateTimeCreated = error.DateTimeCreated,
                    DateTimeInserted = dateTimeNow,
                    DateTimeUpdated = dateTimeNow,
                    Category = error.Category,
                    Severity = error.Severity,
                    Details = error.Details,
                    ErrorCode = error.ErrorCode,
                    Source = error.Source,
                    StackTrace = error.StackTrace
                };
            }
        }

        #endregion
    }
}
