using System.Collections.ObjectModel;
using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using StS1DeprecatedThings.StS1DeprecatedThingsCode.Powers;

namespace StS1DeprecatedThings.StS1DeprecatedThingsCode.Cards;

[Pool(typeof(ColorlessCardPool))]
public class MasterReality() : CustomCardModel(1, CardType.Power, CardRarity.Rare, TargetType.Self)
{
    // public override string? CustomPortraitPath { get; } = "StS1DeprecatedThings/assets/portraits/masterReality.png";

    protected override IEnumerable<DynamicVar> CanonicalVars
    {
        get
        {
            return (IEnumerable<DynamicVar>) new ReadOnlyCollection<DynamicVar>
            (new DynamicVar[1]
            {
                (DynamicVar) new PowerVar<MasterRealityPower>(1M)
            });
        }
    }

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        MasterReality cardSource = this;
        await CreatureCmd.TriggerAnim(cardSource.Owner.Creature, "Cast", cardSource.Owner.Character.CastAnimDelay);
        MasterRealityPower masterRealityPower = await PowerCmd.Apply<MasterRealityPower>(
            cardSource.Owner.Creature, 
            cardSource.DynamicVars["MasterRealityPower"].BaseValue, 
            cardSource.Owner.Creature, 
            (CardModel) cardSource);
    }

    protected override void OnUpgrade() => this.EnergyCost.UpgradeBy(-1);
}