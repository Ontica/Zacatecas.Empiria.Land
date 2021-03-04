/* Empiria Land **********************************************************************************************
*                                                                                                            *
*  Module   : Transaction Management                     Component : Integration Layer                       *
*  Assembly : SIT.Finanzas.Connector.dll                 Pattern   : Data Transfer Object                    *
*  Type     : ApiClient                                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Client to consume SIT web services.                                                            *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;

using Empiria.Zacatecas.Integration.SITFinanzasConnector.Adapters;

namespace Empiria.Zacatecas.Integration.SITFinanzasConnector {

  /// <summary>Client to consume web services from SIT.</summary>
  internal class ApiClient {

    #region Global Variables

    private readonly HttpClient client = new HttpClient();
    private readonly string baseAddress = "http://10.113.5.187:8080/sit-ingresos/api/";

    #endregion Global Variables

    internal ApiClient() {
      this.SetHttpClientProperties();
    }

    #region Internal Methods

    internal async Task<OrdenPagoDto> CreatePaymentRequest(SolicitudDto request) {
      HttpResponseMessage response = await client.PostAsJsonAsync("pagosDependencias/calcularRegistroPublicoCompleto", request);

      response.EnsureSuccessStatusCode();

      return await response.Content.ReadAsAsync<OrdenPagoDto>();
    }


    internal async Task<List<ServicioDto>> GetServicesList() {
      List<ServicioDto> services = new List<ServicioDto>();

      HttpResponseMessage response = await client.GetAsync(baseAddress + "pagosDependencias/serviciosRegistroPublico");

      if (response.IsSuccessStatusCode) {
        services = await response.Content.ReadAsAsync<List<ServicioDto>>();
      }

      return services;
    }

    internal async Task<decimal> GetVariableCost(PresupuestoDto presupuestoDeServicio) {
      var serviceBudget = new PresupuestoRespuestaDto();

      HttpResponseMessage response = await client.PostAsJsonAsync("pagosDependencias/registroPublico", presupuestoDeServicio);

      if (response.IsSuccessStatusCode) {
        serviceBudget = await response.Content.ReadAsAsync<PresupuestoRespuestaDto>();
      }

      return serviceBudget.importeTotal;
    }


    internal async Task<string> GetPaymentFormat(int idPago) {
      string paymentFormUrl = "";

      HttpResponseMessage response = await client.GetAsync(baseAddress + $"formatopago?idPagoElectronico={idPago}");

      if (response.IsSuccessStatusCode) {
        paymentFormUrl = await response.Content.ReadAsStringAsync();
      }

      return paymentFormUrl;
    }


    internal async Task<PagoDto> ValidatePayment(int idPagoElectronico) {
      HttpResponseMessage response = await client.GetAsync(baseAddress + $"pagosDependencias/consultarPagoRegistroPublico/{idPagoElectronico}");

      if (response.IsSuccessStatusCode) {
        return await response.Content.ReadAsAsync<PagoDto>();
      } else {
        throw new Exception($"Can not find payment with id={idPagoElectronico}");
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
