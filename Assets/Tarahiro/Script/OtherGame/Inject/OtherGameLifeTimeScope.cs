using Cysharp.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using Tarahiro;
using Tarahiro.TInput;
using Tarahiro.Ui;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Tarahiro.OtherGame.MasterData;

namespace Tarahiro.OtherGame.Inject
{
    public class OtherGameLifeTimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            Log.DebugLogComment("OtherGameLifetimeScope‚ÅRegisterŠJŽn");
            //OtherGame
            builder.RegisterFactory<Sprite, IOtherGameIcon>(container =>
            {

                return sprite =>
                {
                    var prefab = Resources.Load<OtherGameIcon>("Prefab/OtherGameIcon");
                    OtherGameIcon instance = container.Instantiate(prefab);
                    instance.Construct(sprite);
                    return instance;
                };
            }, Lifetime.Scoped);
            builder.Register<OtherGameModel>(Lifetime.Singleton).WithParameter<string>("FakeProjectCode").AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<OtherGameAbstructView>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<OtherGameMenuView>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<OtherGameDetailView>().AsImplementedInterfaces();
            builder.Register<OtherGameMasterDataProvider>(Lifetime.Singleton).AsImplementedInterfaces();

            builder.RegisterFactory<IOtherGameMenuItemViewArgs, IOtherGameMenuItemView>(container =>
            {
                return args =>
                {
                    var prefab = Resources.Load<OtherGameMenuItemView>("Prefab/OtherGameMenuItemView");
                    OtherGameMenuItemView instance = container.Instantiate(prefab);
                    instance.Construct(args);
                    return instance;
                };
            }, Lifetime.Scoped);

            builder.RegisterFactory<IOtherGameMaster, IOtherGameMenuItemViewArgs>(m => new OtherGameMenuItemViewArgs(m.Id, m.IconPathJp));

            builder.RegisterFactory<IOtherGameMaster, IOtherGameDetailViewArgs>(x => new OtherGameDetailViewArgs(x.Id, x.TitleNameJp, x.GenreNameJp, x.DescriptionJp, x.ScreenShotCenterPathJp, x.ScreenShotRightTopPathJp, x.ScreenShotRightBottomPathJp));


            builder.UseEntryPoints(Lifetime.Singleton, entryPoints =>
            {
                //OtherGame
                entryPoints.Add<OtherGamePresenter>();

            });


        }
    }
}