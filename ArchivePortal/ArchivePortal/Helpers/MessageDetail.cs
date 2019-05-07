using ArchivePortal.Pages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ArchivePortal.Helpers
{
    public class MessageDetail
    {
        public static string GetMsgString(ILogger<MessageDetailModel> logger, byte[] body, out bool isXml)
        {
            string wholeMsg;
            byte[] noNulls = RemoveNullCharsFromByteArray(body);
            var stream = new MemoryStream(noNulls);
            try
            {
                logger.LogTrace("Going to load stream to XDocument");
                //the xdocument parser should auto remove any BOMs
                XDocument doc = XDocument.Load(stream);
                logger.LogTrace("XDocument parsed the stream OK");
                wholeMsg = doc.ToString();
                isXml = true;
            }
            catch (Exception e)
            {
                logger.LogTrace("Exception when trying to parse archived file as xml:{0}", e.Message);
                isXml = false;
                wholeMsg = Encoding.UTF8.GetString(stream.ToArray());
            }

            return wholeMsg;
        }

        public static byte[] RemoveNullCharsFromByteArray(byte[] input)
        {
            int i = input.Length - 1;
            while (input[i] == 0)
                --i;
            // now input[i] is the last non-zero byte
            byte[] output = new byte[i + 1];
            Array.Copy(input, output, i + 1);

            return output;
        }
    }
}
