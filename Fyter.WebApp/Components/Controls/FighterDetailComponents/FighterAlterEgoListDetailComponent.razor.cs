using Microsoft.AspNetCore.Components;
using Fyter.CoreBusiness.FighterModel;
using Fyter.UseCases.Fighters.Interfaces;

namespace Fyter.WebApp.Components.Controls.FighterDetailComponents
{
    public partial class FighterAlterEgoListDetailComponent : ComponentBase
    {
        [Inject]
        public required IViewFighterByIdUseCase ViewFighterByIdUseCase { get; set; }

        [Inject]
        public required NavigationManager NavigationManager { get; set; }

        [Parameter]
        public Fighter? Fighter { get; set; }

        protected override async Task OnParametersSetAsync()
        {
            if (Fighter != null)
            {
                if (
                    Fighter.IsAlterEgo
                    && Fighter.BaseFighter == null
                    && Fighter.BaseFighterId.HasValue
                )
                {
                    // Load the Base Fighter
                    Fighter.BaseFighter = await ViewFighterByIdUseCase.ExecuteAsync(
                        Fighter.BaseFighterId.Value
                    );
                }

                if (!Fighter.IsAlterEgo && (Fighter.AlterEgos == null || !Fighter.AlterEgos.Any()))
                {
                    // Reload Fighter to ensure AlterEgos are loaded
                    var updatedFighter = await ViewFighterByIdUseCase.ExecuteAsync(
                        Fighter.FighterId
                    );
                    Fighter.AlterEgos = updatedFighter.AlterEgos;
                }
            }
        }

        private void GoToBaseFighter(int baseFighterId)
        {
            NavigationManager.NavigateTo($"/fighter/{baseFighterId}");
        }

        private void GoToAlterEgo(int alterEgoId)
        {
            NavigationManager.NavigateTo($"/fighter/{alterEgoId}");
        }
    }
}
