using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.ValueProps;

namespace StS1DeprecatedThings.StS1DeprecatedThingsCode.Powers;

public class OmegaPower : CustomPowerModel
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Counter;

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [];

    public override string? CustomPackedIconPath => "StS1DeprecatedThings/assets/PowerIcons/Icon_Omega.png";
    public override string? CustomBigIconPath => "StS1DeprecatedThings/assets/PowerIcons/Icon_Omega.png";

    public override async Task BeforeTurnEnd(PlayerChoiceContext choiceContext, CombatSide side)
    {
        OmegaPower omegaPower = this;
        if (side != CombatSide.Player)
        {
            return;
        }
        omegaPower.Flash();
        IEnumerable<DamageResult> damageResults = await CreatureCmd.Damage(
            choiceContext,
            omegaPower.CombatState.HittableEnemies,
            omegaPower.Amount,
            ValueProp.Unpowered,
            omegaPower.Owner
        );
    }
}