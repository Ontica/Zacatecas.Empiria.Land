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

  internal class OrdenPagoDto {

    public int idPagoElectronico {
      get; set;
    }

    public int idEstatus {
      get; set;
    }

    public string fechaGeneracion {
      get; set;
    }

    public string fechaVencimiento {
      get; set;
    }

    public decimal total {
      get; set;
    }

    public OrdenPagoConceptoDto[] conceptos {
      get; set;
    }

    public string urlFormatoPago {
      get; set;
    }

  } // class OrdenPagoDto

  internal class OrdenPagoConceptoDto {
    public int idConcepto {
      get; set;
    }

    public string descripcion {
      get; set;
    }

    public decimal importe {
      get; set;
    }
  }

} // namespace Empiria.Zacatecas.Integration.SITFinanzasConnector.Adapters
