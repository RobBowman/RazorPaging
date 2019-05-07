using System;

namespace Utility.Error.Domain.Entities
{
    /// <summary>
    /// Utility Error
    /// </summary>
    public class Error
    {
        // Error Identity.
        public int Id { get; set; }
        public string Identifier { get; set; }

        // Flags
        public bool Acknowledged { get; set; }
        public bool Actioned { get; set; }

        // Error Handler.
        public string ErrorChannel { get; set; }

        // Error Times.
        public DateTime DateTimeCreated { get; set; }
        public DateTime DateTimeInserted { get; set; }
        public DateTime DateTimeUpdated { get; set; }

        // Error Details.
        public string Category { get; set; } /* Business / Application */
        public string Severity { get; set; } /* Informational / Warning / Error / Critical */
        public string Details { get; set; } 
        public string ErrorCode { get; set; } /* 0 => 99999 */
        public string Source { get; set; }
        public string StackTrace { get; set; }
        
    }
}


/*
{
"DateAndTime": "@{utcNow()}", 
"Details": "Unexpected Error Encountered Updating Email Address from CRM   ",  
"ErrorChannel": "SQL",  
"FaultCode": "10071" , 
"Level": "Error", 
"Payload": " " , 
"Source": "UpdateUserEmail -B2C",  
"StackTrace": ""
}  
*/