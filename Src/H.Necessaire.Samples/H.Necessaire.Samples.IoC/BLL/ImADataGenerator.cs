using System.Threading.Tasks;

namespace H.Necessaire.Samples.IoC.BLL
{
    public interface ImADataGenerator
    {
        Task<int[]> GenerateIntegers();
    }
}
