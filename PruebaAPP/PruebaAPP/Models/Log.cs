﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebServiceRest_RFID.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Log
    {
        [JsonProperty(PropertyName = "ID_Usuario")]
        public int ID_Usuario {get; set;}
        [JsonProperty(PropertyName = "RFID")]
        public long RFID { get; set; }
        [JsonProperty(PropertyName = "Fecha")]
        public DateTime Fecha { get; set; }
        [JsonProperty(PropertyName = "ID_Lector")]
        public int ID_Lector { get; set; }
        [JsonProperty(PropertyName = "Estatus")]
        public int Estatus { get; set; }

        public Log(int ID_Usuario, long RFID, DateTime Fecha, int ID_Lector, int Estatus)
        {
            this.ID_Usuario = ID_Usuario;
            this.RFID = RFID;
            this.Fecha = Fecha;
            this.ID_Lector = ID_Lector;
            this.Estatus = Estatus;
        }

        public Log()
        {

        }

    }
}