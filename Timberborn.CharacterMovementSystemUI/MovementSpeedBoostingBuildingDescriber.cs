using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterMovementSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.Localization;

namespace Timberborn.CharacterMovementSystemUI
{
	// Token: 0x02000005 RID: 5
	public class MovementSpeedBoostingBuildingDescriber : BaseComponent, IAwakableComponent, IEntityDescriber
	{
		// Token: 0x06000006 RID: 6 RVA: 0x00002103 File Offset: 0x00000303
		public MovementSpeedBoostingBuildingDescriber(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002112 File Offset: 0x00000312
		public void Awake()
		{
			this._movementSpeedBoostingBuildingSpec = base.GetComponent<MovementSpeedBoostingBuildingSpec>();
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002120 File Offset: 0x00000320
		public IEnumerable<EntityDescription> DescribeEntity()
		{
			string str = string.Format("{0}: +{1}%", this._loc.T(MovementSpeedBoostingBuildingDescriber.BonusLocKey), this._movementSpeedBoostingBuildingSpec.BoostPercentage);
			yield return EntityDescription.CreateTextSection(SpecialStrings.RowStarter + str, 20);
			yield break;
		}

		// Token: 0x04000006 RID: 6
		public static readonly string BonusLocKey = "Bonus.MovementSpeed";

		// Token: 0x04000007 RID: 7
		public readonly ILoc _loc;

		// Token: 0x04000008 RID: 8
		public MovementSpeedBoostingBuildingSpec _movementSpeedBoostingBuildingSpec;
	}
}
