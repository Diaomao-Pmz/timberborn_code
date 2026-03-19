using System;
using System.Collections.Generic;
using System.Text;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.EntitySystem;
using Timberborn.Localization;

namespace Timberborn.BlockSystemUI
{
	// Token: 0x0200000F RID: 15
	public class PlaceableBlockObjectDescriber : BaseComponent, IAwakableComponent, IEntityDescriber
	{
		// Token: 0x0600003C RID: 60 RVA: 0x000029B7 File Offset: 0x00000BB7
		public PlaceableBlockObjectDescriber(ILoc loc)
		{
			this._loc = loc;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000029C6 File Offset: 0x00000BC6
		public void Awake()
		{
			this._labeledEntitySpec = base.GetComponent<LabeledEntitySpec>();
			this._blockObject = base.GetComponent<BlockObject>();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000029E0 File Offset: 0x00000BE0
		public IEnumerable<EntityDescription> DescribeEntity()
		{
			string descriptionLocKey = this._labeledEntitySpec.DescriptionLocKey;
			if (!string.IsNullOrEmpty(descriptionLocKey))
			{
				yield return EntityDescription.CreateTextSection(this._loc.T(descriptionLocKey), -1);
			}
			EntityDescription entityDescription = this.DescribeBlockObject();
			if (entityDescription != null)
			{
				yield return entityDescription;
			}
			EntityDescription entityDescription2 = this.DescribeFlavor();
			if (entityDescription2 != null)
			{
				yield return entityDescription2;
			}
			yield break;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000029F0 File Offset: 0x00000BF0
		public EntityDescription DescribeBlockObject()
		{
			StringBuilder stringBuilder = new StringBuilder();
			if (this._blockObject.Solid)
			{
				stringBuilder.AppendLine(SpecialStrings.RowStarter + this._loc.T(PlaceableBlockObjectDescriber.SolidLocKey));
			}
			if (this._blockObject.GroundOnly)
			{
				stringBuilder.AppendLine(SpecialStrings.RowStarter + this._loc.T(PlaceableBlockObjectDescriber.GroundOnlyLocKey));
			}
			if (this._blockObject.AboveGround)
			{
				stringBuilder.AppendLine(SpecialStrings.RowStarter + this._loc.T(PlaceableBlockObjectDescriber.AboveGroundLocKey));
			}
			if (stringBuilder.Length <= 0)
			{
				return null;
			}
			return EntityDescription.CreateTextSection(stringBuilder.ToStringWithoutNewLineEnd(), 2000);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002AA8 File Offset: 0x00000CA8
		public EntityDescription DescribeFlavor()
		{
			if (this._blockObject.IsFinished)
			{
				string flavorDescriptionLocKey = this._labeledEntitySpec.FlavorDescriptionLocKey;
				if (!string.IsNullOrEmpty(flavorDescriptionLocKey))
				{
					return EntityDescription.CreateFlavorSection(this._loc.T(flavorDescriptionLocKey), 1);
				}
			}
			return null;
		}

		// Token: 0x0400001E RID: 30
		public static readonly string AboveGroundLocKey = "Buildings.AboveGround";

		// Token: 0x0400001F RID: 31
		public static readonly string GroundOnlyLocKey = "Buildings.GroundOnly";

		// Token: 0x04000020 RID: 32
		public static readonly string SolidLocKey = "Buildings.Solid";

		// Token: 0x04000021 RID: 33
		public readonly ILoc _loc;

		// Token: 0x04000022 RID: 34
		public LabeledEntitySpec _labeledEntitySpec;

		// Token: 0x04000023 RID: 35
		public BlockObject _blockObject;
	}
}
