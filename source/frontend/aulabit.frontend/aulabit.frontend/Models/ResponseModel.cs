using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace aulabit.frontend.Models
{
    public class ResponseModel
    {
        public dynamic result { get; set; }
        public bool response { get; set; }
        public string message { get; set; }

        public string href { get; set; }
        public string function { get; set; }

        public ResponseModel()
        {
            this.response = false;
            this.message = "* Error inesperado";
        }

        public void SetResponse(bool r, string m = "")
        {
            this.response = r;
            this.message = m;

            if (!r && string.IsNullOrEmpty(m)) this.message = "* Ocurrio un error inesperado";
        }
    }

}