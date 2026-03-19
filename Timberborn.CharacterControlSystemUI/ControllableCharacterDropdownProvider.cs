using System;
using System.Collections.Generic;
using Timberborn.BaseComponentSystem;
using Timberborn.CharacterControlSystem;
using Timberborn.DropdownSystem;
using Timberborn.TemplateSystem;

namespace Timberborn.CharacterControlSystemUI
{
	// Token: 0x0200000B RID: 11
	public class ControllableCharacterDropdownProvider : BaseComponent, IAwakableComponent, IDropdownProvider
	{
		// Token: 0x06000021 RID: 33 RVA: 0x000027F1 File Offset: 0x000009F1
		public ControllableCharacterDropdownProvider(ControllableCharacterAnimations controllableCharacterAnimations)
		{
			this._controllableCharacterAnimations = controllableCharacterAnimations;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002800 File Offset: 0x00000A00
		public IReadOnlyList<string> Items
		{
			get
			{
				return this._controllableCharacterAnimations.GatAnimations(this._controllableCharacter, this._templateSpec.TemplateName);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x0000281E File Offset: 0x00000A1E
		public void Awake()
		{
			this._controllableCharacter = base.GetComponent<ControllableCharacter>();
			this._templateSpec = base.GetComponent<TemplateSpec>();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00002838 File Offset: 0x00000A38
		public string GetValue()
		{
			return this._controllableCharacter.WaitAnimation;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002845 File Offset: 0x00000A45
		public void SetInitialAnimation()
		{
			this.SetValue(this._controllableCharacter.UnderControl ? this._controllableCharacter.WaitAnimation : this.Items[0]);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002873 File Offset: 0x00000A73
		public void SetValue(string value)
		{
			this._controllableCharacter.ChangeAnimation(value);
		}

		// Token: 0x04000025 RID: 37
		public readonly ControllableCharacterAnimations _controllableCharacterAnimations;

		// Token: 0x04000026 RID: 38
		public ControllableCharacter _controllableCharacter;

		// Token: 0x04000027 RID: 39
		public TemplateSpec _templateSpec;
	}
}
