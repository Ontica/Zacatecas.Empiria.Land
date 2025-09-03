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


    public async Task<IPaymentOrder> GeneratePaymentOrderFor(PaymentOrderRequestDto paymentOrderRequest) {
      SolicitudDto sitRequest = Mapper.MapPaymentRequestToSITRequest(paymentOrderRequest);

      OrdenPagoDto ordenPago = await _apiClient.CreatePaymentRequest(sitRequest);

      string urlPaymentDocument = await _apiClient.GetPaymentFormat(ordenPago.idPagoElectronico);

      PaymentOrderDto paymentOrder = Mapper.MapSITOrdenPagoToPaymentOrderRequest(ordenPago);

      paymentOrder.Attributes.Add("url", urlPaymentDocument);
      paymentOrder.Attributes.Add("mediaType", "application/pdf");

      return paymentOrder;
    }


    public async Task<string> GetPaymentStatus(IPaymentOrder paymentOrder) {

      if (string.IsNullOrWhiteSpace(paymentOrder.UID)) {
        Assertion.RequireFail("Este trámite no generada una línea de captura.");
      }

      int idPagoElectronico = 0;

      if (!int.TryParse(paymentOrder.UID, out idPagoElectronico)) {
        Assertion.RequireFail("El identificador del recibo de pago no es numérico.");
      }

      var SITPayment = await _apiClient.ValidatePayment(idPagoElectronico);

      var payment = Mapper.MapSITPaymentToPayment(SITPayment);

      return payment.Status;
    }

    #endregion Public methods

  } // class PaymentService

} // namespace Empiria.Zacatecas.Integration.SITFinanzasConnector
