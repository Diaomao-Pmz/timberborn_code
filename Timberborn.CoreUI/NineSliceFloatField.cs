using System;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x02000030 RID: 48
	[UxmlElement]
	public class NineSliceFloatField : FloatField
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x000042C0 File Offset: 0x000024C0
		public NineSliceFloatField()
		{
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), 0);
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004313 File Offset: 0x00002513
		public void OnCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			this._nineSliceBackground.GetDataFromStyle(base.customStyle);
			base.MarkDirtyRepaint();
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x0000432C File Offset: 0x0000252C
		public void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			this._nineSliceBackground.GenerateVisualContent(mgc, base.paddingRect);
		}

		// Token: 0x04000072 RID: 114
		public readonly NineSliceBackground _nineSliceBackground = new NineSliceBackground();

		// Token: 0x02000031 RID: 49
		[CompilerGenerated]
		[Serializable]
		public class UxmlSerializedData : FloatField.UxmlSerializedData
		{
			// Token: 0x060000C6 RID: 198 RVA: 0x00004340 File Offset: 0x00002540
			public override object CreateInstance()
			{
				return new NineSliceFloatField();
			}
		}
	}
}
