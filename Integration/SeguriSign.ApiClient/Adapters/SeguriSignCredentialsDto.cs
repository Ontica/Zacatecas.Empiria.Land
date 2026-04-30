/* ***********************************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign Services                   Component : Adapters Layer                          *
*  Assembly : SeguriSign.ApiClient.dll                   Pattern   : Input Data Transfer Object              *
*  Type     : SeguriSignCredentialsDto                   License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Input DTO with user credentials used to sign documents.                                        *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Zacatecas.Integration.SeguriSign {

  /// <summary>Input DTO with user credentials used to sign documents.</summary>
  public class SeguriSignCredentialsDto {

    public string UserName {
      get;
      set;
    } = string.Empty;


    public string Password {
      get;
      set;
    } = string.Empty;


    public string SignKey {
      get; set;
    } = string.Empty;

  }  // SeguriSignCredentialsDto

} // namespace Empiria.Zacatecas.Integration.SeguriSign
