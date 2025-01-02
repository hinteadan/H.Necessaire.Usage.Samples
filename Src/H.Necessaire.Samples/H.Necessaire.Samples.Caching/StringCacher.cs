using System;
using System.Threading.Tasks;

namespace H.Necessaire.Samples.Caching
{
    internal class StringCacher : ImADependency, ImACacher<string>
    {
        #region Construct
        ImALogger logger;
        ImACacher<string> baseCacher;
        public void ReferDependencies(ImADependencyProvider dependencyProvider)
        {
            this.logger = dependencyProvider.GetLogger<StringCacher>();
            this.baseCacher = dependencyProvider.GetCacher<string>(cacherID: "InMemory");
        }
        #endregion

        public async Task<string> AddOrUpdate(string id, Func<string, Task<ImCachebale<string>>> cacheableItemFactory)
        {
            using (new TimeMeasurement(x => Log(nameof(AddOrUpdate), x)))
            {
                return await baseCacher.AddOrUpdate(id, cacheableItemFactory);
            }
        }

        public async Task Clear(params string[] ids)
        {
            using (new TimeMeasurement(x => Log(nameof(Clear), x)))
            {
                await baseCacher.Clear(ids);
            }
        }

        public async Task ClearAll()
        {
            using (new TimeMeasurement(x => Log(nameof(ClearAll), x)))
            {
                await baseCacher.ClearAll();
            }
        }

        public async Task<string> GetOrAdd(string id, Func<string, Task<ImCachebale<string>>> cacheableItemFactory)
        {
            using (new TimeMeasurement(x => Log(nameof(GetOrAdd), x)))
            {
                return await baseCacher.GetOrAdd(id, cacheableItemFactory);
            }
        }

        public async Task RunHousekeepingSession()
        {
            using (new TimeMeasurement(x => Log(nameof(RunHousekeepingSession), x)))
            {
                await baseCacher.RunHousekeepingSession();
            }
        }

        public async Task<OperationResult<string>> TryGet(string id)
        {
            using (new TimeMeasurement(x => Log(nameof(TryGet), x)))
            {
                return await baseCacher.TryGet(id);
            }
        }

        void Log(string actionName, TimeSpan duration)
        {
            logger
                .LogInfo($"{nameof(StringCacher)} operation [{actionName}] took {duration}")
                .ConfigureAwait(continueOnCapturedContext: false)
                .GetAwaiter()
                .GetResult()
                ;
        }
    }
}
