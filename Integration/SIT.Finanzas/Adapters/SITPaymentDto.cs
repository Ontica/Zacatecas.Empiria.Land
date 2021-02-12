/* Empiria Land **********************************************************************************************
*                                                                                                            *
*  Module   : Transaction Management                     Component : Integration Layer                       *
*  Assembly : SIT.Finanzas.Connector.dll                 Pattern   : Data Transfer Object                    *
*  Type     : ApiClient                                  License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : DTO used to get a Payment.                                                                     *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Zacatecas.Integration.SITFinanzasConnector.Adapters {

  internal class SITPaymentDto {

    public string PaymentUID {
      get; set;
    }

    public string PaymentDate {
      get; set;
    }

    public string Status {
      get; set;
    }

    public string PaymentDocumentURL {
      get; set;
    }

    public decimal Total {
      get; set;
    }

  }  // class SITPaymentDto

} // namespace Empiria.Zacatecas.Integration.SITFinanzasConnector.Adapters
