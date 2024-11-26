using gaw241124.Model;
using gaw241124.Presenter;
using gaw241124.View;
using VContainer;
using VContainer.Unity;
using Tarahiro;

namespace gaw241124.Inject
{
    public class EnemyGroupLifetimeScope : LifetimeScope
    {
        IEnemyStoneContainer _enemyStoneContainer;

        protected override void Configure(IContainerBuilder builder)
        {
            Log.DebugLog("Configure");

            builder.Register<EnemyStoneContainer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EnemyStoneView>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EnemyBrain>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EnemyStatus>(Lifetime.Singleton).AsSelf();
            builder.Register<EnemyStonePutter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EnemyStonePutTryer>(Lifetime.Singleton).AsImplementedInterfaces();


            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<EnemyGroupPresenter>();

            });
        }

        public IEnemyStoneContainer GetEnemyStoneContainer()
        {
            if (_enemyStoneContainer == null)
            {
                _enemyStoneContainer = this.Container.Resolve<IEnemyStoneContainer>();
            }
            return _enemyStoneContainer;
        }
    }
}
