using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Utility.Error.Application.Error.Models
{
    /// <summary>
    /// List of Errors.
    /// </summary>
    [Serializable]
    [DataContract]
    public class ErrorDetailListModel
    {
        public List<ErrorDetailModel> Errors { get; set; }
    }
}
