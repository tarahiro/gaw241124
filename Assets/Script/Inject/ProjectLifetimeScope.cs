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
            builder.RegisterComponentInHierarchy<StoneView>().AsImplementedInterfaces();
            builder.Register<StoneModel>(Lifetime.Singleton).AsImplementedInterfaces();


            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<StonePresenter>();

            });
        }
    }
}
