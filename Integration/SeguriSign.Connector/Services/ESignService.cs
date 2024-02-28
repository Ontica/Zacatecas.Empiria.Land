/* ***********************************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign Services                   Component : External connectors                     *
*  Assembly : SeguriSign.Connector.dll                   Pattern   : Service provider                        *
*  Type     : ESignService                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Electronic sign client connector to the SeguriSign platform.                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.IO;
using System.Net;

using SeguriSign.Connector.SeguriSignWS;

using SeguriSign.Connector.Adapters;

namespace SeguriSign.Connector {

  /// <summary>Electronic sign client connector to the SeguriSign platform.</summary>
  public class ESignService {

    static readonly string FILES_PATH = @"E:\\z_archivos_firma\";

    private readonly SgSignToolsWS _apiClient;

    private readonly string _userAssignedKey;

    public ESignService(UserCredentialsDto userCredentials) {

      _apiClient = new SgSignToolsWS();

      _apiClient.Credentials = new NetworkCredential(userCredentials.UserName, userCredentials.Password);

      _apiClient.Url = "http://10.118.11.221:8081";

      _userAssignedKey = GetUserAssignedKey(userCredentials.UserName);
    }

    /// <summary>Firma una cadena de caracteres.</summary>
    public string Sign(string contentToSign) {

      Document documentToBeSigned = CreateDocumentToBeSigned(contentToSign);

      SignDocumentRequest request = CreateSignDocumentRequest(documentToBeSigned);

      var response = (SignDocumentResponse) _apiClient.ProcessMessage(request);

      if (!response.status) {
        throw new Exception("El contenido no pudo ser firmado electrónicamente");
      }

      return GetSignedContent(response.signedDoc.data);
    }

    #region Helpers

    private Document CreateDocumentToBeSigned(string contentToSign) {
      string fileToBeSigned = Path.Combine(FILES_PATH, "por_firmar",
                                           "prueba_firma.txt");

      File.WriteAllText(fileToBeSigned, contentToSign);

      var document = new Document();

      document.filename = Path.GetFileName(fileToBeSigned);
      document.compressed = false;
      document.data = File.ReadAllBytes(fileToBeSigned);

      return document;
    }

    private SignDocumentRequest CreateSignDocumentRequest(Document documentToBeSigned) {
      var request = new SignDocumentRequest();

      request.docToSign = documentToBeSigned;
      request.keyid = _userAssignedKey;
      request.withContent = true;

      return request;
    }

    private string GetSignedContent(byte[] data) {
      string signedPath = Path.Combine(FILES_PATH, "firmados", $"archivo_firmado.txt");

      // string s = response.signedDoc.filename;

      File.WriteAllBytes(signedPath, data);

      return File.ReadAllText(signedPath);
    }

    /// <summary>Regresa la llave asignada al usuario</summary>
    private string GetUserAssignedKey(string userName) {

      GetKeysByUserRequest req = new GetKeysByUserRequest();

      req.username = userName;

      var resp = (GetKeysByUserResponse) _apiClient.ProcessMessage(req);

      if (resp.KeyIDs != null) {
        return resp.KeyIDs[0];

      } else {
        throw new Exception("El usuario no tiene llaves asignadas, SeguriSign Tools");
      }
    }

    #endregion Helpers

  } // class ESignService

} // namespace SeguriSign.Connector
