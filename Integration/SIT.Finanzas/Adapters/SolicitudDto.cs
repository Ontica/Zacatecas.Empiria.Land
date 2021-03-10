/* Empiria Land **********************************************************************************************
*                                                                                                            *
*  Module   : Transaction Management                     Component : Interface adapters                      *
*  Assembly : SIT.Finanzas.Connector.dll                 Pattern   : Data Transfer Object                    *
*  Type     : SolicitudDto                               License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  :                                                                                                *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System.Collections.Generic;

namespace Empiria.Zacatecas.Integration.SITFinanzasConnector.Adapters {

  internal class SolicitudDto {

    /// <summary>DTO used to create a SIT Request.</summary>
    public string contribuyente {
      get; set;
    }

    public string rfc {
      get; set;
    }

    public string direccion {
      get; set;
    }

    public IEnumerable<OrdenDto> servicios {
      get; set;
    }

    public string tramite {
      get; set;
    }

  } // class SolicitudDto


  /// <summary></summary>
  internal class OrdenDto {

    public int idServicio {
      get; set;
    }

    public int cantidad {
      get; set;
    }

    public decimal valor {
      get; set;
    } = 0;

    public int idActoContrato {
      get; set;
    } = 0;

    public string tipoInmueble {
      get; set;
    } = string.Empty;

  } // class OrdenDto


  /// <summary>DTO with data related to SIT service info.</summary>
  internal class ServicioDto {

    public int idServicio {
      get; set;
    }

    public string servicio {
      get; set;
    }

    public string importe {
      get; set;
    }

  } // class ServicioDto


} // namespace Empiria.Zacatecas.Integration.SITFinanzasConnector.Adapters
