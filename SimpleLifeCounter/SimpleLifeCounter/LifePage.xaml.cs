﻿using Newtonsoft.Json;
using SimpleLifeCounter.ViewModels;
using SimpleLifeCounter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SimpleLifeCounter
{
    public partial class LifePage : ContentPage
    {
        private LifePageViewModel vm { get; } = new LifePageViewModel();

        public LifePage()
        {
            InitializeComponent();
            

            BindingContext = vm;

            // 上の邪魔なの消すおまじない
            NavigationPage.SetHasNavigationBar(this, false);

            // LifeSet
            LeftPlyerLife.Text = vm.DefaultLifePoint.ToString();
            RightPlyerLife.Text = vm.DefaultLifePoint.ToString();

            LeftPlyerLifeUp.Clicked += (sender, e) => LeftPlyerLife.Text = LifeUp(LeftPlyerLife);
            LeftPlyerLifeDown.Clicked += (sender, e) => LeftPlyerLife.Text = LifeDown(LeftPlyerLife);
            RightPlyerLifeUp.Clicked += (sender, e) => RightPlyerLife.Text = LifeUp(RightPlyerLife);
            RightPlyerLifeDown.Clicked += (sender, e) => RightPlyerLife.Text = LifeDown(RightPlyerLife);

            toMenuPageButton.Clicked += async(sender,e) => await Navigation.PushAsync(new MenuPage());
            LifeResetButton.Clicked += async (sender, e) =>
            {
                if (vm.LifeResetCheck ? (await DisplayAlert("リセット", "ライフを初期値に戻しますか？", "はい", "いいえ")) : true)
                {
                    LeftPlyerLife.Text = vm.DefaultLifePoint.ToString();
                    RightPlyerLife.Text = vm.DefaultLifePoint.ToString();
                }
            };

            

            DiceThrow.Clicked += async (sender, e) =>
            {
                vm.DebugMethod(); // 解せない-0------------------------------------

                vm.DiceMessegeGenerate();
                await DisplayAlert("ダイス", $"{vm.Message}", "OK");
            };

            CoinToss.Clicked += async (sender, e) =>
            {
                vm.DebugMethod(); // 解せない-0------------------------------------

                vm.CoinMessegeGenerate();
                await DisplayAlert("コイン", $"{vm.Message}", "OK");
            };
        }

        // CIP
        protected override void OnAppearing()
        {
            base.OnAppearing();

            vm.Load();
            BindingContext = vm;

            // もしデフォルトライフとVMの値が違うならデフォルトライフに代入して適用とかにする
            LeftPlyerLife.Text = vm.DefaultLifePoint.ToString();
            RightPlyerLife.Text = vm.DefaultLifePoint.ToString();
        }

        // とりあえず動かすためにここに置く
        private string LifeUp(Label l)
        {
            return (int.Parse(l.Text) + 1).ToString();
        }
        private string LifeDown(Label l)
        {
            return (int.Parse(l.Text) - 1).ToString();
        }

    }
}
