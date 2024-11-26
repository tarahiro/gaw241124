using gaw241124.Model;
using gaw241124.Presenter;
using gaw241124.View;
using VContainer;
using VContainer.Unity;
using Tarahiro;

namespace gaw241124.Inject
{
    public class EnemyGroupLifetimeScope : LifetimeScope, IEnemyGroupInstanceProvider
    {
        IEnemyGroupStonePutter _enemyStoneContainer;

        string _groupId;

        public string GroupId => _groupId;

        public void Construct(string groupId)
        {
            _groupId = groupId;
        }

        protected override void Configure(IContainerBuilder builder)
        {
            Log.DebugLog("Configure");

            builder.Register<EnemyGroupStoneContainer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EnemyStoneView>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EnemyGroupBrain>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EnemyGroupStatus>(Lifetime.Singleton).AsSelf();
            builder.Register<EnemyGroupStonePutter>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EnemyGroupStonePutTryer>(Lifetime.Singleton).AsImplementedInterfaces();


            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<EnemyGroupPresenter>();

            });
        }

        public T GetInstance<T>()
        {
            /*
            if (_enemyStoneContainer == null)
            {
                _enemyStoneContainer = this.Container.Resolve<IEnemyGroupStonePutter>();
            }
            */
            return this.Container.Resolve<T>();
        }


    }
}
