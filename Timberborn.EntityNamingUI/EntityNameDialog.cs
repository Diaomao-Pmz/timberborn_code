using System;
using Timberborn.CoreUI;
using Timberborn.EntityNaming;

namespace Timberborn.EntityNamingUI
{
	// Token: 0x02000005 RID: 5
	public class EntityNameDialog
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002187 File Offset: 0x00000387
		public EntityNameDialog(InputBoxShower inputBoxShower)
		{
			this._inputBoxShower = inputBoxShower;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002198 File Offset: 0x00000398
		public void Show(NamedEntity namedEntity)
		{
			this._inputBoxShower.Create().SetLocalizedMessage(EntityNameDialog.ChangeNameLocKey).SetDefaultValue(namedEntity.EntityName).SetConfirmButton(delegate(string value)
			{
				EntityNameDialog.SetEntityName(value, namedEntity);
			}).Show();
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021ED File Offset: 0x000003ED
		public static void SetEntityName(string newName, NamedEntity namedEntity)
		{
			if (namedEntity && !string.IsNullOrWhiteSpace(newName))
			{
				namedEntity.SetEntityName(newName.Trim());
			}
		}

		// Token: 0x0400000B RID: 11
		public static readonly string ChangeNameLocKey = "EntityPanel.ChangeName";

		// Token: 0x0400000C RID: 12
		public readonly InputBoxShower _inputBoxShower;
	}
}
