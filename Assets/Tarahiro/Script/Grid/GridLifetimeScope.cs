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
            builder.RegisterInstance<SpriteInformationContainer>(_spriteInformationContainer).AsSelf();
            builder.RegisterComponentInHierarchy<GridMonoBehaviourReader>().AsImplementedInterfaces();

            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                //OtherGame
                entryPoints.Add<GridModel>();

            });
        }
    }
}
