# Image Tracking AR Museum (Unity)

Aplicación de realidad aumentada desarrollada en Unity utilizando AR Foundation.

El proyecto permite detectar imágenes físicas mediante la cámara del dispositivo móvil y desplegar modelos 3D interactivos junto con información contextual en pantalla.

## Características principales

Detección de múltiples imágenes mediante image tracking
Aparición dinámica de modelos 3D sobre la imagen detectada
Rotación de objetos mediante interacción táctil
Panel informativo UI independiente del objeto AR
Gestión del estado de tracking de los objetos

## Sistema de tracking implementado

El sistema utiliza ARTrackedImageManager para detectar imágenes físicas
y asociarlas dinámicamente con prefabs mediante un diccionario
imagen → objeto 3D.

Los objetos se instancian frente a la cámara del usuario para mejorar
la visibilidad y se orientan automáticamente hacia el observador.

Además se implementó:

- gestión del estado TrackingState
- destrucción automática de objetos al perder tracking
- panel informativo UI independiente del objeto AR
- botón de limpieza manual de objetos activos

## Tecnologías utilizadas

Unity
C#
AR Foundation
Mobile Touch Input
Canvas UI System

## Mi rol en el proyecto

Implementé el sistema de detección de imágenes mediante AR Foundation, la aparición de modelos 3D asociados a cada imagen y el panel informativo interactivo mostrado en pantalla.

Además desarrollé la interacción táctil para manipulación de objetos y la lógica de gestión del estado de tracking.

Proyecto desarrollado como aplicación educativa tipo museo interactivo en AR.
