/* Empiria Land **********************************************************************************************
*                                                                                                            *
*  Module   : Transaction Management                     Component : Interface adapters                      *
*  Assembly : SIT.Finanzas.Connector.dll                 Pattern   : Data Transfer Object                    *
*  Type     : OrdenPagoDto                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  :                                                                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Zacatecas.Integration.SITFinanzasConnector.Adapters {

  internal class PagoDto {

    public int IdCobro {
      get; set;
    }

    public string estatus {
      get; set;
    }

    public string fechaCobro {
      get; set;
    }

    public double total {
      get; set;
    }

    public string urlRecibo {
      get; set;
    }

  } // class PagoDto

} // namespace Empiria.Zacatecas.Integration.SITFinanzasConnector.Adapters
