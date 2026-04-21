using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace StS1DeprecatedThings.StS1DeprecatedThingsCode.Cards;

[Pool(typeof(IroncladCardPool))]
public class Exhume() : CustomCardModel(1, CardType.Skill,
    CardRarity.Rare, TargetType.Self)
{
    public override string? CustomPortraitPath => "StS1DeprecatedThings/assets/CardPortraits/exhume.png";
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [
        CardKeyword.Exhaust
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromKeyword(CardKeyword.Exhaust),
    ];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        Exhume exhume = this;
        CardSelectorPrefs prefs = new CardSelectorPrefs(exhume.SelectionScreenPrompt, 1);
        CardModel card = (
            await CardSelectCmd.FromSimpleGrid(choiceContext, 
                (IReadOnlyList<CardModel>) PileType.Exhaust.GetPile(exhume.Owner).Cards.Where((CardModel c) => c is not Exhume).ToList(), 
                exhume.Owner, prefs)).FirstOrDefault<CardModel>();
        if (card == null)
            return;
        CardPileAddResult cardPileAddResult = await CardPileCmd.Add(card, PileType.Hand);
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}