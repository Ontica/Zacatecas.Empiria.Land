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
using System.Text;
using System.Threading.Tasks;

namespace Empiria.Zacatecas.Integration.SeguriSign {

  /// <summary>Web api client that consumes Segurisign web services using HTTP/REST.</summary>
  internal class SeguriSignApiClient {

    #region Global Variables

    private readonly HttpClient client = new HttpClient(CreateHttpClientHandler());
    private readonly string baseAddress;

    private string _securityToken;
    private string _signKey;

    #endregion Global Variables

    internal SeguriSignApiClient(string baseAddress) {
      this.baseAddress = baseAddress;
      this.SetHttpClientProperties();
    }

    #region Methods

    internal async Task Authenticate(SeguriSignCredentialsDto credentials) {
      Assertion.Require(credentials, nameof(credentials));
      Assertion.Require(credentials.UserName, nameof(credentials.UserName));
      Assertion.Require(credentials.Password, nameof(credentials.Password));

      HttpResponseMessage response =
          await client.PostAsJsonAsync("seguridata-sgsigntools/users/authLogin", new {
            username = credentials.UserName,
            password = credentials.Password
          });

      if (!response.IsSuccessStatusCode) {
        throw Assertion.EnsureNoReachThisCode($"Error authenticating user {credentials.UserName} " +
                                              $"with SeguriSign API. Status code: {response.StatusCode}");
      }

      var tokenDto = await response.Content.ReadAsAsync<TokenDto>();

      _securityToken = tokenDto.Token;
      _signKey = credentials.SignKey;
    }


    internal async Task<string> SignContent(string content, string docName) {
      Assertion.Require(content, nameof(content));
      Assertion.Require(docName, nameof(docName));

      EnsureUserIsAuthenticated();

      SetAuthorizationHeader();

      var encoder = new UTF8Encoding(false);

      HttpResponseMessage response =
          await client.PostAsJsonAsync("seguridata-sgsigntools/signature/signData?doPkcs7=true&doDetached=false", new {
            info = encoder.GetBytes(content),
            signatureAlgorithm = "SHA256_WITH_RSA",
            keyId = int.Parse(_signKey),
            infoData = docName
          });

      if (!response.IsSuccessStatusCode) {
        throw Assertion.EnsureNoReachThisCode($"Error signing string with SeguriSign API." +
                                              $"Status code: {response.StatusCode}");
      }

      var signatureDto = await response.Content.ReadAsAsync<SignatureDto>();

      return signatureDto.Signature;
    }

    #endregion Methods

    #region Helpers

    /// <summary>ToDo: Remove this method. Acepta cualquier certificado sin validar</summary>
    static private HttpClientHandler CreateHttpClientHandler() {
      var handler = new HttpClientHandler {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
      };

      return handler;
    }


    private void EnsureUserIsAuthenticated() {
      Assertion.Require(_securityToken, "User is not authenticated.");
    }


    private void SetAuthorizationHeader() {
      if (!client.DefaultRequestHeaders.Contains("Authorization")) {
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_securityToken}");
      }
    }


    private void SetHttpClientProperties() {
      client.BaseAddress = new Uri(baseAddress);
      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
    }

    #endregion Helpers

  } // class SeguriSignApiClient


  internal class SignatureDto {

    public string Signature {
      get; set;
    }
  }


  internal class TokenDto {

    public string Token {
      get; set;
    }
  }

}  // namespace Empiria.Zacatecas.Integration.SeguriSign
