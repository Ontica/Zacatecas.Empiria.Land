/* Empiria Land **********************************************************************************************
*                                                                                                            *
*  Module   : Electronic Sign                              Component : Web Api                               *
*  Assembly : Empiria.Land.WebApi.dll                      Pattern   : Controller                            *
*  Type     : SeguriSignApiClientController                License   : Please read LICENSE.txt file          *
*                                                                                                            *
*  Summary  : Public Web API used to generate and retrieve ESign using SeguriSign version 2.0.               *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

using System.Threading.Tasks;
using System.Web.Http;

using Empiria.WebApi;

namespace Empiria.Zacatecas.Integration.SeguriSign.WebApi {

  /// <summary>Public Web API used to generate and retrieve ESign using SeguriSign version 2.0.</summary>
  public class SeguriSignApiClientController : WebApiController {

    #region Web Apis

    [HttpPost]
    [AllowAnonymous]
    [Route("v2/seguri-sign/get-security-token")]
    public async Task<SingleObjectModel> GetSecurityToken([FromBody] SeguriSignRequestDto body) {

      var service = new SignServices();

      await service.Authenticate(body.Credentials);

      var message = new {
        message = "Authentication successful. Security token obtained."
      };

      return new SingleObjectModel(base.Request, message);
    }


    [HttpPost]
    [AllowAnonymous]
    [Route("v2/seguri-sign/sign-content")]
    public async Task<SingleObjectModel> SignContent([FromBody] SeguriSignRequestDto body) {

      var service = new SignServices();

      await service.Authenticate(body.Credentials);

      var signature = await service.Sign(body.ContentToSign);

      return new SingleObjectModel(base.Request, signature);
    }

    #endregion Web Apis

  } // class SeguriSignApiClientController


  public class SeguriSignRequestDto {

    public SeguriSignCredentialsDto Credentials {
      get; set;
    }

    public string ContentToSign {
      get; set;
    }

  }  // class SeguriSignRequestDto

} // namespace Empiria.Zacatecas.Integration.SeguriSign.WebApi
