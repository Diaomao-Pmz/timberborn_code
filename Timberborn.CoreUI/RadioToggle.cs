using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x02000042 RID: 66
	public class RadioToggle
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000115 RID: 277 RVA: 0x00004EFC File Offset: 0x000030FC
		// (remove) Token: 0x06000116 RID: 278 RVA: 0x00004F34 File Offset: 0x00003134
		public event EventHandler<int> RadioButtonSelected;

		// Token: 0x06000117 RID: 279 RVA: 0x00004F69 File Offset: 0x00003169
		public RadioToggle(IEnumerable<VisualElement> radioButtons)
		{
			this._radioButtons = radioButtons.ToImmutableArray<VisualElement>();
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00004F80 File Offset: 0x00003180
		public static RadioToggle Create(IEnumerable<VisualElement> radioButtons)
		{
			RadioToggle radioButtonToggle = new RadioToggle(radioButtons);
			for (int i = 0; i < radioButtonToggle._radioButtons.Length; i++)
			{
				int capturedIndex = i;
				radioButtonToggle._radioButtons[i].RegisterCallback<ClickEvent>(delegate(ClickEvent _)
				{
					EventHandler<int> radioButtonSelected = radioButtonToggle.RadioButtonSelected;
					if (radioButtonSelected == null)
					{
						return;
					}
					radioButtonSelected(radioButtonToggle, capturedIndex);
				}, 0);
			}
			return radioButtonToggle;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00004FFC File Offset: 0x000031FC
		public void Update(int selectedIndex)
		{
			for (int i = 0; i < this._radioButtons.Length; i++)
			{
				this._radioButtons[i].EnableInClassList(RadioToggle.SelectedClass, i == selectedIndex);
			}
		}

		// Token: 0x04000090 RID: 144
		public static readonly string SelectedClass = "selected";

		// Token: 0x04000092 RID: 146
		public readonly ImmutableArray<VisualElement> _radioButtons;
	}
}
