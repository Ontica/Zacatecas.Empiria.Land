/* ***********************************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign Services                   Component : Adapters Layer                          *
*  Assembly : SeguriSign.Connector.dll                   Pattern   : Output DTO                              *
*  Type     : ESignDataDto                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO with electronic sign data.                                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System;

namespace SeguriSign.Connector.Adapters {

  /// <summary>Output DTO with electronic sign data.</summary>
  public class ESignDataDto {

    public string DocumentID {
      get; internal set;
    }

    public string DocumentName {
      get; internal set;
    }

    public string AuthorityName {
      get; internal set;
    }

    public string SerialNumber {
      get; internal set;
    }

    public string SerialName {
      get; internal set;
    }

    public DateTime SignDate {
      get; internal set;
    }

    public DateTime LocalSignDate {
      get; internal set;
    }

    public string SignatoryName {
      get; internal set;
    }

    public string SignatoryRole {
      get; internal set;
    }

    public string SignatureAlgorithm {
      get; internal set;
    }

    public string Signature {
      get; internal set;
    }

    public string Digest {
      get; internal set;
    }

    public string ResponderIssuer {
      get; internal set;
    }

    public string ResponderName {
      get; internal set;
    }

  }  // class ESignDataDto

}  // namespace SeguriSign.Connector.Adapters
