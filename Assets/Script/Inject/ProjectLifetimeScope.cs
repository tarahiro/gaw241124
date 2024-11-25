using gaw241124;
using gaw241124.Model;
using gaw241124.View;
using gaw241124.Presenter;
using Tarahiro.TGrid;
using VContainer;
using VContainer.Unity;
using UnityEngine;

namespace gaw241124.Inject
{
    public class ProjectLifetimeScope : LifetimeScope
    {
        [SerializeField] StoneProvider _stoneProvider;
        protected override void Configure(IContainerBuilder builder)
        {
            //Stone
            builder.RegisterInstance<StoneProvider>(_stoneProvider).AsSelf();
            builder.RegisterComponentInHierarchy<StonePutView>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<StoneNumberUiView>().AsImplementedInterfaces();
            builder.Register<StonePutterModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerStoneInitializer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerStonePutTryer>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerHoldStoneModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerStonePutterModel>(Lifetime.Singleton).AsImplementedInterfaces();

            //Treasure
            builder.RegisterComponentInHierarchy<TreasureView>().AsImplementedInterfaces();
            builder.Register<TreasureModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterFactory<ITreasureItemView, TreasureModelItemArgs>(m => new TreasureModelItemArgs(m.Index,m.GridPosition,m.Id,m.Arg));
            builder.RegisterFactory<Const.Side, Vector2Int, StonePositionArgs>((s,v) => new StonePositionArgs(s,v));


            //Hide
            builder.RegisterComponentInHierarchy<HideView>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<HideModel>().AsImplementedInterfaces();

            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<GridTouchView>();
                entryPoints.Add<StonePresenter>();
                entryPoints.Add<TreasurePresenter>();
                entryPoints.Add<HidePresenter>();

            });
        }
    }
}
