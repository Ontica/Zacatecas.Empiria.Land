/* Empiria Land **********************************************************************************************
*                                                                                                            *
*  Module   : Transaction Management                     Component : Interface adapters                      *
*  Assembly : SIT.Finanzas.Connector.dll                 Pattern   : Mapper class                            *
*  Type     : Mapper                                     License   : Please read LICENSE.txt file            *
*                                                                                                            *
*  Summary  : Internal methods that map data to and from Data Transfer Objects.                              *
*                                                                                                            *
************************* Copyright(c) La Vía Óntica SC, Ontica LLC and contributors. All rights reserved. **/
using System;
using System.Collections.Generic;

using Empiria.Land.Integration.PaymentServices;

namespace Empiria.Zacatecas.Integration.SITFinanzasConnector.Adapters {

  /// <summary>Internal methods that map data to and from Data Transfer Objects.</summary>
  static internal class Mapper {

    #region Internal Methods


    static internal SolicitudDto MapPaymentRequestToSITRequest(PaymentOrderRequestDto paymentRequest) {
      SolicitudDto solicitud = new SolicitudDto();

      solicitud.contribuyente = paymentRequest.RequestedBy;
      solicitud.rfc = paymentRequest.RFC.Replace("-", String.Empty);
      solicitud.direccion = paymentRequest.Address;
      solicitud.servicios = MapConceptsToSITServices(paymentRequest.Concepts);
      solicitud.tramite = paymentRequest.BaseTransactionUID;

      return solicitud;
    }


    static internal PaymentOrderDto MapSITOrdenPagoToPaymentOrderRequest(OrdenPagoDto ordenPago) {
      PaymentOrderDto paymentOrder = new PaymentOrderDto();

      paymentOrder.UID = ordenPago.idPagoElectronico.ToString();

      paymentOrder.Issuer = "SECFIN";
      paymentOrder.Version = "1.0";

      DateTime dateValue;

      if (DateTime.TryParse(ordenPago.fechaGeneracion, out dateValue)) {
        paymentOrder.IssueTime = dateValue;
      }

      if (DateTime.TryParse(ordenPago.fechaVencimiento, out dateValue)) {
        paymentOrder.DueDate = dateValue;
      }

      paymentOrder.Total = ordenPago.total;

      paymentOrder.Status = ordenPago.idEstatus.ToString();

      return paymentOrder;
    }


    static internal SITPaymentDto MapSITPaymentToPayment(PagoDto SITPayment) {
      SITPaymentDto payment = new SITPaymentDto();

      payment.PaymentUID = SITPayment.IdCobro.ToString();
      payment.PaymentDate = SITPayment.fechaCobro;
      payment.PaymentDocumentURL = SITPayment.urlRecibo;
      payment.Total = Convert.ToDecimal(SITPayment.total);
      payment.Status = SITPayment.estatus;

      return payment;
    }


    #endregion Internal Methods

    #region Private Methods

    static private List<OrdenDto> MapConceptsToSITServices(IEnumerable<PaymentOrderRequestConceptDto> concepts) {
      var mappedServices = new List<OrdenDto>();

      foreach (PaymentOrderRequestConceptDto concept in concepts) {
        OrdenDto sitService = new OrdenDto();

        sitService.idServicio = Convert.ToInt32(concept.ConceptUID);
        sitService.cantidad = Convert.ToInt32(concept.Quantity);
        sitService.valor = concept.TaxableBase;

        mappedServices.Add(sitService);
      }

      return mappedServices;
    }

    #endregion Private Methods


    //static internal async Task<string> GetFormatPaymentURL(string electronicPaymentUIDaymentId) {
    //  int idPagoElectronico = Convert.ToInt32(electronicPaymentUIDaymentId);

    //  var apiClient = new ApiClient();

    //  return await apiClient.GetPaymentFormat(idPagoElectronico);
    //}


    //static private async Task<ServicioDto> GetSITService(string serviceUID) {
    //  List<ServicioDto> sitServices = await GetSITServices();

    //  var SITService = sitServices.Find(x => x.idServicio == Convert.ToInt32(serviceUID));

    //  if (SITService == null) {
    //    throw new Exception($"A service with UID '{serviceUID}' was not found.");
    //  }

    //  return SITService;
    //}


    //static private List<ServicioDto> _servicesCache = new List<ServicioDto>();
    //static private async Task<List<ServicioDto>> GetSITServices() {
    //  if ((_servicesCache == null) || (_servicesCache.Count == 0)) {
    //    var apiClient = new ApiClient();

    //    _servicesCache = await apiClient.GetServicesList();
    //  }

    //  return _servicesCache;
    //}


    //static internal async Task<PaymentOrderRequestConceptDto> GetFixedConceptCost(string serviceUID,
    //                                                                              decimal quantity) {
    //  var service = await GetSITService(serviceUID);

    //  var concept = new PaymentOrderRequestConceptDto();

    //  concept.ConceptUID = service.idServicio.ToString();
    //  concept.Quantity = quantity;
    //  concept.UnitCost = Convert.ToDecimal(service.importe);
    //  concept.Total = (concept.UnitCost * concept.Quantity);

    //  return concept;
    //}


    //static internal async Task<decimal> GetVariableConceptCost(string electronicPaymentUId,
    //                                                           string serviceUID,
    //                                                           decimal taxableBase) {
    //  var presupuesto = new PresupuestoDto();

    //  presupuesto.cantidad = 1;
    //  presupuesto.idPagoElectronico = Convert.ToInt32(electronicPaymentUId);
    //  presupuesto.idServicio = Convert.ToInt32(serviceUID);
    //  presupuesto.valor = taxableBase;

    //  var apiClient = new ApiClient();

    //  return await apiClient.GetVariableCost(presupuesto);
    //}


  } // class Mapper

} // namespace Empiria.Zacatecas.Integration.SITFinanzasConnector.Adapters
