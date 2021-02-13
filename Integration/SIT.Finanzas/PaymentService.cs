/* Empiria Land **********************************************************************************************
*                                                                                                            *
*  Module   : Transaction Management                     Component : Integration Layer                       *
*  Assembly : SIT.Finanzas.Connector.dll                 Pattern   : Provider implementation                 *
*  Type     : PaymentService                             License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Implements IPaymentService interface using Zacatecas Finanzas SIT services.                    *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Threading.Tasks;

using Empiria.Land.Integration.PaymentServices;

using Empiria.Zacatecas.Integration.SITFinanzasConnector.Adapters;

namespace Empiria.Zacatecas.Integration.SITFinanzasConnector {

  /// <summary>Implements IPaymentService interface using Zacatecas Finanzas SIT services.</summary>
  public class PaymentService : IPaymentService {

    #region Public methods

    public async Task<decimal> CalculateFixedFee(string serviceUID, decimal quantity) {
      // Zacatecas' SIT service needs Shopping Cart to perform one by one fee calculation.
      return await Task.FromResult(0m);
    }


    public async Task<decimal> CalculateVariableFee(string serviceUID, decimal taxableBase) {
      // Zacatecas' SIT service needs Shopping Cart to perform one by one fee calculation.
      return await Task.FromResult(0m);
    }


    public async Task<IPaymentOrder> GeneratePaymentOrderFor(PaymentOrderRequestDto paymentOrderRequest) {
      return await Mapper.GetPaymentRequest(paymentOrderRequest);
    }


    public async Task<string> GetPaymentStatus(IPaymentOrder paymentOrder) {
      var payment = await Mapper.GetPayment(paymentOrder.UID);

      return payment.Status;
    }

    #endregion Public methods

  } // class PaymentService

} // namespace Empiria.Zacatecas.Integration.SITFinanzasConnector
