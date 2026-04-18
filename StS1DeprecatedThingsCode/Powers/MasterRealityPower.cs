using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.CommonUi;

namespace StS1DeprecatedThings.StS1DeprecatedThingsCode.Powers;
public sealed class MasterRealityPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.None;

    public override string? CustomPackedIconPath => "StS1DeprecatedThings/assets/PowerIcons/Icon_MasterReality.png";

    public override async Task AfterCardGeneratedForCombat(CardModel card, bool addedByPlayer)
    {
        MasterRealityPower masterRealityPower = this;
        if (card.Owner != masterRealityPower.Owner.Player || !addedByPlayer)
            return;
        if (card.IsUpgradable)
        {
            masterRealityPower.Flash();
            CardCmd.Upgrade(card, CardPreviewStyle.None);
        }
    }
}