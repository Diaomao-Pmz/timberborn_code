using System;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x02000036 RID: 54
	[UxmlElement]
	public class NineSliceTextField : TextField
	{
		// Token: 0x060000D2 RID: 210 RVA: 0x000044C0 File Offset: 0x000026C0
		public NineSliceTextField()
		{
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), 0);
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x00004513 File Offset: 0x00002713
		public void OnCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			this._nineSliceBackground.GetDataFromStyle(base.customStyle);
			base.MarkDirtyRepaint();
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x0000452C File Offset: 0x0000272C
		public void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			this._nineSliceBackground.GenerateVisualContent(mgc, base.paddingRect);
		}

		// Token: 0x04000075 RID: 117
		public readonly NineSliceBackground _nineSliceBackground = new NineSliceBackground();

		// Token: 0x02000037 RID: 55
		[CompilerGenerated]
		[Serializable]
		public class UxmlSerializedData : TextField.UxmlSerializedData
		{
			// Token: 0x060000D5 RID: 213 RVA: 0x00004540 File Offset: 0x00002740
			public override object CreateInstance()
			{
				return new NineSliceTextField();
			}
		}
	}
}
