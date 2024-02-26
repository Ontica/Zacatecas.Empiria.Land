/* Empiria Land **********************************************************************************************
*                                                                                                            *
*  Module   : Transaction Management                     Component : Services Layer                          *
*  Assembly : SIT.Finanzas.Connector.dll                 Pattern   : Web Api Proxy                           *
*  Type     : ApiClient                                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Web api client that consumes SIT web services.                                                 *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

using Empiria.Zacatecas.Integration.SITFinanzasConnector.Adapters;

namespace Empiria.Zacatecas.Integration.SITFinanzasConnector {

  /// <summary>Web api client that consumes SIT web services.</summary>
  internal class ApiClient {

    #region Global Variables

    private readonly HttpClient client = new HttpClient();
    private readonly string baseAddress;

    #endregion Global Variables

    internal ApiClient(string baseAddress) {
      this.baseAddress = baseAddress;
      this.SetHttpClientProperties();
    }

    #region Internal Methods

    internal async Task<OrdenPagoDto> CreatePaymentRequest(SolicitudDto request) {
      HttpResponseMessage response =
              await client.PostAsJsonAsync("pagosDependencias/calcularRegistroPublicoCompleto", request);

      response.EnsureSuccessStatusCode();

      return await response.Content.ReadAsAsync<OrdenPagoDto>();
    }


    internal async Task<List<ServicioDto>> GetServicesList() {
      List<ServicioDto> services = new List<ServicioDto>();

      HttpResponseMessage response = await client.GetAsync("pagosDependencias/serviciosRegistroPublico");

      if (response.IsSuccessStatusCode) {
        services = await response.Content.ReadAsAsync<List<ServicioDto>>();
      }

      return services;
    }

    internal async Task<decimal> GetVariableCost(PresupuestoDto presupuestoDeServicio) {
      var serviceBudget = new PresupuestoRespuestaDto();

      HttpResponseMessage response =
              await client.PostAsJsonAsync("pagosDependencias/registroPublico", presupuestoDeServicio);

      if (response.IsSuccessStatusCode) {
        serviceBudget = await response.Content.ReadAsAsync<PresupuestoRespuestaDto>();
      }

      return serviceBudget.importeTotal;
    }


    internal async Task<string> GetPaymentFormat(int idPago) {
      string paymentFormUrl = "";

      HttpResponseMessage response = await client.GetAsync($"formatopago/rp/?idPagoElectronico={idPago}");

      if (response.IsSuccessStatusCode) {
        paymentFormUrl = await response.Content.ReadAsStringAsync();
      }

      return paymentFormUrl;
    }


    internal async Task<PagoDto> ValidatePayment(int idPagoElectronico) {
      HttpResponseMessage response =
              await client.GetAsync($"pagosDependencias/consultarPagoRegistroPublico/{idPagoElectronico}");

      if (response.IsSuccessStatusCode) {
        return await response.Content.ReadAsAsync<PagoDto>();
      } else {
        throw new Exception($"There is none payment with id={idPagoElectronico}");
      }
    }


    #endregion Internal Methods

    #region Private Methods

    private void SetHttpClientProperties() {
      client.BaseAddress = new Uri(baseAddress);
      client.DefaultRequestHeaders.Accept.Clear();
      client.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue("application/json"));
    }

    #endregion Private Variables & Methods


  } // class ApiClient

}  // namespace Empiria.Zacatecas.Integration.SITFinanzasConnector
