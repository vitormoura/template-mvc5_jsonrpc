using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TemplateMVC5_JSONRpc.JSONRPC
{
    public class JsonRPCRequest
    {
        /// <summary>
        /// Identificador da requisição
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Identificador do método, no formato [Controller].[Action]
        /// </summary>
        public string Method { get; set; }

        /// <summary>
        /// Lista de parâmetros para execução do método
        /// </summary>
        public Object[] Params { get; set; }
    }
}