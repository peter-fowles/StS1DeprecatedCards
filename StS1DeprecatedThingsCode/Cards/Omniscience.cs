using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;

namespace StS1DeprecatedThings.StS1DeprecatedThingsCode.Cards;
[Pool(typeof(ColorlessCardPool))]
public class Omniscience() : CustomCardModel(4,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    public override string? CustomPortraitPath => "StS1DeprecatedThings/assets/CardPortraits/omniscience.png";
    
    protected override IEnumerable<DynamicVar> CanonicalVars => [];
    
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        Omniscience omniscience = this;
        CardSelectorPrefs prefs = new CardSelectorPrefs(omniscience.SelectionScreenPrompt, 1);
        CardModel selectedCard = (
            await CardSelectCmd.FromSimpleGrid(choiceContext, 
                PileType.Draw.GetPile(omniscience.Owner).Cards, 
                omniscience.Owner, prefs)).FirstOrDefault<CardModel>();
        if (selectedCard != null)
        {
            await CardCmd.AutoPlay(choiceContext, selectedCard, null);
            await CardCmd.AutoPlay(choiceContext, selectedCard, null);
            await CardCmd.Exhaust(choiceContext, selectedCard);
        }
    }

    protected override void OnUpgrade()
    {
        EnergyCost.UpgradeBy(-1);
    }
}