# Explorador de Archivos

Aplicación de escritorio en C# (.NET 8 / WinForms) para navegar, organizar y gestionar archivos del sistema con funcionalidades integradas de multimedia, base de datos y conversión de documentos.

## Descripción

Este proyecto es un explorador de archivos completo que va más allá de la navegación tradicional. Permite visualizar documentos sin salir de la aplicación, reproducir audio y video, grabar contenido desde la cámara, respaldar archivos en bases de datos y convertirlos entre múltiples formatos.

## Características principales

- **Navegación de archivos** con vista en cuadrícula y lista, búsqueda, copiar, mover, eliminar y renombrar
- **Visores internos** para PDF (WebView2), DOCX, XLSX, PPTX y RTF sin necesidad de abrir programas externos
- **Reproductor de audio** con metadatos automáticos (iTunes Search API) y letras de canciones (lyrics.ovh)
- **Reproductor de video** con LibVLC integrado
- **Cámara** con captura de fotos JPG y grabación de video AVI MJPEG (sin dependencias externas)
- **Grabador de audio** con reproducción inline
- **Base de datos** SQLite local y SQL Server remoto con respaldo binario de archivos
- **Recuperación de archivos** desde la base de datos en caso de eliminación accidental
- **Envío de archivos por correo** mediante Gmail SMTP
- **Convertidor de documentos** entre Word, Excel, PowerPoint y PDF (vía COM Interop)
- **Convertidor de datos** entre JSON, CSV, XML y TXT
- **Limpiador de datos** con detección de duplicados y validación
- **Editor de imágenes** con filtros básicos
- **Geolocalización** de fotos con visualización en mapa Leaflet
- **Estadísticas** y gráficas por tipo de archivo

## Requisitos del sistema

- Windows 10 / 11
- .NET 8 SDK o Runtime
- Microsoft Office instalado (opcional, solo para el convertidor de documentos Office)
- SQL Server (opcional, solo para conexión remota)
- WebView2 Runtime (preinstalado en Windows 10/11)

## Instalación

1. Clona o descarga el repositorio.
2. Abre la solución `Explorador de Archivo.sln` en Visual Studio 2022.
3. Restaura los paquetes NuGet (clic derecho en la solución → Restaurar paquetes NuGet).
4. Compila el proyecto (Ctrl+Shift+B).
5. Ejecuta con F5 o el botón de Inicio.

## Configuración de SQL Server (opcional)

Si quieres usar SQL Server, edita el archivo `DatabaseConfig.cs` y completa los campos:

```csharp
public static class SqlServer
{
    public const string Host     = "localhost\\SQLEXPRESS";
    public const string Port     = "1433";
    public const string Database = "ExploradorDB";
    public const string User     = "sa";
    public const string Password = "tu_contraseña";
}
```

Además debes:
1. Habilitar TCP/IP en SQL Server Configuration Manager
2. Activar autenticación mixta en SQL Server Management Studio
3. Crear la base de datos: `CREATE DATABASE ExploradorDB;`

## Configuración de correo Gmail (opcional)

Para enviar archivos por correo desde la aplicación necesitas una **contraseña de aplicación** de Gmail:

1. Ve a [myaccount.google.com](https://myaccount.google.com)
2. Seguridad → Verificación en 2 pasos → Activar
3. Busca "Contraseñas de aplicaciones" → Crear una nueva
4. Copia los 16 caracteres en el formulario de correo

## Estructura del proyecto

```
Explorador de Archivo/
├── explor.cs                  # Coordinador principal Form1 (partial class)
├── ExplorerUI.cs              # Construcción de la interfaz
├── ExplorerNavigation.cs      # Navegación e historial
├── ExplorerRender.cs          # Renderizado de grid y lista
├── ExplorerFileOps.cs         # Operaciones de archivo
├── DocumentViewer.cs          # Visor de PDF/DOCX/XLSX/PPT
├── DatabaseViewerForm.cs      # Visor de base de datos
├── DatabaseExportForm.cs      # Exportar archivos a BD
├── StatisticsForm.cs          # Gráficas estadísticas
├── DatabaseConfig.cs          # Configuración de SQL Server
├── Services.cs                # DatabaseService SQLite
├── Models.cs                  # Modelos de datos
├── Theme.cs                   # Tema visual oscuro
├── FilePicker.cs              # Selector de archivos interno
├── CameraForm.cs              # Cámara y grabación
├── RecorderForm.cs            # Grabador de audio
├── MediaForms.cs              # Reproductores audio y video
├── AviVideoWriter.cs          # Escritor AVI MJPEG en .NET puro
├── EmailForm.cs               # Envío de correos Gmail
├── ConverterForm.cs           # Convertidor de datos
├── DocumentConverterForm.cs   # Convertidor de Office
├── CleanerForm.cs             # Limpiador de datos
├── ImageEditorForm.cs         # Editor de imágenes
├── GeoImageForm.cs            # Geolocalización de fotos
├── chartform.cs               # Graficador
├── audioservice.cs            # Servicio de metadatos iTunes
└── Program.cs                 # Punto de entrada
```

## Tecnologías y librerías utilizadas

- **AForge.NET** — captura de video desde la cámara
- **NAudio** — captura y reproducción de audio
- **LibVLCSharp** — reproducción de video
- **Microsoft.Data.Sqlite** — base de datos local
- **Microsoft.Data.SqlClient** — conexión a SQL Server
- **Microsoft.Web.WebView2** — visor de PDF embebido
- **CsvHelper** — lectura y escritura de CSV
- **Newtonsoft.Json** — manejo de JSON
- **MailKit** — envío de correos electrónicos
- **iTunes Search API** — metadatos de canciones (gratis, sin autenticación)
- **lyrics.ovh** — letras de canciones (gratis, sin autenticación)

## Uso básico

1. **Navegar**: usa el panel izquierdo para acceder a carpetas frecuentes o escribe una ruta arriba.
2. **Ver archivo**: doble clic abre el visor correspondiente según la extensión.
3. **Respaldar en BD**: selecciona un archivo y presiona "🗄 → DB" en el panel derecho.
4. **Recuperar**: abre "Base de Datos" desde el menú, selecciona el registro y presiona "♻ Recuperar archivo".
5. **Enviar por correo**: selecciona un archivo y presiona "📧 Enviar por correo".
6. **Grabar video**: abre la cámara desde el menú, presiona "▶ Iniciar" y luego "🔴 Grabar".

## Autor

Proyecto desarrollado para la materia de Programación Avanzada.

## Licencia

Uso académico.
