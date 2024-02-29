/* ***********************************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign Services                   Component : Adapters Layer                          *
*  Assembly : SeguriSign.Connector.dll                   Pattern   : Converter                               *
*  Type     : ESignXmlToSignDataConverter                License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Converts a SeguriSign Xml document to a ESignData structure.                                   *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Xml;

namespace SeguriSign.Connector.Adapters {

  /// <summary>Converts a SeguriSign Xml document to a ESignData structure.</summary>
  internal class ESignXmlToSignDataConverter {

    private readonly XmlDocument _xml;

    internal ESignXmlToSignDataConverter(string signXmlEvidence) {
      _xml = new XmlDocument();

      _xml.LoadXml(signXmlEvidence);
    }

    internal ESignDataDto Convert() {
      return new ESignDataDto {
        DocumentID = GetDocumentID(),
        DocumentName = GetDocumentName(),
        AuthorityName = GetAuthorityName(),
        SerialNumber = GetSerialNumber(),
        SerialName = GetSerialName(),
        SignDate = GetSignDate(),
        LocalSignDate = GetLocalSignDate(),
        SignatoryName = GetSignatoryName(),
        SignatoryRole = GetSignatoryRole(),
        SignatureAlgorithm = GetSignatureAlgorithm(),
        Signature = GetSignature(),
        Digest = GetDigest(),
        ResponderIssuer = GetResponderIssuer(),
        ResponderName = GetResponderName()
      };
    }

    #region Helpers

    private string GetAuthorityName() {
      return GetXmlAttributeValue("/signinginfo/ca", "name");
    }

    private string GetDigest() {
      return GetXmlAttributeValue("/signinginfo/ca/signatory/tsp", "digest");
    }

    private string GetDocumentID() {
      return GetXmlAttributeValue("/signinginfo/document", "id");
    }

    private string GetDocumentName() {
      return GetXmlAttributeValue("/signinginfo/document", "name");
    }

    private DateTime GetLocalSignDate() {
      var value = GetXmlAttributeValue("/signinginfo/ca/signatory/signature", "localdate");

      return DateTime.Parse(value);
    }

    private string GetResponderIssuer() {
      return GetXmlAttributeValue("/signinginfo/ca/signatory/tsp", "responderissuer");
    }

    private string GetResponderName() {
      return GetXmlAttributeValue("/signinginfo/ca/signatory/tsp", "respondername");
    }

    private string GetSerialNumber() {
      return GetXmlAttributeValue("/signinginfo/ca/signatory", "serialnum");
    }

    private string GetSerialName() {
      return GetXmlAttributeValue("/signinginfo/ca/signatory", "serialname");
    }

    private string GetSignatoryName() {
      return GetXmlAttributeValue("/signinginfo/ca/signatory", "name");
    }

    private string GetSignatoryRole() {
      return GetXmlAttributeValue("/signinginfo/ca/signatory", "role");
    }

    private string GetSignature() {
      var xpath = "/signinginfo/ca/signatory/signature";

      XmlNode node = _xml.SelectSingleNode(xpath);

      return node.InnerText;
    }

    private string GetSignatureAlgorithm() {
      return GetXmlAttributeValue("/signinginfo/ca/signatory/signature", "algorithm");
    }

    private DateTime GetSignDate() {
      var value = GetXmlAttributeValue("/signinginfo/ca/signatory/signature", "date");

      return DateTime.Parse(value)
                     .ToUniversalTime();
    }

    private string GetXmlAttributeValue(string xPath, string attributeName) {
      XmlNode node = _xml.SelectSingleNode(xPath);

      return node.Attributes[attributeName].Value;
    }

    #endregion Helpers

  }  // class ESignXmlToSignDataConverter

}  // namespace SeguriSign.Connector.Adapters
