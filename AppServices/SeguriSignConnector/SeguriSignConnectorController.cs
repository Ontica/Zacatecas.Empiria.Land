/* Empiria Land **********************************************************************************************
*                                                                                                            *
*  Module   : ESign                                        Component : Web Api                               *
*  Assembly : Empiria.Land.WebApi.dll                      Pattern   : Controller                            *
*  Type     : ESignController                              License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Public Web API used to generate and retrieve ESign.                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Web.Http;

using Empiria.WebApi;

using SeguriSign.Connector;
using SeguriSign.Connector.Adapters;

namespace Empiria.Zacatecas.Integration.SeguriSign.WebApi {

  /// <summary>Public Web API used to generate and retrieve ESign.</summary>
  public class SeguriSignConnectorController : WebApiController {

    private readonly string ESIGN_SERVICE_PROVIDER_URL = ConfigurationData.GetString("ESign.ServiceProvider.URL");

    #region Web Apis

    [HttpPost]
    [AllowAnonymous]
    [Route("v1/seguri-sign/e-sign")]
    public SingleObjectModel ESignContent([FromBody] SignRequestDto body) {

      var service = new ESignService(ESIGN_SERVICE_PROVIDER_URL, body.SignerCredentials);

      var documentUID = Guid.NewGuid().ToString();

      ESignDataDto eSignData = service.Sign(body.ContentToSign, documentUID);

      return new SingleObjectModel(base.Request, eSignData);
    }


    [HttpPost]
    [AllowAnonymous]
    [Route("v1/seguri-sign/signed-pdf-document/{sequenceID}")]
    public SingleObjectModel GetSignedPdfDocument([FromBody] SignRequestDto body,
                                                  [FromUri] string sequenceID) {

      var service = new ESignService(ESIGN_SERVICE_PROVIDER_URL, body.SignerCredentials);

      byte[] pdf = service.GetSignedPdfDocument(sequenceID);

      return new SingleObjectModel(base.Request, pdf);
    }

    #endregion Web Apis

  } // class SeguriSignConnectorController


  public class SignRequestDto {

    public SignerCredentialsDto SignerCredentials {
      get; set;
    }

    public string ContentToSign {
      get; set;
    }

  }  // class SignRequestDto

} // namespace Empiria.Zacatecas.Integration.SeguriSign.WebApi
