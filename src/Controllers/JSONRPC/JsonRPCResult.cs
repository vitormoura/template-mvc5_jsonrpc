using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TemplateMVC5_JSONRpc.Results;

namespace TemplateMVC5_JSONRpc.JSONRPC
{
    /// <summary>
    /// Representa um resultado de uma chamada JSON-RPC
    /// </summary>
    public class JsonRPCResult : JsonNetResult
    {
        /// <summary>
        /// Objeto retornado como resultado da chamada
        /// </summary>
        public Object Result
        {
            get;
            private set;
        }

        public JsonRPCResult(Object result)
        {
            this.Result = result;
        }

        public JsonRPCResult(Exception error)
        {
            this.Result = error;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            //O route data possui os dados sobre a requisição original, vamos precisar dela para obter o ID da request
            JsonRPCRequest jsonRequest = context.RouteData.Values[JsonRPCRoute.ROUTE_DATA_RPC_REQUEST_KEY] as JsonRPCRequest;
            
            if( jsonRequest == null)
            {
                throw new InvalidOperationException("A requisição não foi originada por uma chamada JSON-RPC");
            }

            //O objeto JSONRPCResponse abraça o resultado da operação para retornarmos o resultado no padrão esperado
            JsonRPCResponse resp = new JsonRPCResponse();
            resp.id = jsonRequest.ID;

            this.Data = resp;
            
            if (this.Result is Exception)
            {
                resp.error = (this.Result as Exception).Message;
            }
            else
            {
                resp.data = this.Result;
            }
            
            base.ExecuteResult(context);
        }

        /// <summary>
        /// Cria um resultado JSON-RPC contendo a mensagem de erro da exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static JsonRPCResult Error(Exception ex)
        {
            return new JsonRPCResult(ex);
        }

        /// <summary>
        /// Cria um resultado JSON-RPC com os dados de retorno informados
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static JsonRPCResult Success(Object data)
        {
            if (data is Exception)
                throw new ArgumentException("O objeto de resultado não pode ser do tipo erro nesse contexto");

            return new JsonRPCResult(data);
        }


    }
}