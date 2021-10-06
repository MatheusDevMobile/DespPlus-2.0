using DespPlus.Models;
using DespPlus.Services.Interface;
using DespPlus.ViewModels.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DespPlus.ViewModels
{
    public class OnboardingViewModel : IViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private int _position;
        public string Title { get; }
        protected INavigatorService NavigatorService { get; }
        public OnboardingViewModel(INavigatorService navigatorService)
        {
            NavigatorService = navigatorService;

            SetNextButtonText("PRÓXIMO");
            SetSkipButtonText("PULAR");
            OnBoarding();
            LaunchNextCommand();
            LaunchSkipCommand();
        }

        public async Task ReceiveNavigationParameters(IReadOnlyDictionary<string, object> parameters)
        {
        }
        public ICommand NextCommand { get; private set; }
        public ICommand SkipCommand { get; private set; }
        private void SetNextButtonText(string nextButtonText) => NextButtonText = nextButtonText;
        private void SetSkipButtonText(string skipButtonText) => SkipButtonText = skipButtonText;

        private void OnBoarding()
        {
            Items = new ObservableCollection<Onboarding>
            {
                new Onboarding
                {
                    Title = "Olá!",
                    Content = "Bem vindo ao DespPlus!",
                    Animation = "hello.json"
                },
                new Onboarding
                {
                    Title = "Sob seu controle!",
                    Content = "Tenha todos os seus gastos organizados.",
                    Animation = "onb1.json"
                },
                new Onboarding
                {
                    Title = "Totalmente Free!",
                    Content = "O DespPlus é livre de anúncios e \ntodas as funções são e sempre serão \ngratuitas!",
                    Animation = "veryHappy.json"
                }
            };
        }

        private void LaunchNextCommand()
        {

            NextCommand = new Command(() =>
            {
                if (LastPositionReached())
                {
                    ExitOnBoarding();
                }
                else
                {
                    MoveToNextPosition();
                }
            });
        }
        private void LaunchSkipCommand()
        {
            SkipCommand = new Command(() =>
            {
                ExitOnBoarding();
            });
        }

        private void ExitOnBoarding()
        {
            NavigatorService.BackToAsync();
        }

        private void MoveToNextPosition()
        {
            var nextPosition = ++Position;
            Position = nextPosition;
        }

        private bool LastPositionReached() => Position == Items.Count - 1;

        public ObservableCollection<Onboarding> Items { get; set; }

        public string NextButtonText { get; set; }
        public string SkipButtonText { get; set; }

        public int Position
        {
            get => _position;
            set
            {
                if (value != _position)
                {
                    _position = value;
                    UpdateNextButtonText();
                }
            }
        }

        private void UpdateNextButtonText()
        {
            if (LastPositionReached())
            {
                SetNextButtonText("ENTENDI");
            }
            else
            {
                SetNextButtonText("PRÓXIMO");
            }
        }
    }
}
