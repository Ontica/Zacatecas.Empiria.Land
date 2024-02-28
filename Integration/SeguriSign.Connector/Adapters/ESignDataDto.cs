/* ***********************************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign Services                   Component : Adapters Layer                          *
*  Assembly : SeguriSign.Connector.dll                   Pattern   : Output DTO                              *
*  Type     : ESignDataDto                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Output DTO with electronic sign data.                                                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace SeguriSign.Connector.Adapters {

  /// <summary>Output DTO with electronic sign data.</summary>
  public class ESignDataDto {

    public string Signature {
      get;
      internal set;
    }

  }  // class ESignDataDto

}  // namespace SeguriSign.Connector.Adapters
