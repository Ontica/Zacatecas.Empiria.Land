/* ***********************************************************************************************************
*                                                                                                            *
*  Module   : Connector Services                         Component : Data Layer                              *
*  Assembly : SeguriSign.Connector.dll                   Pattern   : Data Service                            *
*  Type     : ConnectorBuilder                           License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Builder used to manage connector for electronic signature.                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Net;
using System.Text;
using ServiceReference;


namespace SeguriSign.Connector.Services
{

    /// <summary>Builder used to manage connector for electronic signature.</summary>
    public class ConnectorService
    {


        /// <summary>Busca llaves asignadas a un usuario</summary>
        internal string SearchKey(SgSignToolsWSPortTypeClient ws)
        {

            GetUserRequest req = new GetUserRequest();
            req.username = ws.ClientCredentials.UserName.UserName;

            try
            {

                GetKeysByUserResponse resp = (GetKeysByUserResponse)ws.ProcessMessage(req);

                if (resp.KeyIDs != null)
                {
                    return resp.KeyIDs[0];

                }
                else
                {
                    return "El Usuario no tiene llaves asignadas, SeguriSign Tools";
                }
                
            }
            catch (Exception ex)
            {

                return $"Error, {ex.Message} {ex.InnerException}";
            }

        }


        /// <summary>Firma documento con contenido</summary>
        public string SignWithContent()
        {

            SgSignToolsWSPortTypeClient ws = new SgSignToolsWSPortTypeClient();

            GetCredentials(ws);
            string userKey = SearchKey(ws);

            SignDocumentRequest req = new SignDocumentRequest();

            var directory = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            string pathToSign = Path.Combine(directory.Parent.Parent.Parent.FullName,
                                      @"tests-docs",
                                      $"archivo_a_firmar.txt");

            req.docToSign = new Document();
            req.docToSign.filename = Path.GetFileName(pathToSign);
            req.docToSign.compressed = false;
            req.docToSign.data = File.ReadAllBytes(pathToSign);
            req.keyid = "2019"; // "userKey";
            req.withContent = true;


            try
            {

                string signedPath = Path.Combine(directory.Parent.Parent.Parent.FullName,
                                      @"tests-docs",
                                      $"archivo_firmado.txt");

                SignDocumentResponse resp = (SignDocumentResponse)ws.ProcessMessage(req);

                bool status = resp.status;
                string s = resp.signedDoc.filename;
                File.WriteAllBytes(signedPath, resp.signedDoc.data);

                return "Se firmó archivo con contenido";
            }
            catch (Exception ex)
            {

                return $"Error, {ex.Message} {ex.InnerException}";
            }

        }


        private void GetCredentials(SgSignToolsWSPortTypeClient ws)
        {
            NetworkCredential networkCredential = new NetworkCredential("Catastro2021", "12345678a");
            ws.ClientCredentials.UserName.UserName = networkCredential.UserName;
            ws.ClientCredentials.UserName.Password = networkCredential.Password;
        }


    } // class ConnectorBuilder

} // namespace SeguriSign.Connector.Domain
