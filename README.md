# BasicSupermarket API

Una API RESTful para la gestión de un supermercado, desarrollada con .NET 8.0. Este proyecto implementa funcionalidades para gestionar productos, categorías, carritos de compra y órdenes.

## 🚀 Características

- Gestión de productos y categorías
- Sistema de carrito de compras
- Procesamiento de órdenes
- Autenticación de usuarios
- Manejo de errores centralizado
- Tests unitarios
- Documentación de API con Swagger
- Containerización con Docker

## 🛠️ Tecnologías

- .NET 8.0
- Entity Framework Core
- AutoMapper
- Docker
- GitHub Actions para CI/CD

## 📋 Prerrequisitos

- .NET 8.0 SDK
- Docker (opcional)
- Un IDE como Visual Studio o Rider

## 🔧 Instalación y Ejecución

### Ejecución Local

1. Clonar el repositorio:
```bash
git clone [URL-del-repositorio]
```

2. Navegar al directorio del proyecto:
```bash
cd BasicSupermarket
```

3. Restaurar dependencias:
```bash
dotnet restore
```

4. Ejecutar el proyecto:
```bash
dotnet run --project BasicSupermarket
```

### Usando Docker

1. Construir la imagen:
```bash
docker-compose build
```

2. Ejecutar los contenedores:
```bash
docker-compose up
```

## 🗄️ Estructura del Proyecto

```
BasicSupermarket/
├── Controllers/        # Controladores de la API
├── Domain/            # Entidades y lógica de dominio
├── Services/          # Servicios de la aplicación
├── Persistence/       # Acceso a datos y migraciones
├── Dtos/             # Objetos de transferencia de datos
├── Config/           # Configuraciones
└── Mapping/          # Perfiles de AutoMapper
```

## 🔍 API Endpoints

- `GET /api/products` - Obtener todos los productos
- `GET /api/products/{id}` - Obtener un producto específico
- `POST /api/products` - Crear un nuevo producto
- `PUT /api/products/{id}` - Actualizar un producto
- `DELETE /api/products/{id}` - Eliminar un producto

Similar estructura para:
- Categories (`/api/categories`)
- Cart (`/api/cart`)
- Orders (`/api/orders`)

## 🧪 Tests

Para ejecutar los tests:

```bash
dotnet test
```

## 👥 Contribución

1. Fork el proyecto
2. Crea tu rama de feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abre un Pull Request

## 📄 Licencia

Este proyecto está bajo la Licencia MIT - ver el archivo [LICENSE.md](LICENSE.md) para más detalles.
