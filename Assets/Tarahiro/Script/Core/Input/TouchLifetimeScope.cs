using VContainer;
using VContainer.Unity;
using UnityEngine;

namespace Tarahiro.TInput
{
    public class TouchLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {

            });
        }
    }
}
