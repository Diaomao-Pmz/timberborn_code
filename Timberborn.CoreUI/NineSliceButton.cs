using System;
using System.Runtime.CompilerServices;
using UnityEngine.UIElements;

namespace Timberborn.CoreUI
{
	// Token: 0x0200002E RID: 46
	[UxmlElement]
	public class NineSliceButton : Button
	{
		// Token: 0x060000BE RID: 190 RVA: 0x000041E0 File Offset: 0x000023E0
		public NineSliceButton()
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

		// Token: 0x060000BF RID: 191 RVA: 0x00004289 File Offset: 0x00002489
		public void OnCustomStyleResolved(CustomStyleResolvedEvent e)
		{
			this._nineSliceBackground.GetDataFromStyle(base.customStyle);
			base.MarkDirtyRepaint();
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x000042A2 File Offset: 0x000024A2
		public void OnGenerateVisualContent(MeshGenerationContext mgc)
		{
			this._nineSliceBackground.GenerateVisualContent(mgc, base.paddingRect);
		}

		// Token: 0x04000071 RID: 113
		public readonly NineSliceBackground _nineSliceBackground = new NineSliceBackground();

		// Token: 0x0200002F RID: 47
		[CompilerGenerated]
		[Serializable]
		public class UxmlSerializedData : Button.UxmlSerializedData
		{
			// Token: 0x060000C1 RID: 193 RVA: 0x000042B6 File Offset: 0x000024B6
			public override object CreateInstance()
			{
				return new NineSliceButton();
			}
		}
	}
}
