# Arquitectura de la solución DRAFTME_BACK

## 1. Visión general

La solución es una **API REST de ASP.NET Core** organizada en varios proyectos siguiendo una separación por capas:

```text
DRAFTME_API
	↓
DRAFTME_BUSINESS
	↓
DRAFTME_CORE

DRAFTME_API
	↓
DRAFTME_INFRA
	↓
DRAFTME_CORE
```

La responsabilidad de cada proyecto es:

| Proyecto | Responsabilidad |
|---|---|
| `DRAFTME_CORE` | Modelos, DTOs, enumeraciones e interfaces |
| `DRAFTME_BUSINESS` | Casos de uso, validaciones, mapeos y lógica de negocio |
| `DRAFTME_INFRA` | Base de datos, Entity Framework, repositorios y migraciones |
| `DRAFTME_API` | Entrada HTTP: controladores, middleware, configuración y arranque |

La aplicación se ejecuta realmente desde `DRAFTME_API`. Los otros proyectos son librerías que la API utiliza.

---

## 2. La solución `.sln`

El archivo `DRAFTME_BACK.sln` es un contenedor de Visual Studio. No contiene la lógica de la aplicación. Indica:

- Qué proyectos forman parte de la solución.
- Qué configuraciones existen: `Debug` y `Release`.
- Qué proyectos se compilan.
- Qué proyectos dependen de otros a nivel de solución.

Actualmente contiene estos cuatro proyectos:

```text
DRAFTME_API
DRAFTME_CORE
DRAFTME_INFRA
DRAFTME_BUSINESS
```

La solución permite abrirlos, compilarlos y ejecutarlos conjuntamente desde Visual Studio. La información concreta de cada proyecto está en su archivo `.csproj`.

---

## 3. Los archivos `.csproj`

Cada proyecto tiene un archivo `.csproj`, por ejemplo:

```text
DRAFTME_API/DRAFTME_API.csproj
```

Estos archivos definen:

- La versión de .NET utilizada.
- Si el proyecto es ejecutable o una librería.
- Los paquetes NuGet.
- Las referencias a otros proyectos.
- Opciones del compilador.

Todos los proyectos tienen configurado:

```xml
<TargetFramework>net10.0</TargetFramework>
```

Esto significa que están configurados para compilar contra **.NET 10**.

También tienen:

```xml
<Nullable>enable</Nullable>
```

Activa las comprobaciones de referencias anulables. Por ejemplo:

```csharp
public string? Escudo { get; set; }
```

indica que `Escudo` puede contener `null`.

En cambio:

```csharp
public string Nombre { get; set; }
```

indica que, conceptualmente, `Nombre` no debería ser nulo.

Además:

```xml
<ImplicitUsings>enable</ImplicitUsings>
```

permite utilizar algunos namespaces comunes sin declararlos manualmente en cada archivo.

---

## 4. Proyecto `DRAFTME_CORE`

Es el proyecto más independiente de la solución. Su `.csproj` no tiene referencias externas ni paquetes NuGet.

Su estructura es:

```text
DRAFTME_CORE
├── DTOs
├── Enums
├── Interfaces
└── Models
```

### 4.1. Models

Los modelos representan los conceptos principales de la aplicación:

```text
Categoria
HistoricoPlayer
HistoricoTeam
Oferta
Player
Scouter
Team
User
```

Por ejemplo, `Team` contiene propiedades simples, relaciones y colecciones:

```csharp
public class Team
{
	public int Id { get; set; }
	public string Nombre { get; set; }
	public DateTime Formacion { get; set; }
	public int NumSocios { get; set; }
	public string Estadio { get; set; }
	public int CategoriaId { get; set; }
	public Categoria Categoria { get; set; }
	public int Clasificacion { get; set; }
	public int Puntos { get; set; }
	public List<HistoricoTeam> Historico { get; set; } = new();
	public List<Oferta> Ofertas { get; set; } = new();
	public string? Escudo { get; set; }
}
```

Este modelo representa una entidad de negocio y también se utiliza como entidad de Entity Framework Core.

La relación:

```csharp
public int CategoriaId { get; set; }
public Categoria Categoria { get; set; }
```

representa una relación entre `Team` y `Categoria`.

### 4.2. DTOs

Los DTO son objetos destinados a transportar datos entre capas o entre la API y el cliente.

Ejemplos:

```text
TeamDTO
TeamDTOSumarized
PlayerDTO
UserDTO
OfertaDTO
```

La diferencia conceptual es:

- El modelo representa cómo funciona internamente la aplicación o cómo se guarda en la base de datos.
- El DTO representa qué datos se quieren exponer o recibir desde la API.

Por ejemplo, `TeamDTOSumarized` probablemente contiene menos información que `TeamDTO`. Esto evita devolver siempre todas las relaciones y propiedades de la entidad.

### 4.3. Interfaces

El archivo `DRAFTME_CORE/Interfaces/IRepository.cs` define una abstracción de repositorio:

```csharp
public interface IRepository<TEntity> where TEntity : class
{
	IQueryable<TEntity> Query { get; }
	void Add(TEntity entity);
	void Update(TEntity entity);
	void Delete(TEntity entity);
	void Attach(TEntity entity);
	Task SaveChangesAsync();
}
```

`DRAFTME_CORE` define la interfaz, pero no sabe cómo se implementa. La implementación está en `DRAFTME_INFRA`.

Esto permite que la lógica de negocio dependa de:

```csharp
IRepository<Team>
```

en vez de depender directamente de Entity Framework o MySQL.

---

## 5. Proyecto `DRAFTME_INFRA`

`DRAFTME_INFRA` es la capa de infraestructura. Se ocupa de los detalles externos de la aplicación:

- Base de datos.
- Entity Framework Core.
- MySQL.
- Repositorios.
- Migraciones.

Depende de `DRAFTME_CORE`.

### 5.1. `DraftMeDBContext`

El archivo `DRAFTME_INFRA/Context/DraftMeDBContext.cs` es el contexto de Entity Framework:

```csharp
public class DraftMeDBContext : DbContext
{
	public DraftMeDBContext(DbContextOptions<DraftMeDBContext> options)
		: base(options)
	{
	}

	public DbSet<Categoria> Categorias { get; set; }
	public DbSet<HistoricoPlayer> HistoricosPlayer { get; set; }
	public DbSet<HistoricoTeam> HistoricosTeam { get; set; }
	public DbSet<Player> Players { get; set; }
	public DbSet<Scouter> Scouters { get; set; }
	public DbSet<Team> Teams { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<Oferta> Ofertas { get; set; }
}
```

Cada `DbSet` representa normalmente una tabla o conjunto de entidades:

```csharp
public DbSet<Team> Teams { get; set; }
```

permite consultar los equipos desde la base de datos.

### 5.2. Configuración de MySQL

La extensión `DRAFTME_INFRA/Extensions/DraftMeDBContextExtension.cs` registra el contexto en el contenedor de dependencias:

```csharp
services.AddDbContext<DraftMeDBContext>(options =>
{
	options.UseMySql(
		configuration.GetConnectionString("DraftMe"),
		new MySqlServerVersion(new Version(8, 0, 0)),
		b => b.MigrationsAssembly(
			typeof(DraftMeDBContext).Assembly.FullName)
	);
});
```

Aquí se realizan varias tareas:

1. Se lee la cadena de conexión llamada `DraftMe`.
2. Se configura MySQL como proveedor.
3. Se indica que el servidor es MySQL 8.
4. Se indica que las migraciones están en el ensamblado de infraestructura.
5. Se registra `DraftMeDBContext` como servicio con duración `scoped`.

La cadena de conexión está en `DRAFTME_API/appsettings.json`:

```json
"DraftMe": "server=localhost;port=3306;database=draftme;user=root;password=root"
```

La aplicación espera que exista un servidor MySQL local escuchando en el puerto `3306`.

### 5.3. Repositorio

La clase `DRAFTME_INFRA/Repositories/Repository.cs` implementa la interfaz del proyecto Core:

```csharp
public class Repository<TEntity> : IRepository<TEntity>
	where TEntity : class
```

Internamente utiliza Entity Framework:

```csharp
public IQueryable<TEntity> Query => Table;
public void Add(TEntity entity) => Table.Add(entity);
public async Task SaveChangesAsync()
	=> await context.SaveChangesAsync();
```

La lógica de negocio no necesita conocer directamente `DraftMeDBContext`. Solamente utiliza `IRepository<TEntity>`.

### 5.4. Registro del repositorio

En `DRAFTME_INFRA/Extensions/ServiceExtension.cs` se registra esta relación:

```csharp
services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
```

Esto significa que cuando alguna clase solicite `IRepository<T>`, el contenedor proporcionará una instancia de `Repository<T>`.

---

## 6. Migraciones de Entity Framework

La carpeta `DRAFTME_INFRA/Migrations` contiene migraciones como:

```text
20250613150625_InitialCreate.cs
20250710161053_CatHisPla.cs
20250729113224_Ofertas.cs
```

Una migración representa un cambio estructural en la base de datos, por ejemplo:

- Crear las tablas iniciales.
- Añadir categorías e históricos.
- Añadir ofertas.

También existe:

```text
DraftMeDBContextModelSnapshot.cs
```

que representa el estado conocido actual del modelo de Entity Framework.

El flujo habitual es:

```text
Cambiar un modelo
		↓
Crear una migración
		↓
Aplicar la migración a MySQL
```

Comandos habituales:

```powershell
dotnet ef migrations add NombreDeLaMigracion --project DRAFTME_INFRA --startup-project DRAFTME_API
```

```powershell
dotnet ef database update --project DRAFTME_INFRA --startup-project DRAFTME_API
```

`--project` indica dónde guardar la migración y `--startup-project` indica qué proyecto contiene la configuración de ejecución.

---

## 7. Proyecto `DRAFTME_BUSINESS`

Este proyecto contiene los casos de uso de la aplicación.

Depende de `DRAFTME_CORE` y utiliza:

- MediatR.
- AutoMapper.
- FluentValidation.
- Entity Framework Core.
- Algunas abstracciones de ASP.NET Core.

Su estructura principal es:

```text
DRAFTME_BUSINESS
├── Extensions
├── Mappers
└── Services
```

### 7.1. Servicios organizados por funcionalidad

Los servicios están agrupados por entidad:

```text
Services
├── Categorias
├── Historicos
│   ├── Players
│   └── Teams
├── Ofertas
├── Players
├── Scouters
├── Teams
└── Users
```

Dentro de cada entidad normalmente existen:

```text
Commands
Queries
```

#### Commands

Los `Commands` modifican datos:

```text
Create
Update
Delete
```

#### Queries

Las `Queries` consultan datos:

```text
GetAll
GetById
GetByUsername
GetAllByPlayer
GetAllByTeam
```

Esta organización se parece al patrón **CQRS**: las consultas leen datos y los comandos modifican datos.

---

## 8. MediatR

MediatR se utiliza para enviar comandos y consultas a sus handlers correspondientes.

Por ejemplo, en `TeamController`:

```csharp
return await Mediator.Send(new GetAllTeams());
```

El controlador no llama directamente a `GetAllTeamsHandler`. MediatR busca automáticamente el handler correspondiente:

```csharp
IRequestHandler<GetAllTeams, List<TeamDTOSumarized>>
```

La solicitud es:

```csharp
public class GetAllTeams : IRequest<List<TeamDTOSumarized>>
{
}
```

Y el handler es:

```csharp
public class GetAllTeamsHandler
	: IRequestHandler<GetAllTeams, List<TeamDTOSumarized>>
```

El registro se hace en `DRAFTME_BUSINESS/Extensions/ServiceExtension.cs`:

```csharp
services.AddMediatR(configuration =>
{
	configuration.RegisterServicesFromAssembly(
		typeof(ServiceExtension).Assembly);
});
```

Esto indica a MediatR que busque automáticamente handlers dentro del ensamblado de `DRAFTME_BUSINESS`.

---

## 9. Handlers

Un handler contiene la implementación concreta de un caso de uso.

Por ejemplo, `CreateTeamHandler` recibe por inyección:

```csharp
IRepository<Team> repository
IValidator<CreateTeam> validator
IConfiguration configuration
IMapper mapper
```

Necesita:

- Un repositorio de equipos.
- Un validador del comando.
- Configuración de la aplicación.
- AutoMapper.

El flujo del handler es:

```text
1. Validar la petición
2. Guardar la imagen si existe
3. Crear la entidad Team
4. Añadirla al repositorio
5. Guardar cambios
6. Convertir Team a TeamDTO
7. Devolver el DTO
```

Estas instrucciones persisten la entidad:

```csharp
repository.Add(team);
await repository.SaveChangesAsync();
```

Y esta convierte el modelo interno en un DTO público:

```csharp
return mapper.Map<TeamDTO>(team);
```

---

## 10. Validaciones con FluentValidation

Los validadores están junto a los comandos:

```text
CreateTeamValidation.cs
UpdateTeamValidation.cs
```

Por ejemplo:

```csharp
RuleFor(x => x.Nombre)
	.MaximumLength(20)
	.WithMessage("Maximo nombre de 20 caracteres");
```

También hay validaciones que consultan la base de datos:

```csharp
RuleFor(x => x.CategoriaId)
	.MustAsync(async (id, cancellationToken) =>
		await repository.Query.AnyAsync(
			x => x.Id == id,
			cancellationToken));
```

Esto comprueba que la categoría indicada realmente existe.

Los validadores se registran manualmente:

```csharp
services.AddScoped<IValidator<CreateTeam>, CreateTeamValidation>();
services.AddScoped<IValidator<UpdateTeam>, UpdateTeamValidation>();
```

Cuando se crea un `CreateTeamHandler`, el contenedor puede proporcionarle automáticamente su `IValidator<CreateTeam>`.

---

## 11. AutoMapper

Los perfiles de AutoMapper están en:

```text
DRAFTME_BUSINESS/Mappers
```

Por ejemplo, `TeamProfile.cs` define:

```csharp
CreateMap<Team, TeamDTOSumarized>().ReverseMap();
CreateMap<Team, TeamDTO>().ReverseMap();
```

Esto permite conversiones entre:

```text
Team ↔ TeamDTOSumarized
Team ↔ TeamDTO
```

La configuración se registra así:

```csharp
services.AddAutoMapper(configuration =>
{
	configuration.AddMaps(typeof(ServiceExtension).Assembly);
});
```

AutoMapper busca los perfiles dentro del ensamblado de `DRAFTME_BUSINESS`.

---

## 12. Proyecto `DRAFTME_API`

Este es el proyecto ejecutable. Su `.csproj` utiliza:

```xml
<Project Sdk="Microsoft.NET.Sdk.Web">
```

La API referencia directamente a:

```xml
<ProjectReference Include="..\DRAFTME_BUSINESS\DRAFTME_BUSINESS.csproj" />
<ProjectReference Include="..\DRAFTME_INFRA\DRAFTME_INFRA.csproj" />
```

La API necesita ambos proyectos:

- `DRAFTME_BUSINESS` para ejecutar los casos de uso.
- `DRAFTME_INFRA` para registrar la base de datos y los repositorios.

### 12.1. `Program.cs`

Este archivo configura toda la aplicación.

#### Creación del builder

```csharp
var builder = WebApplication.CreateBuilder(args);
```

Crea el entorno de ejecución y carga automáticamente la configuración, el logging, las variables de entorno y los servicios de ASP.NET Core.

#### Controladores

```csharp
builder.Services.AddControllers();
```

Activa el sistema de controladores MVC/API.

#### Configuración

```csharp
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
```

Registra la configuración para que pueda inyectarse en otras clases.

#### CORS

```csharp
builder.Services.AddCors(...);
```

Permite que otros orígenes, por ejemplo un frontend, llamen a la API.

Actualmente la configuración permite prácticamente cualquier origen:

```csharp
.SetIsOriginAllowed(origin => true)
.AllowCredentials();
```

Esto puede ser útil durante el desarrollo, pero debería restringirse en producción.

#### Subida de archivos

```csharp
builder.Services.Configure<FormOptions>(options =>
{
	options.MultipartBodyLengthLimit = 10 * 1024 * 1024;
});
```

Permite peticiones `multipart/form-data` de hasta 10 MB. Esto se utiliza para subir imágenes como escudos.

#### Base de datos e infraestructura

```csharp
builder.Services.AddCustomDraftMeDBContext(config);
builder.Services.ConfigureInfraestructure();
builder.Services.AddBusinessLayer();
```

Estas llamadas registran:

1. El `DbContext`.
2. Los repositorios.
3. MediatR, validadores y AutoMapper.

#### Swagger

```csharp
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
```

Activa la generación de documentación OpenAPI y la interfaz Swagger.

---

## 13. Pipeline HTTP

Después de construir la aplicación:

```csharp
var app = builder.Build();
```

se configura el pipeline:

```csharp
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
```

Swagger solamente se habilita en el entorno de desarrollo.

Después:

```csharp
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
```

Cada petición pasa por estos componentes antes de llegar al controlador.

Finalmente:

```csharp
app.Run();
```

inicia el servidor web.

---

## 14. Controladores

Los controladores son la puerta de entrada HTTP.

Por ejemplo, `DRAFTME_API/Controllers/TeamController.cs` tiene:

```csharp
[Route("api/[controller]")]
[ApiController]
public class TeamController : BaseApiController
```

Por tanto, la ruta base es:

```text
/api/Team
```

### Endpoints de `TeamController`

#### Obtener todos los equipos

```csharp
[HttpGet("All")]
```

Ruta:

```text
GET /api/Team/All
```

Ejecuta:

```csharp
Mediator.Send(new GetAllTeams())
```

#### Obtener un equipo

```csharp
[HttpGet("{id}")]
```

Ruta:

```text
GET /api/Team/5
```

Ejecuta:

```csharp
Mediator.Send(new GetTeamById { Id = id })
```

#### Crear un equipo

```csharp
[HttpPost]
public async Task<TeamDTO> Create([FromForm] CreateTeam create)
```

Ruta:

```text
POST /api/Team
```

Utiliza `[FromForm]` porque el comando puede incluir un archivo:

```csharp
public IFormFile? Escudo { get; set; }
```

La petición debe ser `multipart/form-data`.

#### Eliminar un equipo

```csharp
[HttpDelete("{id}")]
```

Ruta:

```text
DELETE /api/Team/5
```

#### Actualizar un equipo

```csharp
[HttpPut("{id}")]
```

Ruta:

```text
PUT /api/Team/5
```

También utiliza `[FromForm]` porque permite actualizar la imagen.

---

## 15. Middleware de excepciones

El archivo `DRAFTME_API/Middlewares/ExceptionHandlingMiddleware.cs` intercepta excepciones no controladas:

```csharp
try
{
	await _next(context);
}
catch (Exception ex)
{
	await HandleExceptionAsync(context, ex);
}
```

Convierte ciertos tipos de excepción en códigos HTTP:

| Excepción | Respuesta |
|---|---|
| `KeyNotFoundException` | `404 Not Found` |
| `FormatException` | `422 Unprocessable Entity` |
| Cualquier otra excepción | `500 Internal Server Error` |

Esto evita que cada controlador tenga que repetir bloques `try/catch`.

---

## 16. Flujo completo de una petición

Tomemos esta petición:

```text
GET /api/Team/All
```

El flujo completo es:

```text
Cliente HTTP
	↓
ASP.NET Core
	↓
ExceptionHandlingMiddleware
	↓
Routing
	↓
TeamController.GetAllTeamsAsync()
	↓
MediatR
	↓
GetAllTeamsHandler
	↓
IRepository<Team>
	↓
Repository<Team>
	↓
DraftMeDBContext
	↓
MySQL
```

Después, la respuesta vuelve en sentido contrario:

```text
MySQL
	↓
Entity Framework
	↓
Repository
	↓
GetAllTeamsHandler
	↓
AutoMapper
	↓
List<TeamDTOSumarized>
	↓
TeamController
	↓
Respuesta JSON HTTP
```

El controlador coordina la petición, pero la consulta real está en el handler.

---

## 17. Inyección de dependencias

La aplicación utiliza ampliamente la **inyección de dependencias**.

Por ejemplo:

```csharp
public class CreateTeamHandler(
	IRepository<Team> repository,
	IValidator<CreateTeam> validator,
	IConfiguration configuration,
	IMapper mapper)
```

El handler no crea manualmente esos objetos con `new`. ASP.NET Core los crea automáticamente porque fueron registrados en `Program.cs` mediante:

```csharp
AddCustomDraftMeDBContext
ConfigureInfraestructure
AddBusinessLayer
```

Los tiempos de vida utilizados principalmente son:

- `Singleton`: una instancia para toda la aplicación.
- `Scoped`: una instancia por petición HTTP.
- `Transient`: una instancia cada vez que se solicita.

El repositorio y el `DbContext` son `Scoped`, que es el comportamiento habitual para trabajar con una petición HTTP y una unidad de trabajo de base de datos.

---

## 18. Configuración y entornos

Tienes estos archivos:

```text
appsettings.json
appsettings.Development.json
Properties/launchSettings.json
```

### `appsettings.json`

Contiene configuración general:

- Logging.
- Hosts permitidos.
- Cadena de conexión.
- Ruta de imágenes.

### `appsettings.Development.json`

Puede sobrescribir valores cuando la aplicación se ejecuta en desarrollo.

### `launchSettings.json`

Define cómo se inicia localmente:

- URLs HTTP y HTTPS.
- Perfil de ejecución.
- Entorno `Development`.
- Variables de entorno.

Cuando ejecutas desde Visual Studio, normalmente se utiliza uno de los perfiles definidos ahí.

---

## 19. Dependencias importantes

### `DRAFTME_BUSINESS`

Utiliza:

```text
MediatR
```

Para separar comandos, consultas y handlers.

```text
AutoMapper
```

Para convertir entidades en DTOs.

```text
FluentValidation
```

Para validar comandos.

```text
EntityFrameworkCore
```

Para utilizar extensiones de consulta como `Include`, `ToListAsync` o `AnyAsync`.

### `DRAFTME_INFRA`

Utiliza:

```text
Pomelo.EntityFrameworkCore.MySql
```

Como proveedor de Entity Framework para MySQL.

```text
Microsoft.EntityFrameworkCore.Design
```

Para crear migraciones y trabajar con herramientas de Entity Framework.

```text
MySqlConnector
```

Para la conexión con MySQL.

### `DRAFTME_API`

Utiliza:

```text
Swashbuckle.AspNetCore
```

Para Swagger/OpenAPI.

---

## 20. Cómo ejecutar la solución

Desde Visual Studio:

1. Abre `DRAFTME_BACK.sln`.
2. Establece `DRAFTME_API` como proyecto de inicio.
3. Comprueba que MySQL está ejecutándose.
4. Comprueba la cadena de conexión.
5. Ejecuta con `F5` o `Ctrl + F5`.
6. Abre Swagger en la URL indicada por `launchSettings.json`.

Desde PowerShell, en la raíz de la solución:

```powershell
dotnet restore
dotnet build
dotnet run --project DRAFTME_API
```

Para compilar toda la solución:

```powershell
dotnet build DRAFTME_BACK.sln
```

---

## 21. Aspectos a revisar al retomar el proyecto

### Versiones de .NET y Entity Framework

Los proyectos apuntan a:

```text
net10.0
```

pero varias dependencias de Entity Framework utilizan la versión `9.0.0`, por ejemplo:

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.0" />
```

y:

```xml
<PackageReference Include="Pomelo.EntityFrameworkCore.MySql" Version="9.0.0" />
```

Conviene revisar que las versiones principales de .NET, Entity Framework Core y Pomelo sean compatibles entre sí. No necesariamente implica que el proyecto no compile, pero es una de las primeras cosas que comprobar al actualizar o restaurar el proyecto.

### Credenciales en `appsettings.json`

La contraseña de MySQL está escrita directamente en el archivo:

```json
"password=root"
```

Para desarrollo puede funcionar, pero en producción debería utilizarse Secret Manager, variables de entorno, Azure Key Vault u otro sistema de secretos.

### Ruta absoluta de imágenes

Actualmente existe una ruta específica de Windows:

```json
"ImagesPath": "C:\\Users\\josec\\OneDrive\\Documentos\\GitHub\\DRAFTME_FRONT\\DRAFTME_FRONT\\public"
```

La aplicación solamente funcionará correctamente si esa carpeta existe en esa máquina. En otro ordenador será necesario cambiarla.

### CORS

La configuración actual permite cualquier origen y credenciales:

```csharp
.SetIsOriginAllowed(origin => true)
.AllowCredentials();
```

Es apropiado revisarlo antes de publicar la API.

### Autorización

El pipeline contiene:

```csharp
app.UseAuthorization();
```

pero no se observa en `Program.cs` una configuración de autenticación con `AddAuthentication()`. Además, los controladores no parecen utilizar atributos como `[Authorize]`. Por tanto, actualmente `UseAuthorization()` no parece estar protegiendo endpoints de forma efectiva.

### Ausencia de proyecto de tests

La solución contiene cuatro proyectos, pero no aparece un proyecto separado de pruebas automatizadas. Por ahora no hay una capa específica con tests unitarios o de integración.

---

## 22. Resumen mental

Puedes pensar en la aplicación así:

```text
DRAFTME_API
"Recibe peticiones HTTP"
		↓
DRAFTME_BUSINESS
"Decide qué debe hacer la aplicación"
		↓
DRAFTME_CORE
"Define los modelos y contratos"
		↑
DRAFTME_INFRA
"Implementa el acceso a base de datos"
```

Para añadir una funcionalidad nueva, normalmente seguirías este proceso:

1. Añadir o modificar un modelo en `DRAFTME_CORE/Models`.
2. Añadir el DTO correspondiente en `DRAFTME_CORE/DTOs`.
3. Crear un comando o consulta en `DRAFTME_BUSINESS/Services`.
4. Crear su handler.
5. Crear su validador si es necesario.
6. Añadir el mapeo en `DRAFTME_BUSINESS/Mappers`.
7. Añadir o modificar el endpoint en `DRAFTME_API/Controllers`.
8. Si cambia el modelo de base de datos, crear una migración.
9. Aplicar la migración a MySQL.
10. Probar el endpoint desde Swagger o desde el frontend.

La arquitectura actual es una API monolítica separada internamente por capas. No son microservicios independientes, sino varios proyectos compilados y ejecutados como una única aplicación web.
