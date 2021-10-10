using System.Threading.Tasks;

namespace IR_tech_test.Service.Contracts
{
  public interface ICacheService<T>
  {
    Task<T> CacheTryGetValueSet(string key, T entries);
  }
}
