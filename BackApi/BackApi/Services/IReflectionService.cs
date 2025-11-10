using System.Collections.Generic;

namespace BackApi.Services
{
  public interface IReflectionService
  {
    IEnumerable<string> GetImporterDllNames();
  }
}
