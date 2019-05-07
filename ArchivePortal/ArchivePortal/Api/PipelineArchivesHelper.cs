//using ArchivePortal.Core;
//using Microsoft.Extensions.Logging;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;

//namespace ArchivePortal.Api
//{
//    public class PipelineArchivesHelper
//    {
//        public static string GetMsgString(ILogger<PipelineArchivesController> logger, List<PipelineArchive> archiveRecs, out bool isXml)
//        {
//            string wholeMsg;
//            isXml = false;
//            if (item.Body == null)
//            {
//                logger.Debug("item.Body is null so nothing to do!");
//                wholeMsg = " ";
//            }
//            else
//            {
//                byte[] noNulls = DataUtils.RemoveNullCharsFromByteArray(item.Body);
//                var stream = new MemoryStream(noNulls);
//                try
//                {
//                    logger.Debug("Going to load stream to XDocument");
//                    //the xdocument parser should auto remove any BOMs
//                    XDocument doc = XDocument.Load(stream);
//                    logger.Debug("XDocument parsed the stream OK");
//                    wholeMsg = doc.ToString();
//                    isXml = true;
//                }
//                catch (Exception e)
//                {
//                    logger.DebugFormat("Exception when trying to parse archived file as xml:{0}", e.Message);
//                    isXml = false;
//                    wholeMsg = Encoding.UTF8.GetString(stream.ToArray());
//                }
//            }

//            return wholeMsg;
//        }

//        public static byte[] RemoveNullCharsFromByteArray(byte[] input)
//        {
//            int i = input.Length - 1;
//            while (input[i] == 0)
//                --i;
//            // now input[i] is the last non-zero byte
//            byte[] output = new byte[i + 1];
//            Array.Copy(input, output, i + 1);

//            return output;
//        }

//    }
//}
