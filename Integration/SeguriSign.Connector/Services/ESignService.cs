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
using System.Text;

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

      SignDocumentRequest signRequest = CreateSignDocumentRequest(documentToBeSigned);

      var signResponse = (SignDocumentResponse) _apiClient.ProcessMessage(signRequest);

      if (!signResponse.status) {
        throw new Exception("El contenido no pudo ser firmado electrónicamente");
      }

      byte[] signedData = signResponse.signedDoc.data;

      string signSequenceID = GetESignSequenceID(signedData, documentToBeSigned.filename);

      string signXmlEvidence = GetEvidenceXmlString(signSequenceID);

      var converter = new ESignXmlToSignDataConverter(signXmlEvidence);

      var eSignData = converter.Convert();

      return eSignData;
    }

    private string GetEvidenceXmlString(string signSequenceID) {
      var xmlRequest = new GetXMLForensicEvidencesUnilateralRequest();

      xmlRequest.idFromVerify = signSequenceID;

      _apiClient.Timeout = 5 * 60 * 1000;

      var xmlResponse = (GetXMLForensicEvidencesUnilateralResponse) _apiClient.ProcessMessage(xmlRequest);

      byte[] xmlData = xmlResponse.evidences.data;

      return Encoding.UTF8.GetString(xmlData, 0, xmlData.Length);
    }

    private string GetESignSequenceID(byte[] signedData, string filename) {
      var verifyRequest = new VerifyRequest();

      var verifyDocument = new Document();

      verifyDocument.filename = filename;
      verifyDocument.compressed = false;
      verifyDocument.data = signedData;

      verifyRequest.signedDoc = verifyDocument;

      var verifyResponse = (VerifyResponse) _apiClient.ProcessMessage(verifyRequest);

      return verifyResponse.sequence;
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
