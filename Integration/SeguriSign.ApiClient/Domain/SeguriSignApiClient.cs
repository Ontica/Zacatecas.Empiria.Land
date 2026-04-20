/* Empiria Land **********************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign                            Component : Services Layer                          *
*  Assembly : SeguriSign.ApliClient.dll                  Pattern   : Web Api Proxy                           *
*  Type     : SeguriSignApiClient                        License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Web api client that consumes Segurisign web services using HTTP/REST.                          *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Empiria.Zacatecas.Integration.SeguriSign {

  /// <summary>Web api client that consumes Segurisign web services using HTTP/REST.</summary>
  internal class SeguriSignApiClient {

    #region Global Variables

    private readonly HttpClient client = new HttpClient();
    private readonly string baseAddress;

    #endregion Global Variables

    internal SeguriSignApiClient(string baseAddress) {
      this.baseAddress = baseAddress;
      this.SetHttpClientProperties();
    }

    #region Methods

    internal async Task<string> Authenticate(string username, string password) {
      HttpResponseMessage response =
          await client.PostAsJsonAsync("users/authLogin", new {
            username,
            password
          });

      response.EnsureSuccessStatusCode();

      var tokenDto = await response.Content.ReadAsAsync<TokenDto>();

      return tokenDto.Token;
    }

    #endregion Methods

    #region Private Methods

    private void SetHttpClientProperties() {
      client.BaseAddress = new Uri(baseAddress);
      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
    }

    #endregion Private Variables & Methods

  } // class SeguriSignApiClient


  internal class TokenDto {

    public string Token {
      get; set;
    }
  }

}  // namespace Empiria.Zacatecas.Integration.SeguriSign
