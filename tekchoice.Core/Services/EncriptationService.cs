using tekchoice.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace tekchoice.Core.Services
{
    public class EncriptationService : IEncriptation
    {
        /// <summary>
        /// Constructor of class  EncriptationService
        /// </summary>
        public EncriptationService()
        {
        }
        /// <summary>
        /// method for encrypt string
        /// </summary>
        /// <param name="cadena"></param>
        /// <returns></returns>
        public string EncriptarCadena(string cadena)
        {

            string cadenaEncriptada = "";
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(cadena));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);

            cadenaEncriptada = sb.ToString();
            return cadenaEncriptada;
        }
    }
}
