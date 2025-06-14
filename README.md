# Basic Supermarket API

API REST para un sistema de supermercado básico desarrollado en .NET.

## 🚀 Características

- Autenticación y autorización con JWT
- Gestión de roles de usuario
- API RESTful
- Documentación con Swagger
- Base de datos MySQL
- Containerización con Docker
- Validación de datos
- Manejo centralizado de errores

## 🛠️ Tecnologías

- .NET 8
- Entity Framework Core
- MySQL
- JWT Authentication
- Swagger/OpenAPI
- Docker
- FluentValidation
- AutoMapper

## 📋 Prerrequisitos

- .NET 8 SDK
- Docker y Docker Compose
- MySQL (si se ejecuta localmente sin Docker)

## 🔧 Configuración

1. Clona el repositorio:
```bash
git clone [URL_DEL_REPOSITORIO]
cd BasicSupermarket
```

2. Configura las variables de entorno:
   - Copia `appsettings.Development.json` a `appsettings.json`
   - Ajusta la cadena de conexión y otras configuraciones según tu entorno

3. Ejecuta con Docker:
```bash
docker-compose up -d
```

4. O ejecuta localmente:
```bash
dotnet restore
dotnet run
```

## 📚 Documentación de la API

Una vez que la aplicación esté en ejecución, puedes acceder a la documentación Swagger en:
```
https://localhost:5001/swagger
```

## 🔐 Autenticación

La API utiliza JWT para la autenticación. Para acceder a los endpoints protegidos:

1. Obtén un token mediante el endpoint de login
2. Incluye el token en el header de las peticiones:
```
Authorization: Bearer {tu_token}
```

## 🏗️ Estructura del Proyecto

```
BasicSupermarket/
├── Controllers/     # Endpoints de la API
├── Services/        # Lógica de negocio
├── Domain/         # Entidades del dominio
├── Persistence/    # Configuración de BD y repositorios
├── Dtos/           # Objetos de transferencia de datos
├── Mapping/        # Configuración de AutoMapper
└── Config/         # Configuraciones de la aplicación
```

## 🧪 Testing

Para ejecutar los tests:
```bash
dotnet test
```

## 📦 Despliegue

### Con Docker
```bash
docker-compose up -d
```

### Sin Docker
```bash
dotnet publish -c Release
dotnet run --project BasicSupermarket
```

## 🤝 Contribución

1. Fork el proyecto
2. Crea una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📝 Licencia

Este proyecto está bajo la Licencia MIT - ver el archivo [LICENSE.md](LICENSE.md) para más detalles. 