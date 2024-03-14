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
using System.Net;
using System.Text;

using SeguriSign.Connector.SeguriSignWS;

using SeguriSign.Connector.Adapters;

namespace SeguriSign.Connector {

  /// <summary>Electronic sign client connector to the SeguriSign platform.</summary>
  public class ESignService {

    private readonly SgSignToolsWS _apiClient = new SgSignToolsWS();

    private readonly string _userAssignedKey;

    public ESignService(string serviceUrl, SignerCredentialsDto signerCredentials) {

      _apiClient.Credentials = new NetworkCredential(signerCredentials.UserName, signerCredentials.Password);

      _apiClient.Url = serviceUrl;

      _userAssignedKey = GetUserAssignedKey(signerCredentials.UserName);
    }


    public byte[] GetSignedPdfDocument(string signSequenceID) {
      GetPrintableUnilateralRequest pdfRequest = new GetPrintableUnilateralRequest();

      pdfRequest.idFromVerify = signSequenceID;
      pdfRequest.watermarkid = -1;
      pdfRequest.watermarkidSpecified = true;

      _apiClient.Timeout = 5 * 60 * 1000;

      GetPrintableUnilateralResponse response = (GetPrintableUnilateralResponse) _apiClient.ProcessMessage(pdfRequest);

      return response.printableDoc.data;
    }


    public ESignDataDto Sign(string contentToSign, string documentUID) {

      Document documentToBeSigned = CreateDocumentToBeSigned(contentToSign, documentUID);

      SignDocumentRequest signRequest = CreateSignDocumentRequest(documentToBeSigned);

      var signResponse = (SignDocumentResponse) _apiClient.ProcessMessage(signRequest);

      if (!signResponse.status) {
        throw new Exception("El contenido no pudo ser firmado electrónicamente");
      }

      byte[] signedData = signResponse.signedDoc.data;

      string signSequenceID = GetESignSequenceID(signedData, documentToBeSigned.filename);

      string signXmlEvidence = GetEvidenceXmlString(signSequenceID);

      var converter = new ESignXmlToSignDataConverter(signXmlEvidence);

      return converter.Convert();
    }


    #region Helpers

    private Document CreateDocumentToBeSigned(string contentToSign, string documentUID) {
      var encoder = new UTF8Encoding(false);

      var document = new Document();

      document.filename = documentUID;
      document.compressed = false;
      document.data = encoder.GetBytes(contentToSign);

      return document;
    }


    private SignDocumentRequest CreateSignDocumentRequest(Document documentToBeSigned) {
      var request = new SignDocumentRequest();

      request.docToSign = documentToBeSigned;
      request.keyid = _userAssignedKey;
      request.withContent = true;

      return request;
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
