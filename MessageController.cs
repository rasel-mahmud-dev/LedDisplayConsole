using System;
using System.Runtime.InteropServices;
using System.Web.Http;

namespace RsDisplayConsole {
    public class MessageController : ApiController {
        public class LineContent {
            public string text { get; set; }
            public int? fs { get; set; }
            public int? effect { get; set; }
            public int? speed { get; set; } 
            
            public string color { get; set; }
        }

        public class message {
            public string ip { get; set; }
            public LineContent line1 { get; set; }
            public LineContent line2 { get; set; }
            public LineContent line3 { get; set; }
        }

        private int m_nSendType;

        private IntPtr m_pSendParams;

        public string Get(string id) {
            return id;
        }

        public string Post(message msg) {
            init(msg);
            return showMessage(msg);
        }

        private void init(message msg) {
            m_nSendType = 0;
            string iP = msg.ip;
            m_pSendParams = Marshal.StringToHGlobalUni(iP);
        }

        private string showMessage(message msg) {
            IntPtr intPtr = new IntPtr(0);
            int nHeight = 64;
            int nColor = 1;
            int nGray = 1;
            int nCardType = 0;
            if (CSDKExport.Hd_CreateScreen(128, nHeight, nColor, nGray, nCardType, intPtr, 0) != 0) {
                Console.WriteLine("Failed to create screen");
                return CSDKExport.Hd_GetSDKLastError().ToString();
            }

            int num2 = CSDKExport.Hd_AddProgram(intPtr, 0, 0, intPtr, 0);
            if (num2 == -1) {
                Console.WriteLine("Failed to create program");
                return CSDKExport.Hd_GetSDKLastError().ToString();
            }

            int nX = 0;
            int nY = 0;
            int nWidth = 128;
            int nHeight2 = 17;
            int num3 = CSDKExport.Hd_AddArea(num2, nX, nY, nWidth, nHeight2, intPtr, 0, 0, intPtr, 0);
            if (num3 == -1) {
                Console.WriteLine("Failed to create area");
                return CSDKExport.Hd_GetSDKLastError().ToString();
            }

            IntPtr intPtr2 = Marshal.StringToHGlobalUni(msg.line1.text);
            IntPtr intPtr3 = Marshal.StringToHGlobalUni("Arial");
            int nTextColor = CSDKExport.Hd_GetColor(255, 0, 0);
            int nStyle = 260;
            int line1Fs = msg.line1.fs ?? 16;
            int line1Effect = msg.line1.effect ?? 0;
            int line1Speed = msg.line1.speed ?? 30;
            if (CSDKExport.Hd_AddSimpleTextAreaItem(num3, intPtr2, nTextColor, 0, nStyle, intPtr3, line1Fs, line1Effect, line1Speed, 201, 3,
                    intPtr, 0) == -1) {
                Marshal.FreeHGlobal(intPtr2);
                Marshal.FreeHGlobal(intPtr3);
                return CSDKExport.Hd_GetSDKLastError().ToString();
            }

            Marshal.FreeHGlobal(intPtr2);
            Marshal.FreeHGlobal(intPtr3);
            nX = 0;
            nY = 18;
            nWidth = 128;
            nHeight2 = 26;
            num3 = CSDKExport.Hd_AddArea(num2, nX, nY, nWidth, nHeight2, intPtr, 0, 0, intPtr, 0);
            if (num3 == -1) {
                Console.WriteLine("Failed to create area");
                return CSDKExport.Hd_GetSDKLastError().ToString();
            }

            IntPtr intPtr4 = Marshal.StringToHGlobalUni(msg.line2.text);
            IntPtr hglobal = Marshal.StringToHGlobalUni("Arial");
            int nTextColor2 = CSDKExport.Hd_GetColor(0, 255, 0);
            int nStyle2 = 260;
            int line2Fs = msg.line2.fs ?? 14;
            int line2Effect = msg.line2.effect ?? 3;
            int line2Speed = msg.line2.speed ?? 20;
            if (CSDKExport.Hd_AddSimpleTextAreaItem(num3, intPtr4, nTextColor2, 0, nStyle2, intPtr3, line2Fs, line2Effect, line2Speed, 201, 3,
                    intPtr, 0) == -1) {
                Marshal.FreeHGlobal(intPtr4);
                Marshal.FreeHGlobal(intPtr3);
                Console.WriteLine("Failed to Hd_AddSimpleTextAreaItem Hd_GetSDKLastError");
                return CSDKExport.Hd_GetSDKLastError().ToString();
            }

            Marshal.FreeHGlobal(intPtr4);
            Marshal.FreeHGlobal(hglobal);
            nX = 0;
            nY = 45;
            nWidth = 128;
            nHeight2 = 18;
            num3 = CSDKExport.Hd_AddArea(num2, nX, nY, nWidth, nHeight2, intPtr, 0, 0, intPtr, 0);
            if (num3 == -1) {
                Console.WriteLine("Failed to create area");
                return CSDKExport.Hd_GetSDKLastError().ToString();
            }

            IntPtr intPtr5 = Marshal.StringToHGlobalUni(msg.line3.text);
            IntPtr intPtr6 = Marshal.StringToHGlobalUni("Arial");
            int nTextColor3 = CSDKExport.Hd_GetColor(255, 0, 0);
            int nStyle3 = 260;
            int line3Fs = msg.line3.fs ?? 18;
            int line3Effect = msg.line3.effect ?? 0;
            int line3Speed = msg.line3.speed ?? 30;
            
            if (CSDKExport.Hd_AddSimpleTextAreaItem(num3, intPtr5, nTextColor3, 0, nStyle3, intPtr6, line3Fs, line3Effect, line3Speed, 201, 3,
                    intPtr, 0) == -1) {
                Marshal.FreeHGlobal(intPtr5);
                Marshal.FreeHGlobal(intPtr6);
                Console.WriteLine("Failed to Hd_AddSimpleTextAreaItem Hd_GetSDKLastError");
                return CSDKExport.Hd_GetSDKLastError().ToString();
            }

            Marshal.FreeHGlobal(intPtr5);
            Marshal.FreeHGlobal(intPtr6);
            if (CSDKExport.Hd_SendScreen(m_nSendType, m_pSendParams, intPtr, intPtr, 0) != 0) {
                Console.WriteLine("Failed to send screen");
                return CSDKExport.Hd_GetSDKLastError().ToString();
            }

            return "200";
        }

        [HttpPost]
        [Route("api/message/send")]
        public IHttpActionResult ReceiveMessage([FromBody] message data) {
            if (data == null)
                return BadRequest("Invalid or missing JSON body.");

            return Ok(new {
                status = "received",
                data
            });
        }
    }
}