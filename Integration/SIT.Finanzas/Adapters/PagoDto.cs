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

    public int? idCobro {
      get; set;
    } = null;


    public string estatus {
      get; set;
    } = string.Empty;


    public string fechaCobro {
      get; set;
    } = string.Empty;


    public decimal? total {
      get; set;
    } = null;


    public string urlRecibo {
      get; set;
    } = string.Empty;

  } // class PagoDto

} // namespace Empiria.Zacatecas.Integration.SITFinanzasConnector.Adapters
