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

    #region Web Apis

    [HttpPost]
    [AllowAnonymous]
    [Route("v1/seguri-sign/e-sign")]
    public SingleObjectModel ESignContent([FromBody] SignRequestDto body) {

      var service = new ESignService(body.UserCredentials);

      ESignDataDto eSignData = service.Sign(body.ContentToSign);

      return new SingleObjectModel(base.Request, eSignData);
    }

    #endregion Web Apis

  } // class SeguriSignConnectorController


  public class SignRequestDto {

    public UserCredentialsDto UserCredentials {
      get; set;
    }

    public string ContentToSign {
      get; set;
    }

  }  // class SignRequestDto

} // namespace Empiria.Zacatecas.Integration.SeguriSign.WebApi
