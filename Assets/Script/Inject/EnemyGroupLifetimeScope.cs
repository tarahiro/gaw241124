using gaw241124.Model;
using gaw241124.Presenter;
using gaw241124.View;
using VContainer;
using VContainer.Unity;
using Tarahiro;
using System;
using UniRx;

namespace gaw241124.Inject
{
    public class EnemyGroupLifetimeScope : LifetimeScope, IEnemyGroupInstanceProvider
    {
        IEnemyGroupStonePutter _enemyStoneContainer;
        CompositeDisposable disposables = new CompositeDisposable();

        string _groupId;

        Subject<string> _disposed = new Subject<string>();
        public IObservable<string> Disposed => _disposed;
        public string GroupId => _groupId;

        public void Construct(string groupId)
        {
            _groupId = groupId;
        }

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<EnemyGroupStoneContainer>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<EnemyStoneView>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<EnemyGroupBrain>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<EnemyGroupStatus>(Lifetime.Scoped).AsSelf();
            builder.Register<EnemyGroupStonePutter>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.Register<EnemyGroupStonePutTryer>(Lifetime.Scoped).AsImplementedInterfaces();
            builder.RegisterInstance<CompositeDisposable>(disposables);


            builder.UseEntryPoints(Lifetime.Scoped, entryPoints =>
            {
                entryPoints.Add<EnemyGroupPresenter>();

            });
        }

        public T GetInstance<T>()
        {
            return this.Container.Resolve<T>();
        }

        public void DisposeEnemyGroup()
        {
            _disposed.OnNext(GroupId);
            disposables.Dispose();
            Dispose();
        }



    }
}
