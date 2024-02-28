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

    internal string Convert() {
      return GetESign();
    }

    #region Helpers

    private string GetESign() {
      var xpath = "/signinginfo/ca/signatory/signature";

      XmlNode node = _xml.SelectSingleNode(xpath);

      return node.InnerText;
    }

    #endregion Helpers

  }  // class ESignXmlToSignDataConverter

}  // namespace SeguriSign.Connector.Adapters
