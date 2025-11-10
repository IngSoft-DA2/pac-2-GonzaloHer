using System.Reflection;
using IImporter;

namespace BackApi.Services
{
  public class ReflectionService : IReflectionService
  {
    private readonly ILogger<ReflectionService> _logger;
    private readonly string _pluginsPath;

    public ReflectionService(ILogger<ReflectionService> logger)
    {
      _logger = logger;
      _pluginsPath = Path.Combine(Directory.GetCurrentDirectory(), "reflection");
    }

    public IEnumerable<string> GetImporterDllNames()
    {
      var importerNames = new List<string>();

      if (!Directory.Exists(_pluginsPath))
      {
        Directory.CreateDirectory(_pluginsPath);
        _logger.LogInformation($"Directorio 'reflection' creado en: {_pluginsPath}");
        return importerNames;
      }

      var dllFiles = Directory.GetFiles(_pluginsPath, "*.dll", SearchOption.TopDirectoryOnly);

      foreach (var dllPath in dllFiles)
      {
        try
        {
          var assembly = Assembly.LoadFrom(dllPath);

          var validTypes = assembly.GetTypes()
              .Where(t =>
                  t.IsClass &&
                  !t.IsAbstract &&
                  typeof(ImporterInterface).IsAssignableFrom(t))
              .ToList();

          foreach (var type in validTypes)
          {
            try
            {
              if (Activator.CreateInstance(type) is ImporterInterface instance)
              {
                var name = instance.GetName();
                importerNames.Add(name);
              }
            }
            catch (Exception ex)
            {
              _logger.LogWarning($"No se pudo instanciar {type.Name} de {dllPath}: {ex.Message}");
            }
          }
        }
        catch (ReflectionTypeLoadException ex)
        {
          _logger.LogWarning($"Error al cargar tipos desde {dllPath}: {ex.Message}");
        }
        catch (Exception ex)
        {
          _logger.LogWarning($"No se pudo analizar {dllPath}: {ex.Message}");
        }
      }

      return importerNames;
    }

  }
}
