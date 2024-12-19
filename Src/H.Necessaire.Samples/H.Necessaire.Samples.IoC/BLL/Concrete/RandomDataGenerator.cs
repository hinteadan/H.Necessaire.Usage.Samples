using System;
using System.Linq;
using System.Threading.Tasks;

namespace H.Necessaire.Samples.IoC.BLL.Concrete
{
    [ID("random")]
    [Alias("rnd")]
    internal class RandomDataGenerator : ImADataGenerator
    {
        public Task<int[]> GenerateIntegers()
        {
            return
                Enumerable
                .Range(0, Random.Shared.Next(1, 10))
                .Select(i => Random.Shared.Next())
                .ToArray()
                .AsTask()
                ;
        }
    }
}
