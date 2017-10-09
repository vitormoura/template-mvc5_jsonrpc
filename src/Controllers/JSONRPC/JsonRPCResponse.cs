using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TemplateMVC5_JSONRpc.JSONRPC
{
    public class JsonRPCResponse
    {
        public String error { get; set; }

        public Object data { get; set; }

        public String id { get; set; }
    }
}