using System;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x02000032 RID: 50
	[UxmlElement]
	public class NineSliceIntegerField : IntegerField
	{
		// Token: 0x060000C8 RID: 200 RVA: 0x00004350 File Offset: 0x00002550
		public NineSliceIntegerField()
		{
			base.generateVisualContent = (Action<MeshGenerationContext>)Delegate.Combine(base.generateVisualContent, new Action<MeshGenerationContext>(this.OnGenerateVisualContent));
			base.RegisterCallback<CustomStyleResolvedEvent>(new EventCallback<CustomStyleResolvedEvent>(this.OnCustomStyleResolved), 0);
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000043A3 File Offset: 0x000025A3
		public void OnCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			this._nineSliceBackground.GetDataFromStyle(base.customStyle);
			base.MarkDirtyRepaint();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000043BC File Offset: 0x000025BC
		public void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			this._nineSliceBackground.GenerateVisualContent(mgc, base.paddingRect);
		}

		// Token: 0x04000073 RID: 115
		public readonly NineSliceBackground _nineSliceBackground = new NineSliceBackground();

		// Token: 0x02000033 RID: 51
		[CompilerGenerated]
		[Serializable]
		public class UxmlSerializedData : IntegerField.UxmlSerializedData
		{
			// Token: 0x060000CB RID: 203 RVA: 0x000043D0 File Offset: 0x000025D0
			public override object CreateInstance()
			{
				return new NineSliceIntegerField();
			}
		}
	}
}
