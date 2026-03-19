using System;
using Timberborn.BaseComponentSystem;
using Timberborn.CoreUI;
using Timberborn.EntityPanelSystem;
using Timberborn.PowerGeneration;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.PowerGenerationUI
{
	// Token: 0x02000007 RID: 7
	public class AdjustableStrengthPowerGeneratorFragment : IEntityPanelFragment
	{
		// Token: 0x06000007 RID: 7 RVA: 0x00002100 File Offset: 0x00000300
		public AdjustableStrengthPowerGeneratorFragment(VisualElementLoader visualElementLoader, IntegerSliderFactory integerSliderFactory)
		{
			this._visualElementLoader = visualElementLoader;
			this._integerSliderFactory = integerSliderFactory;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002118 File Offset: 0x00000318
		public VisualElement InitializeFragment()
		{
			this._root = this._visualElementLoader.LoadVisualElement("Game/EntityPanel/AdjustableStrengthPowerGeneratorFragment");
			this._sliderRoot = UQueryExtensions.Q<VisualElement>(this._root, "SliderRoot", null);
			UQueryExtensions.Q<Button>(this._root, "FlipRotation", null).RegisterCallback<ClickEvent>(delegate(ClickEvent _)
			{
				this.FlipRotation();
			}, 0);
			return this._root;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x0000217C File Offset: 0x0000037C
		public void ShowFragment(BaseComponent entity)
		{
			this._generator = entity.GetComponent<AdjustableStrengthPowerGenerator>();
			if (this._generator)
			{
				this._sliderRoot.Add(this.CreateSlider());
				this._root.ToggleDisplayStyle(true);
				return;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021CC File Offset: 0x000003CC
		public void ClearFragment()
		{
			if (this._generator)
			{
				this._sliderRoot.Clear();
				this._generator = null;
			}
			this._root.ToggleDisplayStyle(false);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021F9 File Offset: 0x000003F9
		public void UpdateFragment()
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021FB File Offset: 0x000003FB
		public void FlipRotation()
		{
			if (this._generator)
			{
				this._generator.FlipRotation();
			}
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002218 File Offset: 0x00000418
		public VisualElement CreateSlider()
		{
			int current = Mathf.RoundToInt(this._generator.GeneratorStrength * (float)this._generator.MaxValue);
			return this._integerSliderFactory.Create(current, this._generator.MaxValue, new Action<int>(this.ChangeValue));
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002266 File Offset: 0x00000466
		public void ChangeValue(int newValue)
		{
			this._generator.GeneratorStrength = (float)newValue / (float)this._generator.MaxValue;
		}

		// Token: 0x04000008 RID: 8
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000009 RID: 9
		public readonly IntegerSliderFactory _integerSliderFactory;

		// Token: 0x0400000A RID: 10
		public AdjustableStrengthPowerGenerator _generator;

		// Token: 0x0400000B RID: 11
		public VisualElement _root;

		// Token: 0x0400000C RID: 12
		public VisualElement _sliderRoot;
	}
}
