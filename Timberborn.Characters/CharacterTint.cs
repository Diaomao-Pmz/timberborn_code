using System;
using Timberborn.BaseComponentSystem;
using UnityEngine;

namespace Timberborn.Characters
{
	// Token: 0x0200000D RID: 13
	public class CharacterTint : BaseComponent, IAwakableComponent, IChildhoodInfluenced
	{
		// Token: 0x0600002A RID: 42 RVA: 0x000024CA File Offset: 0x000006CA
		public void Awake()
		{
			this._characterMaterialModifier = base.GetComponent<CharacterMaterialModifier>();
		}

		// Token: 0x0600002B RID: 43 RVA: 0x000024D8 File Offset: 0x000006D8
		public void SetTint(Color tintColor)
		{
			this._isEnabled = true;
			this._tintColor = tintColor;
			this.UpdateMaterialProperties();
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000024EE File Offset: 0x000006EE
		public void DisableTint()
		{
			this._isEnabled = false;
			this.UpdateMaterialProperties();
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002500 File Offset: 0x00000700
		public void InfluenceByChildhood(Character child)
		{
			CharacterTint component = child.GetComponent<CharacterTint>();
			if (component)
			{
				this.CopyFrom(component);
			}
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002523 File Offset: 0x00000723
		public void UpdateMaterialProperties()
		{
			this._characterMaterialModifier.SetColor(CharacterTint.TintColorId, this._tintColor);
			this._characterMaterialModifier.SetFloat(CharacterTint.TintEnabledId, this._isEnabled ? 1f : 0f);
		}

		// Token: 0x0600002F RID: 47 RVA: 0x0000255F File Offset: 0x0000075F
		public void CopyFrom(CharacterTint characterTint)
		{
			this._isEnabled = characterTint._isEnabled;
			this._tintColor = characterTint._tintColor;
			this.UpdateMaterialProperties();
		}

		// Token: 0x04000018 RID: 24
		public static readonly int TintColorId = Shader.PropertyToID("_TintColor");

		// Token: 0x04000019 RID: 25
		public static readonly int TintEnabledId = Shader.PropertyToID("_TintEnabled");

		// Token: 0x0400001A RID: 26
		public CharacterMaterialModifier _characterMaterialModifier;

		// Token: 0x0400001B RID: 27
		public bool _isEnabled;

		// Token: 0x0400001C RID: 28
		public Color _tintColor;
	}
}
