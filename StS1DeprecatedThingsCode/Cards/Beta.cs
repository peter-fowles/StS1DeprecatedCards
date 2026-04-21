using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace StS1DeprecatedThings.StS1DeprecatedThingsCode.Cards;

[Pool(typeof(TokenCardPool))]
public class Beta() : CustomCardModel(2, CardType.Skill,
    CardRarity.Token, TargetType.Self)
{
    public override string? CustomPortraitPath => "StS1DeprecatedThings/assets/CardPortraits/beta.png";
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Exhaust
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromCard<Omega>(),
        HoverTipFactory.FromKeyword(CardKeyword.Exhaust)
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        Beta beta = this;
        await CreatureCmd.TriggerAnim(beta.Owner.Creature, "Cast", beta.Owner.Character.CastAnimDelay);
        CardCmd.PreviewCardPileAdd(
            await CardPileCmd.AddGeneratedCardToCombat(
                Omega.Create(beta.Owner, beta.CombatState), 
                PileType.Draw, true, CardPilePosition.Random));
    }

    public static Beta Create(Player owner, CombatState combatState)
    {
        return combatState.CreateCard<Beta>(owner);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}