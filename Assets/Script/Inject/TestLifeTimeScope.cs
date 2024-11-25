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
    public class TestLifeTimeScope : LifetimeScope
    {
        [SerializeField] StoneProvider _stoneProvider;
        protected override void Configure(IContainerBuilder builder)
        {
        }
    }
}
