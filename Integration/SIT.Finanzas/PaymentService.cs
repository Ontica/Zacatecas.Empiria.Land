/* Empiria Land **********************************************************************************************
*                                                                                                            *
*  Module   : Transaction Management                     Component : Integration Layer                       *
*  Assembly : SIT.Finanzas.Connector.dll                 Pattern   : Provider implementation                 *
*  Type     : PaymentService                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Implements IPaymentService interface using Zacatecas Finanzas SIT services.                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System.Threading.Tasks;

using Empiria.Land.Integration.PaymentServices;

using Empiria.Zacatecas.Integration.SITFinanzasConnector.Adapters;

namespace Empiria.Zacatecas.Integration.SITFinanzasConnector {

  /// <summary>Implements IPaymentService interface using Zacatecas Finanzas SIT services.</summary>
  public class PaymentService : IPaymentService {

    private readonly ApiClient _apiClient;

    #region Public methods

    public PaymentService(string baseAddress) {
      _apiClient = new ApiClient(baseAddress);
    }


    public async Task<decimal> CalculateFixedFee(string serviceUID, decimal quantity) {
      // Zacatecas' SIT service needs Shopping Cart to perform one by one fee calculation.
      return await Task.FromResult(0m);
    }


    public async Task<decimal> CalculateVariableFee(string serviceUID, decimal taxableBase) {
      // Zacatecas' SIT service needs Shopping Cart to perform one by one fee calculation.
      return await Task.FromResult(0m);
    }


    public async Task EnsureIsPayed(string paymentOrderUID, decimal amount) {
      SITPaymentDto payment = await GetPayment(paymentOrderUID);

      Assertion.Require(payment.Status,
            $"El recibo de pago '{paymentOrderUID}' no está registrado " +
            $"en la Secretaría de Finanzas.");

      Assertion.Require(payment.Status == "PAGADO",
                  $"El recibo de pago {paymentOrderUID} no ha sido pagado. " +
                  $"Su estado actual es '{payment.Status}'.");

      Assertion.Require(amount == payment.Total,
                  $"El importe {amount.ToString("C2")} no coincide con el importe " +
                  $"pagado en la Secretaría de Finanzas.");
    }


    public async Task<IPaymentOrder> GeneratePaymentOrderFor(PaymentOrderRequestDto paymentOrderRequest) {
      SolicitudDto sitRequest = Mapper.MapPaymentRequestToSITRequest(paymentOrderRequest);

      OrdenPagoDto ordenPago = await _apiClient.CreatePaymentRequest(sitRequest);

      string urlPaymentDocument = await _apiClient.GetPaymentFormat(ordenPago.idPagoElectronico);

      PaymentOrderDto paymentOrder = Mapper.MapSITOrdenPagoToPaymentOrderRequest(ordenPago);

      paymentOrder.Attributes.Add("url", urlPaymentDocument);
      paymentOrder.Attributes.Add("mediaType", "application/pdf");

      return paymentOrder;
    }


    public async Task<string> GetPaymentStatus(string paymentOrderUID) {
      SITPaymentDto payment = await GetPayment(paymentOrderUID);

      return payment.Status;
    }

    #endregion Public methods

    #region Helpers

    private async Task<SITPaymentDto> GetPayment(string paymentOrderUID) {

      if (string.IsNullOrWhiteSpace(paymentOrderUID)) {
        Assertion.RequireFail("No ha sido generada una línea de captura para este trámite.");
      }

      int idPagoElectronico = 0;

      if (!int.TryParse(paymentOrderUID, out idPagoElectronico)) {
        Assertion.RequireFail("El identificador del recibo de pago no es numérico.");
      }

      var sitPayment = await _apiClient.ValidatePayment(idPagoElectronico);

      Assertion.Require(sitPayment != null && sitPayment.total != null,
                  $"No se encontró la información del recibo de pago " +
                  $"{idPagoElectronico} en la Secretaría de Finanzas.");

      return Mapper.MapSITPaymentToPayment(sitPayment);
    }

    #endregion Helpers

  } // class PaymentService

} // namespace Empiria.Zacatecas.Integration.SITFinanzasConnector
