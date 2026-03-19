using System;
using System.Reflection;
using UnityEngine.UIElements;

namespace Timberborn.DebuggingUI
{
	// Token: 0x02000013 RID: 19
	public class ObjectViewerFoldableNode : ObjectViewerNode
	{
		// Token: 0x0600006B RID: 107 RVA: 0x000039CB File Offset: 0x00001BCB
		public ObjectViewerFoldableNode(Foldout root, FieldInfo nodeFieldInfo, ObjectViewerNodeFactory objectViewerNodeFactory) : base(objectViewerNodeFactory, root, nodeFieldInfo)
		{
			this._foldout = root;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000039DD File Offset: 0x00001BDD
		public void InitializeFoldout()
		{
			INotifyValueChangedExtensions.RegisterValueChangedCallback<bool>(this._foldout, delegate(ChangeEvent<bool> evt)
			{
				if (evt.newValue)
				{
					this.Unfold();
				}
			});
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000039F7 File Offset: 0x00001BF7
		public void Unfold()
		{
			this._foldout.SetValueWithoutNotify(true);
			if (!this._wasUnfolded)
			{
				if (base.IsOddInHierarchy())
				{
					base.Root.AddToClassList(ObjectViewerFoldableNode.OddElementClass);
				}
				this.RecreateChildren();
			}
			this._wasUnfolded = true;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00003A32 File Offset: 0x00001C32
		public bool IsUnfolded
		{
			get
			{
				return this._foldout.value;
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x00003A3F File Offset: 0x00001C3F
		public virtual void RecreateChildren()
		{
		}

		// Token: 0x04000056 RID: 86
		public static readonly string OddElementClass = "object-viewer-foldout--odd";

		// Token: 0x04000057 RID: 87
		public readonly Foldout _foldout;

		// Token: 0x04000058 RID: 88
		public bool _wasUnfolded;
	}
}
