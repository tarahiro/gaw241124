using gaw241124.Model;
using gaw241124.View;
using gaw241124.Presenter;
using Tarahiro.TGrid;
using VContainer;
using VContainer.Unity;

namespace gaw241124.Inject
{
    public class ProjectLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            //Stone
            builder.RegisterComponentInHierarchy<StoneView>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<StoneNumberUiView>().AsImplementedInterfaces();
            builder.Register<StoneModel>(Lifetime.Singleton).AsImplementedInterfaces();

            //Treasure
            builder.RegisterComponentInHierarchy<TreasureView>().AsImplementedInterfaces();
            builder.Register<TreasureModel>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterFactory<ITreasureItemView, TreasureModelItemArgs>(m => new TreasureModelItemArgs(m.Index,m.GridPosition,m.Id));

            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<StonePresenter>();
                entryPoints.Add<TreasurePresenter>();

            });
        }
    }
}
