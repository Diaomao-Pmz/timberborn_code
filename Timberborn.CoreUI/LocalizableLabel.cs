using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Timberborn.Localization;
using UnityEngine;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x02000024 RID: 36
	[UxmlElement]
	public class LocalizableLabel : Label, ILocalizableElement
	{
		// Token: 0x06000091 RID: 145 RVA: 0x0000357C File Offset: 0x0000177C
		public LocalizableLabel()
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

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00003625 File Offset: 0x00001825
		public bool IsSet
		{
			get
			{
				return !string.IsNullOrEmpty(this._textLocKey);
			}
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003635 File Offset: 0x00001835
		public void Localize(ILoc loc)
		{
			this.text = loc.T(this._textLocKey);
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00003649 File Offset: 0x00001849
		public void OnCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			this._nineSliceBackground.GetDataFromStyle(base.customStyle);
			base.MarkDirtyRepaint();
		}

		// Token: 0x06000095 RID: 149 RVA: 0x00003662 File Offset: 0x00001862
		public void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			if (this._nineSliceBackground.IsNineSlice)
			{
				this._nineSliceBackground.GenerateVisualContent(mgc, base.paddingRect);
			}
		}

		// Token: 0x04000050 RID: 80
		[UxmlAttribute("text-loc-key")]
		public string _textLocKey;

		// Token: 0x04000051 RID: 81
		public readonly NineSliceBackground _nineSliceBackground = new NineSliceBackground();

		// Token: 0x02000025 RID: 37
		[CompilerGenerated]
		[Serializable]
		public class UxmlSerializedData : Label.UxmlSerializedData
		{
			// Token: 0x06000096 RID: 150 RVA: 0x00003683 File Offset: 0x00001883
			[RegisterUxmlCache]
			[Conditional("UNITY_EDITOR")]
			public static void Register()
			{
				UxmlDescriptionCache.RegisterType(typeof(LocalizableLabel.UxmlSerializedData), new UxmlAttributeNames[]
				{
					new UxmlAttributeNames("_textLocKey", "text-loc-key", null, Array.Empty<string>())
				}, false);
			}

			// Token: 0x06000097 RID: 151 RVA: 0x000036B7 File Offset: 0x000018B7
			public override object CreateInstance()
			{
				return new LocalizableLabel();
			}

			// Token: 0x06000098 RID: 152 RVA: 0x000036C0 File Offset: 0x000018C0
			public override void Deserialize(object obj)
			{
				base.Deserialize(obj);
				LocalizableLabel localizableLabel = (LocalizableLabel)obj;
				if (UnityEngine.UIElements.UxmlSerializedData.ShouldWriteAttributeValue(this._textLocKey_UxmlAttributeFlags))
				{
					localizableLabel._textLocKey = this._textLocKey;
				}
			}

			// Token: 0x04000052 RID: 82
			[UxmlAttribute("text-loc-key")]
			[SerializeField]
			private string _textLocKey;

			// Token: 0x04000053 RID: 83
			[SerializeField]
			[UxmlIgnore]
			[HideInInspector]
			private UnityEngine.UIElements.UxmlSerializedData.UxmlAttributeFlags _textLocKey_UxmlAttributeFlags;
		}
	}
}
