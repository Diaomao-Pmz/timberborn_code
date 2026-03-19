using System;
using System.Collections.Generic;
using System.Linq;
using Timberborn.Localization;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x02000045 RID: 69
	public class RadioToggleFactory
	{
		// Token: 0x0600011E RID: 286 RVA: 0x00005072 File Offset: 0x00003272
		public RadioToggleFactory(VisualElementLoader visualElementLoader, ILoc loc)
		{
			this._visualElementLoader = visualElementLoader;
			this._loc = loc;
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005088 File Offset: 0x00003288
		public RadioToggle CreateLocalizable(IEnumerable<string> optionLocKeys, VisualElement parent)
		{
			return this.Create(from optionLocKey in optionLocKeys
			select this._loc.T(optionLocKey), parent);
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000050A4 File Offset: 0x000032A4
		public RadioToggle CreateLocalizable<T>(string optionLocKeyPrefix, VisualElement parent) where T : Enum
		{
			IEnumerable<string> optionLocKeys = Enum.GetValues(typeof(T)).Cast<T>().Select(delegate(T value)
			{
				string optionLocKeyPrefix2 = optionLocKeyPrefix;
				T t = value;
				return optionLocKeyPrefix2 + ((t != null) ? t.ToString() : null);
			});
			return this.CreateLocalizable(optionLocKeys, parent);
		}

		// Token: 0x06000121 RID: 289 RVA: 0x000050EC File Offset: 0x000032EC
		public RadioToggle Create(IEnumerable<string> options, VisualElement parent)
		{
			List<VisualElement> list = new List<VisualElement>();
			foreach (string option in options)
			{
				VisualElement visualElement = this.CreateRadioButton(option);
				parent.Add(visualElement);
				list.Add(visualElement);
			}
			return RadioToggle.Create(list);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00005150 File Offset: 0x00003350
		public VisualElement CreateRadioButton(string option)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Core/RadioButton");
			UQueryExtensions.Q<Label>(visualElement, "Text", null).text = option;
			return visualElement;
		}

		// Token: 0x04000096 RID: 150
		public readonly VisualElementLoader _visualElementLoader;

		// Token: 0x04000097 RID: 151
		public readonly ILoc _loc;
	}
}
