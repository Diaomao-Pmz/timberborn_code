using System;
using Timberborn.InputSystem;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x02000050 RID: 80
	public class TextElementInitializer : IVisualElementInitializer
	{
		// Token: 0x0600014A RID: 330 RVA: 0x000058B5 File Offset: 0x00003AB5
		public TextElementInitializer(InputBlocker inputBlocker)
		{
			this._inputBlocker = inputBlocker;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000058C4 File Offset: 0x00003AC4
		public void InitializeVisualElement(VisualElement visualElement)
		{
			TextField textField = visualElement as TextField;
			if (textField != null)
			{
				if (textField.isReadOnly)
				{
					goto IL_3E;
				}
			}
			else
			{
				IntegerField integerField = visualElement as IntegerField;
				if (integerField != null)
				{
					if (integerField.isReadOnly)
					{
						goto IL_3E;
					}
				}
				else
				{
					FloatField floatField = visualElement as FloatField;
					if (floatField == null || floatField.isReadOnly)
					{
						goto IL_3E;
					}
				}
			}
			bool flag = true;
			goto IL_40;
			IL_3E:
			flag = false;
			IL_40:
			if (flag)
			{
				TextElement textElement = UQueryExtensions.Q<TextElement>(visualElement, null, null);
				textElement.RegisterCallback<FocusInEvent>(new EventCallback<FocusInEvent>(this.OnFocusIn), 0);
				textElement.RegisterCallback<FocusOutEvent>(new EventCallback<FocusOutEvent>(this.OnFocusOut), 0);
			}
		}

		// Token: 0x0600014C RID: 332 RVA: 0x00005941 File Offset: 0x00003B41
		public void OnFocusIn(FocusInEvent evt)
		{
			this._inputBlocker.Block();
		}

		// Token: 0x0600014D RID: 333 RVA: 0x0000594E File Offset: 0x00003B4E
		public void OnFocusOut(FocusOutEvent evt)
		{
			this._inputBlocker.Unblock();
		}

		// Token: 0x040000B0 RID: 176
		public readonly InputBlocker _inputBlocker;
	}
}
