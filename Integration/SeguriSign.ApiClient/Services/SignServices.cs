/* Empiria Land **********************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign                            Component : Integration Layer                       *
*  Assembly : SeguriSign.ApiClient.dll                   Pattern   : Provider implementation                 *
*  Type     : SignServices                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Provides electronic sign services using SeguriSign web services.                               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Threading.Tasks;

namespace Empiria.Zacatecas.Integration.SeguriSign {

  /// <summary>Provides electronic sign services using SeguriSign web services.</summary>
  public class SignServices {

    private readonly SeguriSignApiClient _apiClient;

    #region Methods

    public SignServices() {
      var baseAddress = ConfigurationData.GetString("ElectronicSignature.ServiceProvider.URL");

      _apiClient = new SeguriSignApiClient(baseAddress);
    }

    public async Task Authenticate(SeguriSignCredentialsDto credentials) {
      await _apiClient.Authenticate(credentials);
    }


    public async Task<string> Sign(string content) {
      return await _apiClient.SignContent(content, "abcdefg.pdf");
    }

    #endregion Methods

  } // class SignServices

} // namespace Empiria.Zacatecas.Integration.SeguriSign
