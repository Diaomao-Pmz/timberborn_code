using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Timberborn.BaseComponentSystem;
using Timberborn.BlockSystem;
using Timberborn.Common;
using Timberborn.EntitySystem;
using Timberborn.Rendering;
using UnityEngine;

namespace Timberborn.Illumination
{
	// Token: 0x02000013 RID: 19
	public class Illuminator : BaseComponent, IAwakableComponent, IPreInitializableEntity, IPreviewStateListener
	{
		// Token: 0x06000092 RID: 146 RVA: 0x0000316D File Offset: 0x0000136D
		public Illuminator(IlluminationService illuminationService, MaterialColorer materialColorer)
		{
			this._illuminationService = illuminationService;
			this._materialColorer = materialColorer;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003199 File Offset: 0x00001399
		public void Awake()
		{
			this._illuminatorLightObjects = base.GetComponent<IlluminatorLightObjects>();
		}

		// Token: 0x06000094 RID: 148 RVA: 0x000031A7 File Offset: 0x000013A7
		public void PreInitializeEntity()
		{
			this.UpdateColor();
			this.Toggle(false);
		}

		// Token: 0x06000095 RID: 149 RVA: 0x000031B6 File Offset: 0x000013B6
		public void OnEnterPreviewState()
		{
			this.Toggle(false);
		}

		// Token: 0x06000096 RID: 150 RVA: 0x000031BF File Offset: 0x000013BF
		public IlluminatorToggle CreateToggle()
		{
			return new IlluminatorToggle(this);
		}

		// Token: 0x06000097 RID: 151 RVA: 0x000031C7 File Offset: 0x000013C7
		public IlluminatorColorizer CreateColorizer(int priority)
		{
			return new IlluminatorColorizer(this, priority);
		}

		// Token: 0x06000098 RID: 152 RVA: 0x000031D0 File Offset: 0x000013D0
		[UsedImplicitly]
		public void SetStrength(float strength)
		{
			this._strength = strength;
			this.UpdateStrength();
		}

		// Token: 0x06000099 RID: 153 RVA: 0x000031E0 File Offset: 0x000013E0
		public void IncrementTurnedOnToggles()
		{
			int turnedOnToggles = this._turnedOnToggles;
			this._turnedOnToggles = turnedOnToggles + 1;
			if (turnedOnToggles == 0)
			{
				this.Toggle(true);
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00003208 File Offset: 0x00001408
		public void DecrementTurnedOnToggles()
		{
			int num = this._turnedOnToggles - 1;
			this._turnedOnToggles = num;
			if (num == 0)
			{
				this.Toggle(false);
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000322F File Offset: 0x0000142F
		public void SetColor(int priority, Color color)
		{
			this._colors[priority] = color;
			this.UpdateColor();
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00003244 File Offset: 0x00001444
		public void ClearColor(int priority)
		{
			if (this._colors.Remove(priority))
			{
				this.UpdateColor();
			}
		}

		// Token: 0x0600009D RID: 157 RVA: 0x0000325A File Offset: 0x0000145A
		public void Toggle(bool state)
		{
			this._isOn = state;
			this.UpdateStrength();
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000326C File Offset: 0x0000146C
		public void UpdateStrength()
		{
			if (this._illuminatorLightObjects)
			{
				this._illuminatorLightObjects.SetActive(this._isOn);
			}
			if (this._isOn)
			{
				this._materialColorer.EnableLighting(this, new float?(this._strength));
				return;
			}
			this._materialColorer.DisableLighting(this);
		}

		// Token: 0x0600009F RID: 159 RVA: 0x000032C4 File Offset: 0x000014C4
		public void UpdateColor()
		{
			Color color = this._colors.IsEmpty<KeyValuePair<int, Color>>() ? this._illuminationService.DefaultColor : this._colors.Values[this._colors.Count - 1];
			Color? topColor = this._topColor;
			Color color2 = color;
			if (topColor == null || (topColor != null && topColor.GetValueOrDefault() != color2))
			{
				this._topColor = new Color?(color);
				this._materialColorer.SetLightingColor(this, color);
			}
		}

		// Token: 0x0400002D RID: 45
		public static readonly float DefaultStrength = 1f;

		// Token: 0x0400002E RID: 46
		public readonly IlluminationService _illuminationService;

		// Token: 0x0400002F RID: 47
		public readonly MaterialColorer _materialColorer;

		// Token: 0x04000030 RID: 48
		public IlluminatorLightObjects _illuminatorLightObjects;

		// Token: 0x04000031 RID: 49
		public float _strength = Illuminator.DefaultStrength;

		// Token: 0x04000032 RID: 50
		public bool _isOn;

		// Token: 0x04000033 RID: 51
		public int _turnedOnToggles;

		// Token: 0x04000034 RID: 52
		public readonly SortedList<int, Color> _colors = new SortedList<int, Color>();

		// Token: 0x04000035 RID: 53
		public Color? _topColor;
	}
}
