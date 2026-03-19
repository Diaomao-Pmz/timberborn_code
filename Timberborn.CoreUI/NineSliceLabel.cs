using System;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x02000034 RID: 52
	[UxmlElement]
	public class NineSliceLabel : Label
	{
		// Token: 0x060000CD RID: 205 RVA: 0x000043E0 File Offset: 0x000025E0
		public NineSliceLabel()
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

		// Token: 0x060000CE RID: 206 RVA: 0x00004489 File Offset: 0x00002689
		public void OnCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			this._nineSliceBackground.GetDataFromStyle(base.customStyle);
			base.MarkDirtyRepaint();
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000044A2 File Offset: 0x000026A2
		public void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			this._nineSliceBackground.GenerateVisualContent(mgc, base.paddingRect);
		}

		// Token: 0x04000074 RID: 116
		public readonly NineSliceBackground _nineSliceBackground = new NineSliceBackground();

		// Token: 0x02000035 RID: 53
		[CompilerGenerated]
		[Serializable]
		public class UxmlSerializedData : Label.UxmlSerializedData
		{
			// Token: 0x060000D0 RID: 208 RVA: 0x000044B6 File Offset: 0x000026B6
			public override object CreateInstance()
			{
				return new NineSliceLabel();
			}
		}
	}
}
