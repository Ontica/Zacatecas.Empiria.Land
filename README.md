# Sistema del Registro Público de la Propiedad del Gobierno del Estado de Zacatecas (Backend)

El Sistema del Registro Público de la Propiedad del [Gobierno del Estado de Zacatecas](https://zacatecas.gob.mx/)
(RIDEZAC), está siendo modernizado por nuestra organización.

Este repositorio contiene la capa de adaptación de los componentes del *backend* de nuestro sistema de propósito
general para registros públicos de la propiedad (conocido como [Empiria Land](https://github.com/Ontica/Empiria.Land)),
conforme a las necesidades específicas del gobierno del Estado.

Así mismo, contiene los componentes desarrollados especialmente para la integración del Sistema con
el sistema de recaudación de la Secretaría de Finanzas y con el sistema de firma electrónica de la
Secretaría de la Función Pública.

Es para nosotros motivo de orgullo poder contribuir al desarrollo de Zacatecas con este Sistema.

## Contenido

El *backend* de *Empiria Land* adaptado a las necesidades de Zacatecas contiene los siguientes componentes de software:

1.  **AppServices**  
    Contiene casos de uso especiales para la oficina del Registro Público de la Propiedad
    y sirve como agrupador de servicios web de tipo Http/Json de la solución.

    A través de sus componentes es posible modificar el comportamiento predeterminado
    de las web apis, y de los componentes y de los casos de uso que conforman *Empiria Land*.

2.  **SeguriSign.Connector**  
    Permite conectarse a los servicios web de firma electrónica proporcionados por la Secretaría de la Función Pública.

3.  **SIT.Finanzas**  
    Componentes para conectarse a los servicios proporcionados por el sistema SIT de la Secretaría de Finanzas.

## Licencia

Este producto y sus partes se distribuyen mediante una licencia GNU AFFERO
GENERAL PUBLIC LICENSE, para uso exclusivo del Gobierno del Estado de
Zacatecas y de su personal.

Para cualquier otro uso (con excepción a lo estipulado en los Términos de
Servicio de GitHub), es indispensable obtener con nuestra organización una
licencia distinta a esta.

Lo anterior restringe la distribución, copia, modificación, almacenamiento,
instalación, compilación o cualquier otro uso del producto o de sus partes,
a terceros, empresas privadas o a su personal, sean o no proveedores de
servicios de las entidades públicas mencionadas.

El desarrollo, evolución y mantenimiento de este producto está siendo pagado
en su totalidad con recursos públicos, y está protegido por las leyes nacionales
e internacionales de derechos de autor.

## Copyright

Copyright © 2009-2024. La Vía Óntica SC, Ontica LLC y autores.
Todos los derechos reservados.
