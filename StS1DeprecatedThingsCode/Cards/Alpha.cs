using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace StS1DeprecatedThings.StS1DeprecatedThingsCode.Cards;
[Pool(typeof(ColorlessCardPool))]
public class Alpha() : CustomCardModel(1, CardType.Skill,
    CardRarity.Rare, TargetType.Self)
{
    public override string? CustomPortraitPath => "StS1DeprecatedThings/assets/CardPortraits/alpha.png";
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    public override IEnumerable<CardKeyword> CanonicalKeywords =>
    [
        CardKeyword.Exhaust
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        Alpha alpha = this;
        await CreatureCmd.TriggerAnim(alpha.Owner.Creature, "Cast", alpha.Owner.Character.CastAnimDelay);
        CardCmd.PreviewCardPileAdd(
            await CardPileCmd.AddGeneratedCardToCombat(
                Beta.Create(alpha.Owner, alpha.CombatState), 
                PileType.Draw, true, CardPilePosition.Random));
    }
    
    protected override void OnUpgrade()
    {
        AddKeyword(CardKeyword.Innate);
    }
}