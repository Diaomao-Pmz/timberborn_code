using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Timberborn.Localization;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x0200002A RID: 42
	[UxmlElement]
	public class LocalizableToggle : Toggle, ILocalizableElement
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x00003844 File Offset: 0x00001A44
		public LocalizableToggle()
		{
			Action<MeshGenerationContext> generateVisualContent = base.generateVisualContent;
			Delegate[] array = ((generateVisualContent != null) ? generateVisualContent.GetInvocationList() : null) ?? new Delegate[0];
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
			foreach (Delegate @delegate in array)
			{
				base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Remove(base.generateVisualContent, (Action<MeshGenerationContext>)@delegate);
				base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, (Action<MeshGenerationContext>)@delegate);
			}
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), 0);
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000038FE File Offset: 0x00001AFE
		public bool IsSet
		{
			get
			{
				return !string.IsNullOrEmpty(this._textLocKey);
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x0000390E File Offset: 0x00001B0E
		public void Localize(ILoc loc)
		{
			base.text = loc.T(this._textLocKey);
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00003922 File Offset: 0x00001B22
		public void OnCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			this._nineSliceBackground.GetDataFromStyle(base.customStyle);
			base.MarkDirtyRepaint();
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000393B File Offset: 0x00001B3B
		public void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			if (this._nineSliceBackground.IsNineSlice)
			{
				this._nineSliceBackground.GenerateVisualContent(mgc, base.paddingRect);
			}
		}

		// Token: 0x0400005A RID: 90
		[UxmlAttribute("text-loc-key")]
		public string _textLocKey;

		// Token: 0x0400005B RID: 91
		public readonly NineSliceBackground _nineSliceBackground = new NineSliceBackground();

		// Token: 0x0200002B RID: 43
		[CompilerGenerated]
		[Serializable]
		public class UxmlSerializedData : Toggle.UxmlSerializedData
		{
			// Token: 0x060000AD RID: 173 RVA: 0x0000395C File Offset: 0x00001B5C
			[RegisterUxmlCache]
			[Conditional("UNITY_EDITOR")]
			public static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(LocalizableToggle.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("_textLocKey", "text-loc-key", null, Array.Empty<string>())
				}, false);
			}

			// Token: 0x060000AE RID: 174 RVA: 0x00003990 File Offset: 0x00001B90
			public override object CreateInstance()
			{
				return new LocalizableToggle();
			}

			// Token: 0x060000AF RID: 175 RVA: 0x00003998 File Offset: 0x00001B98
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				LocalizableToggle localizableToggle = (LocalizableToggle)obj;
				if (UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this._textLocKey_UxmlAttributeFlags))
				{
					localizableToggle._textLocKey = this._textLocKey;
				}
			}

			// Token: 0x0400005C RID: 92
			[UxmlAttribute("text-loc-key")]
			[SerializeField]
			private string _textLocKey;

			// Token: 0x0400005D RID: 93
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags _textLocKey_UxmlAttributeFlags;
		}
	}
}
