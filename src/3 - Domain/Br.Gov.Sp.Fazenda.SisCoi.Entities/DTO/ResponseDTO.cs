using System.Net;

//using Br.Gov.Sp.Fazenda.SisCoi.Entities.jqGrid;

namespace Br.Gov.Sp.Fazenda.SisCoi.Entities.DTO
{

    public class ResponseDTO {

        public dynamic ResponseData { get; set; }

        public HttpStatusCode StatusCode { get; set; }

        public ReturnMessageDTO Retorno { get; set; }

        public string ThrownException { get; set; }

        public string Optional { get; set; }

        public ResponseDTO() {
            StatusCode = HttpStatusCode.OK;
        }
    }

    public class ReturnMessageDTO {

        public string MessageCode;

        public string MessageTitle;

        public string MessageDescription;

        public ReturnMessageDTO(string MessageCode, string Message, string MessageDescription) {
            this.MessageCode = MessageCode;
            this.MessageTitle = Message;
            this.MessageDescription = MessageDescription;
        }
    }
}
