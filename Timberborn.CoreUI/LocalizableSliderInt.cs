using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Timberborn.Localization;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x02000028 RID: 40
	[UxmlElement]
	public class LocalizableSliderInt : SliderInt, ILocalizableElement
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x000037A0 File Offset: 0x000019A0
		public bool IsSet
		{
			get
			{
				return !string.IsNullOrEmpty(this._textLocKey);
			}
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000037B0 File Offset: 0x000019B0
		public void Localize(ILoc loc)
		{
			base.label = loc.T(this._textLocKey);
		}

		// Token: 0x04000057 RID: 87
		[UxmlAttribute("text-loc-key")]
		public string _textLocKey;

		// Token: 0x02000029 RID: 41
		[CompilerGenerated]
		[Serializable]
		public class UxmlSerializedData : SliderInt.UxmlSerializedData
		{
			// Token: 0x060000A4 RID: 164 RVA: 0x000037CC File Offset: 0x000019CC
			[RegisterUxmlCache]
			[Conditional("UNITY_EDITOR")]
			public static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(LocalizableSliderInt.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("_textLocKey", "text-loc-key", null, Array.Empty<string>())
				}, false);
			}

			// Token: 0x060000A5 RID: 165 RVA: 0x00003800 File Offset: 0x00001A00
			public override object CreateInstance()
			{
				return new LocalizableSliderInt();
			}

			// Token: 0x060000A6 RID: 166 RVA: 0x00003808 File Offset: 0x00001A08
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				LocalizableSliderInt localizableSliderInt = (LocalizableSliderInt)obj;
				if (UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this._textLocKey_UxmlAttributeFlags))
				{
					localizableSliderInt._textLocKey = this._textLocKey;
				}
			}

			// Token: 0x04000058 RID: 88
			[UxmlAttribute("text-loc-key")]
			[SerializeField]
			private string _textLocKey;

			// Token: 0x04000059 RID: 89
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags _textLocKey_UxmlAttributeFlags;
		}
	}
}
