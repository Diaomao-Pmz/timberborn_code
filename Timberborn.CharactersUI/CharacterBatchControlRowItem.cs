using System;
using Timberborn.BatchControl;
using Timberborn.Characters;
using UnityEngine.UIElements;

namespace Timberborn.CharactersUI
{
	// Token: 0x02000004 RID: 4
	public class CharacterBatchControlRowItem : IBatchControlRowItem, IUpdatableBatchControlRowItem
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x000020BE File Offset: 0x000002BE
		public VisualElement Root { get; }

		// Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public CharacterBatchControlRowItem(VisualElement root, Label entityName, Character character)
		{
			this.Root = root;
			this._entityName = entityName;
			this._character = character;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E3 File Offset: 0x000002E3
		public void UpdateRowItem()
		{
			this._entityName.text = this._character.FirstName;
		}

		// Token: 0x04000007 RID: 7
		public readonly Label _entityName;

		// Token: 0x04000008 RID: 8
		public readonly Character _character;
	}
}
