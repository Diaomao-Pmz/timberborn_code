using System;
using Timberborn.BaseComponentSystem;
using Timberborn.EntitySystem;
using Timberborn.StatusSystem;
using UnityEngine;

namespace Timberborn.CharacterModelSystem
{
	// Token: 0x0200000D RID: 13
	public class CharacterStatusIconCyclerPositioner : BaseComponent, IAwakableComponent, ILateUpdatableComponent, IInitializableEntity
	{
		// Token: 0x0600004B RID: 75 RVA: 0x00002936 File Offset: 0x00000B36
		public void Awake()
		{
			this._characterModel = base.GetComponent<CharacterModel>();
			base.DisableComponent();
		}

		// Token: 0x0600004C RID: 76 RVA: 0x0000294C File Offset: 0x00000B4C
		public void InitializeEntity()
		{
			StatusIconCycler component = base.GetComponent<StatusIconCycler>();
			this._statusIconCyclerTransform = component.Root.transform;
			this._iconOffset = this._statusIconCyclerTransform.localPosition;
			base.EnableComponent();
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002988 File Offset: 0x00000B88
		public void LateUpdate()
		{
			this._statusIconCyclerTransform.position = this._characterModel.Position + this._iconOffset;
		}

		// Token: 0x04000022 RID: 34
		public CharacterModel _characterModel;

		// Token: 0x04000023 RID: 35
		public Transform _statusIconCyclerTransform;

		// Token: 0x04000024 RID: 36
		public Vector3 _iconOffset;
	}
}
