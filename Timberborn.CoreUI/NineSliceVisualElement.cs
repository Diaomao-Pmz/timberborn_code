using System;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x02000038 RID: 56
	[UxmlElement]
	public class NineSliceVisualElement : VisualElement
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00004550 File Offset: 0x00002750
		public NineSliceVisualElement()
		{
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), 0);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x000045A3 File Offset: 0x000027A3
		public void OnCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			this._nineSliceBackground.GetDataFromStyle(base.customStyle);
			base.MarkDirtyRepaint();
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000045BC File Offset: 0x000027BC
		public void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			this._nineSliceBackground.GenerateVisualContent(mgc, base.paddingRect);
		}

		// Token: 0x04000076 RID: 118
		public readonly NineSliceBackground _nineSliceBackground = new NineSliceBackground();

		// Token: 0x02000039 RID: 57
		[CompilerGenerated]
		[Serializable]
		public class UxmlSerializedData : VisualElement.UxmlSerializedData
		{
			// Token: 0x060000DA RID: 218 RVA: 0x000045D0 File Offset: 0x000027D0
			public override object CreateInstance()
			{
				return new NineSliceVisualElement();
			}
		}
	}
}
