/* ***********************************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign Services                   Component : Adapters Layer                          *
*  Assembly : SeguriSign.Connector.dll                   Pattern   : Input Data Transfer Object              *
*  Type     : SignerCredentialsDto                       License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Input DTO with user credentials used to sign documents.                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace SeguriSign.Connector.Adapters {

  /// <summary>Input DTO with user credentials used to sign documents.</summary>
  public class SignerCredentialsDto {

    public string UserName {
      get;
      set;
    }

    public string Password {
      get;
      set;
    }

  }  // SignerCredentialsDto

} // namespace SeguriSign.Connector.Adapters
