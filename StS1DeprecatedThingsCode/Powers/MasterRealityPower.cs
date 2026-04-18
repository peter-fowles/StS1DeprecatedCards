// Decompiled with JetBrains decompiler
// Type: MegaCrit.Sts2.Core.Models.Powers.PillarOfCreationPower
// Assembly: sts2, Version=0.1.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 1B6C867B-1925-4D98-A300-0B940AE284E4
// Assembly location: /home/pfowles/.local/share/Steam/steamapps/common/Slay the Spire 2/data_sts2_linuxbsd_x86_64/sts2.dll

#nullable enable
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
        masterRealityPower.Flash();
        if (card.IsUpgradable)
        {
            CardCmd.Upgrade(card, CardPreviewStyle.None);
        }
    }
}