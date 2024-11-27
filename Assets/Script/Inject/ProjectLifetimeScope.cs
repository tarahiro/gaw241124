using gaw241124;
using gaw241124.Model;
using gaw241124.View;
using gaw241124.Presenter;
using Tarahiro.TGrid;
using VContainer;
using VContainer.Unity;
using UnityEngine;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;

namespace gaw241124.Inject
{
    public class ProjectLifetimeScope : LifetimeScope
    {
        [SerializeField] StoneProvider _stoneProvider;
        protected override void Configure(IContainerBuilder builder)
        {
            Log.DebugLogComment("LifetimeScope‚ÅRegisterŠJŽn");

            //Stone
            builder.RegisterInstance<StoneProvider>(_stoneProvider).AsSelf();
            builder.RegisterComponentInHierarchy<StonePutView>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<StoneNumberUiView>().AsImplementedInterfaces();
            builder.Register<StonePutterModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerStoneInitializer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerStonePutTryer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerHoldStoneModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerStonePutter>(Lifetime.Singleton).AsImplementedInterfaces();

            //Treasure
            builder.RegisterComponentInHierarchy<TreasureView>().AsImplementedInterfaces();
            builder.Register<TreasureModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterFactory<ITreasureItemView, TreasureModelItemArgs>(m => new TreasureModelItemArgs(m.Index,m.GridPosition,m.Id,m.Arg));
            builder.RegisterFactory<Const.Side, Vector2Int, StonePositionArgs>((s,v) => new StonePositionArgs(s,v));

            //Enemy
            builder.RegisterFactory<Vector2Int, List<Vector2Int>, IEnemyGroupStoneChain>(container =>
                {
                    var _gridProvider = container.Resolve<IGridProvider>();
                    var _stoneProvider = container.Resolve<StoneProvider>();
                    return (x, y) =>
                    {
                        return new EnemyGroupStoneChain(x, y, _gridProvider, _stoneProvider);
                    };
                }, Lifetime.Singleton);
            builder.RegisterComponentInHierarchy<EyesightView>().AsImplementedInterfaces();
            builder.Register<EnemyWholeGroupHundler>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.RegisterFactory<string, IEnemyGroupInstanceProvider>(container =>
            {
                return s =>
                {
                    var prefab = Resources.Load<EnemyGroupLifetimeScope>("Prefab/EnemyGroupLifetimeScope");
                    EnemyGroupLifetimeScope instance = container.Instantiate(prefab);
                    instance.Construct(s);
                    return instance;
                };
            }, Lifetime.Scoped);
            builder.RegisterFactory<IEnemyInitialStoneItemView, EnemyInitialStoneArgs>(x => new EnemyInitialStoneArgs(x.Id, x.GridPosition, x.EyesightDirection));
            builder.RegisterComponentInHierarchy<EnemyInitialStoneView>().AsImplementedInterfaces();
            builder.Register<EnemyBrain>(Lifetime.Singleton).AsImplementedInterfaces();


            //Hide
            builder.RegisterComponentInHierarchy<HideView>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<HideModel>().AsImplementedInterfaces();

            //Turn
            builder.Register<TurnModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<EnemyTurn>(Lifetime.Singleton).As<IEnemyTurn>();
            builder.Register<PlayerTurn>(Lifetime.Singleton).As<IPlayerTurn>();
            builder.Register<Turn>(Lifetime.Singleton).AsImplementedInterfaces();

            //GameClear
            builder.Register<GameClearModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<GameClearView>().AsImplementedInterfaces();

            //GameOver
            builder.Register<GameOverModel>(Lifetime.Singleton).AsImplementedInterfaces();

            //Title
            builder.Register<TitleModel>(Lifetime.Singleton).AsImplementedInterfaces();

            //Manager
            builder.Register<AdapterManagerToModel>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<PlayerInputView>();
                entryPoints.Add<StonePresenter>();
                entryPoints.Add<TreasurePresenter>();
                entryPoints.Add<HidePresenter>();
                entryPoints.Add<EnemyPresenter>();
                entryPoints.Add<PlayerPresenter>();
                entryPoints.Add<TurnPresenter>();
                entryPoints.Add<GameEndPresenter >();
                entryPoints.Add<GameClearPresenter >();

                entryPoints.Add<GameManager>();

#if ENABLE_DEBUG
                entryPoints.Add<DebugManager>();
#endif

            });
        }
    }
}
