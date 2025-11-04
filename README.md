# AppStoreBooks

## Descripción

AppStoreBooks es una aplicación web desarrollada en ASP.NET Core MVC con .NET 9.0. Permite gestionar libros y categorías, y utiliza Entity Framework Core para interactuar con la base de datos.

## Características

- Gestión de usuarios con Identity.
- CRUD de libros y categorías.
- Diseño responsivo utilizando Bootstrap.
- Configuración de base de datos con Entity Framework Core.

## Requisitos

- .NET 9.0 SDK
- Visual Studio 2022 o superior
- SQL Server (o cualquier base de datos compatible con Entity Framework Core)

## Instalación

1. Clona este repositorio:
   ```bash
   git clone https://github.com/Santiagocrocs05/AppStoreBooks.git
   ```
2. Navega al directorio del proyecto:
   ```bash
   cd AppStoreBooks/src/AppStore
   ```
3. Restaura los paquetes NuGet:
   ```bash
   dotnet restore
   ```
4. Configura la cadena de conexión en `appsettings.json`.

## Ejecución

1. Compila el proyecto:
   ```bash
   dotnet build
   ```
2. Ejecuta la aplicación:
   ```bash
   dotnet run
   ```

## Estructura del Proyecto

```
AppStore/
├── Controllers/       # Controladores MVC
├── Models/            # Modelos de datos
├── Views/             # Vistas de la aplicación
├── wwwroot/           # Archivos estáticos (CSS, JS, imágenes)
├── appsettings.json   # Configuración de la aplicación
├── Program.cs         # Punto de entrada de la aplicación
```

## Tecnologías Utilizadas

- ASP.NET Core MVC
- Entity Framework Core
- Bootstrap
- SQL Server

## Contribución

1. Haz un fork del repositorio.
2. Crea una nueva rama:
   ```bash
   git checkout -b feature/nueva-funcionalidad
   ```
3. Realiza tus cambios y haz un commit:
   ```bash
   git commit -m "Agrega nueva funcionalidad"
   ```
4. Envía tus cambios:
   ```bash
   git push origin feature/nueva-funcionalidad
   ```
5. Abre un Pull Request.

## Licencia

Este proyecto está bajo la licencia MIT. Consulta el archivo `LICENSE` para más detalles.
