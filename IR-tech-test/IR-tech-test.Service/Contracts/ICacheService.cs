using System.Threading.Tasks;

namespace IR_tech_test.Service.Contracts
{
  public interface ICacheService<T>
  {
    T Get(string key);
    void Set(string key, T values);
  }
}
