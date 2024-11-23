using VContainer;
using VContainer.Unity;

namespace Tarahiro.TInput
{
    public class InputLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {

            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                //Touch
                entryPoints.Add<TTouch>();
                entryPoints.Add<TFlick>();
                entryPoints.Add<TCanvas>();

#if ENABLE_VIRTUAL_CURSOR
                entryPoints.Add<TVIrtualCursor>();
#endif
            });
        }
    }
}
