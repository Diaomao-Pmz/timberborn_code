using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Timberborn.Localization;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x02000026 RID: 38
	[UxmlElement]
	public class LocalizableSlider : Slider, ILocalizableElement
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000036FC File Offset: 0x000018FC
		public bool IsSet
		{
			get
			{
				return !string.IsNullOrEmpty(this._textLocKey);
			}
		}

		// Token: 0x0600009B RID: 155 RVA: 0x0000370C File Offset: 0x0000190C
		public void Localize(ILoc loc)
		{
			base.label = loc.T(this._textLocKey);
		}

		// Token: 0x04000054 RID: 84
		[UxmlAttribute("text-loc-key")]
		public string _textLocKey;

		// Token: 0x02000027 RID: 39
		[CompilerGenerated]
		[Serializable]
		public class UxmlSerializedData : Slider.UxmlSerializedData
		{
			// Token: 0x0600009D RID: 157 RVA: 0x00003728 File Offset: 0x00001928
			[RegisterUxmlCache]
			[Conditional("UNITY_EDITOR")]
			public static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(LocalizableSlider.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("_textLocKey", "text-loc-key", null, Array.Empty<string>())
				}, false);
			}

			// Token: 0x0600009E RID: 158 RVA: 0x0000375C File Offset: 0x0000195C
			public override object CreateInstance()
			{
				return new LocalizableSlider();
			}

			// Token: 0x0600009F RID: 159 RVA: 0x00003764 File Offset: 0x00001964
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				LocalizableSlider localizableSlider = (LocalizableSlider)obj;
				if (UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this._textLocKey_UxmlAttributeFlags))
				{
					localizableSlider._textLocKey = this._textLocKey;
				}
			}

			// Token: 0x04000055 RID: 85
			[UxmlAttribute("text-loc-key")]
			[SerializeField]
			private string _textLocKey;

			// Token: 0x04000056 RID: 86
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags _textLocKey_UxmlAttributeFlags;
		}
	}
}
