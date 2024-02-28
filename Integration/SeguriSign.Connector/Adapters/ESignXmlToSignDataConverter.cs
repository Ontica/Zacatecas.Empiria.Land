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
      var xpath = "/signinginfo/ca";

      XmlNode node = _xml.SelectSingleNode(xpath);

      return node.Attributes["name"].Value;
    }

    private string GetDigest() {
      var xpath = "/signinginfo/ca/signatory/tsp";

      XmlNode node = _xml.SelectSingleNode(xpath);

      return node.Attributes["digest"].Value;
    }

    private string GetDocumentID() {
      var xpath = "/signinginfo/document";

      XmlNode node = _xml.SelectSingleNode(xpath);

      return node.Attributes["id"].Value;
    }

    private string GetDocumentName() {
      var xpath = "/signinginfo/document";

      XmlNode node = _xml.SelectSingleNode(xpath);

      return node.Attributes["name"].Value;
    }

    private DateTime GetLocalSignDate() {
      var xpath = "/signinginfo/ca/signatory/signature";

      XmlNode node = _xml.SelectSingleNode(xpath);

      return DateTime.Parse(node.Attributes["localdate"].Value);
    }

    private string GetResponderIssuer() {
      var xpath = "/signinginfo/ca/signatory/tsp";

      XmlNode node = _xml.SelectSingleNode(xpath);

      return node.Attributes["responderissuer"].Value;
    }

    private string GetResponderName() {
      var xpath = "/signinginfo/ca/signatory/tsp";

      XmlNode node = _xml.SelectSingleNode(xpath);

      return node.Attributes["respondername"].Value;
    }

    private string GetSerialNumber() {
      var xpath = "/signinginfo/ca/signatory";

      XmlNode node = _xml.SelectSingleNode(xpath);

      return node.Attributes["serialnum"].Value;
    }

    private string GetSerialName() {
      var xpath = "/signinginfo/ca/signatory";

      XmlNode node = _xml.SelectSingleNode(xpath);

      return node.Attributes["serialname"].Value;
    }


    private string GetSignatoryName() {
      var xpath = "/signinginfo/ca/signatory";

      XmlNode node = _xml.SelectSingleNode(xpath);

      return node.Attributes["name"].Value;
    }

    private string GetSignatoryRole() {
      var xpath = "/signinginfo/ca/signatory";

      XmlNode node = _xml.SelectSingleNode(xpath);

      return node.Attributes["role"].Value;
    }

    private string GetSignature() {
      var xpath = "/signinginfo/ca/signatory/signature";

      XmlNode node = _xml.SelectSingleNode(xpath);

      return node.InnerText;
    }

    private string GetSignatureAlgorithm() {
      var xpath = "/signinginfo/ca/signatory/signature";

      XmlNode node = _xml.SelectSingleNode(xpath);

      return node.Attributes["algorithm"].Value;
    }

    private DateTime GetSignDate() {
      var xpath = "/signinginfo/ca/signatory/signature";

      XmlNode node = _xml.SelectSingleNode(xpath);

      return DateTime.Parse(node.Attributes["date"].Value)
                     .ToUniversalTime();
    }

    #endregion Helpers

  }  // class ESignXmlToSignDataConverter

}  // namespace SeguriSign.Connector.Adapters
