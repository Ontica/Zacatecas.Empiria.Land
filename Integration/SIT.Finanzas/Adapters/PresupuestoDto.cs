/* Empiria Land **********************************************************************************************
*                                                                                                            *
*  Module   : Transaction Management                     Component : Interface adapters                      *
*  Assembly : SIT.Finanzas.Connector.dll                 Pattern   : Data Transfer Object                    *
*  Type     : ServicioPresupuestoDto                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  :                                                                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/

namespace Empiria.Zacatecas.Integration.SITFinanzasConnector.Adapters {

  /// <summary></summary>
  internal class PresupuestoDto {

    public int idServicio {
      get; set;
    }

    public int cantidad {
      get; set;
    }

    public decimal valor {
      get; set;
    }

    public int idPagoElectronico {
      get; set;
    }

  }  // class ServicioPresupuestoDto


  /// <summary></summary>
  internal class PresupuestoRespuestaDto {

    public int idPresupuesto {
      get; set;
    }

    public int cantidad {
      get; set;
    }

    public decimal importeTotal {
      get; set;
    }

  }  // class ServicioPresupuestoDto


}  // namespace Empiria.Zacatecas.Integration.SITFinanzasConnector.Adapters
