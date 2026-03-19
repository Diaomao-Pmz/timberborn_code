using System;
using Timberborn.BaseComponentSystem;
using Timberborn.SelectionSystem;

namespace Timberborn.CharacterModelSystem
{
	// Token: 0x02000012 RID: 18
	public class VisibleSelectedCharacterModel : BaseComponent, IAwakableComponent, ISelectionListener
	{
		// Token: 0x06000077 RID: 119 RVA: 0x00002ED9 File Offset: 0x000010D9
		public void Awake()
		{
			this._characterModel = base.GetComponent<CharacterModel>();
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00002EE7 File Offset: 0x000010E7
		public void OnSelect()
		{
			this._characterModel.IgnoreModelBlockade();
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00002EF4 File Offset: 0x000010F4
		public void OnUnselect()
		{
			this._characterModel.UnignoreModelBlockade();
		}

		// Token: 0x04000032 RID: 50
		public CharacterModel _characterModel;
	}
}
