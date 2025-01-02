using System;
using System.Threading.Tasks;

namespace H.Necessaire.Samples.Caching
{
    internal class CustomCacher<T> : ImADependency, ImACacher<T>
    {
        #region Construct
        ImALogger logger;
        ImACacher<T> baseCacher;
        public void ReferDependencies(ImADependencyProvider dependencyProvider)
        {
            this.logger = dependencyProvider.GetLogger<CustomCacher<T>>();
            this.baseCacher = dependencyProvider.GetCacher<T>(cacherID: "InMemory");
        }
        #endregion

        public async Task<T> AddOrUpdate(string id, Func<string, Task<ImCachebale<T>>> cacheableItemFactory)
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

        public async Task<T> GetOrAdd(string id, Func<string, Task<ImCachebale<T>>> cacheableItemFactory)
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

        public async Task<OperationResult<T>> TryGet(string id)
        {
            using (new TimeMeasurement(x => Log(nameof(TryGet), x)))
            {
                return await baseCacher.TryGet(id);
            }
        }

        void Log(string actionName, TimeSpan duration)
        {
            logger
                .LogInfo($"{nameof(CustomCacher<T>)} operation [{actionName}] took {duration}")
                .ConfigureAwait(continueOnCapturedContext: false)
                .GetAwaiter()
                .GetResult()
                ;
        }
    }
}
