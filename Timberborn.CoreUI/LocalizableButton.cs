using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Timberborn.Localization;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x02000022 RID: 34
	[UxmlElement]
	public class LocalizableButton : Button, ILocalizableElement
	{
		// Token: 0x06000088 RID: 136 RVA: 0x000033FC File Offset: 0x000015FC
		public LocalizableButton()
		{
			Delegate[] invocationList = base.generateVisualContent.GetInvocationList();
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
			foreach (Delegate @delegate in invocationList)
			{
				base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Remove(base.generateVisualContent, (Action<MeshGenerationContext>)@delegate);
				base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, (Action<MeshGenerationContext>)@delegate);
			}
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), 0);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000089 RID: 137 RVA: 0x000034A5 File Offset: 0x000016A5
		public bool IsSet
		{
			get
			{
				return !string.IsNullOrEmpty(this._textLocKey);
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x000034B5 File Offset: 0x000016B5
		public void Localize(ILoc loc)
		{
			this.text = loc.T(this._textLocKey);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x000034C9 File Offset: 0x000016C9
		public void OnCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			this._nineSliceBackground.GetDataFromStyle(base.customStyle);
			base.MarkDirtyRepaint();
		}

		// Token: 0x0600008C RID: 140 RVA: 0x000034E2 File Offset: 0x000016E2
		public void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			if (this._nineSliceBackground.IsNineSlice)
			{
				this._nineSliceBackground.GenerateVisualContent(mgc, base.paddingRect);
			}
		}

		// Token: 0x0400004C RID: 76
		[UxmlAttribute("text-loc-key")]
		public string _textLocKey;

		// Token: 0x0400004D RID: 77
		public readonly NineSliceBackground _nineSliceBackground = new NineSliceBackground();

		// Token: 0x02000023 RID: 35
		[CompilerGenerated]
		[Serializable]
		public class UxmlSerializedData : Button.UxmlSerializedData
		{
			// Token: 0x0600008D RID: 141 RVA: 0x00003503 File Offset: 0x00001703
			[RegisterUxmlCache]
			[Conditional("UNITY_EDITOR")]
			public static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(LocalizableButton.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("_textLocKey", "text-loc-key", null, Array.Empty<string>())
				}, false);
			}

			// Token: 0x0600008E RID: 142 RVA: 0x00003537 File Offset: 0x00001737
			public override object CreateInstance()
			{
				return new LocalizableButton();
			}

			// Token: 0x0600008F RID: 143 RVA: 0x00003540 File Offset: 0x00001740
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				LocalizableButton localizableButton = (LocalizableButton)obj;
				if (UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this._textLocKey_UxmlAttributeFlags))
				{
					localizableButton._textLocKey = this._textLocKey;
				}
			}

			// Token: 0x0400004E RID: 78
			[UxmlAttribute("text-loc-key")]
			[SerializeField]
			private string _textLocKey;

			// Token: 0x0400004F RID: 79
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags _textLocKey_UxmlAttributeFlags;
		}
	}
}
