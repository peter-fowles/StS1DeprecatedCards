using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using StS1DeprecatedThings.StS1DeprecatedThingsCode.Powers;

namespace StS1DeprecatedThings.StS1DeprecatedThingsCode.Cards;

[Pool(typeof(TokenCardPool))]
public class Omega() : CustomCardModel(3, CardType.Power,
    CardRarity.Token, TargetType.Self)
{
    public override string? CustomPortraitPath => "StS1DeprecatedThings/assets/CardPortraits/omega.png";
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<OmegaPower>(50M)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        Omega cardSource = this;
        await CreatureCmd.TriggerAnim(cardSource.Owner.Creature, "Cast", cardSource.Owner.Character.CastAnimDelay);
        await PowerCmd.Apply<OmegaPower>(
            cardSource.Owner.Creature,
            cardSource.DynamicVars["OmegaPower"].BaseValue,
            cardSource.Owner.Creature,
            cardSource
            );
    }

    public static Omega Create(Player owner, CombatState combatState)
    {
        return combatState.CreateCard<Omega>(owner);
    }

    protected override void OnUpgrade()
    {
        DynamicVars["OmegaPower"].UpgradeValueBy(10M);
    }
}