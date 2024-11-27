using VContainer;
using VContainer.Unity;
using UnityEngine;

namespace Tarahiro.TGrid
{
    public class GridLifetimeScope : LifetimeScope
    {
        [SerializeField] SpriteInformationContainer _spriteInformationContainer;

        protected override void Configure(IContainerBuilder builder)
        {
            Log.DebugLogComment("OtherGameLifetimeScope‚ÅRegisterŠJŽn");

            builder.RegisterInstance<SpriteInformationContainer>(_spriteInformationContainer).AsSelf();
            builder.RegisterComponentInHierarchy<GridMonoBehaviourReader>().AsImplementedInterfaces();


            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                entryPoints.Add<GridProvider>();

            });
        }
    }
}
