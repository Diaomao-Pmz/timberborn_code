using System;
using System.Collections.Generic;
using System.Reflection;
using Timberborn.Common;
using UnityEngine.UIElements;

namespace Timberborn.DebuggingUI
{
	// Token: 0x02000014 RID: 20
	public class ObjectViewerNode
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00003A5D File Offset: 0x00001C5D
		public VisualElement Root { get; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00003A65 File Offset: 0x00001C65
		protected ObjectViewerNodeFactory ObjectViewerNodeFactory { get; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000074 RID: 116 RVA: 0x00003A6D File Offset: 0x00001C6D
		// (set) Token: 0x06000075 RID: 117 RVA: 0x00003A75 File Offset: 0x00001C75
		private protected object Value { protected get; private set; }

		// Token: 0x06000076 RID: 118 RVA: 0x00003A7E File Offset: 0x00001C7E
		public ObjectViewerNode(ObjectViewerNodeFactory objectViewerNodeFactory, VisualElement root, FieldInfo nodeFieldInfo)
		{
			this.ObjectViewerNodeFactory = objectViewerNodeFactory;
			this.Root = root;
			this._nodeFieldInfo = nodeFieldInfo;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003AA6 File Offset: 0x00001CA6
		public virtual void Update(object parentValue)
		{
			this.Value = ((this._nodeFieldInfo != null) ? this._nodeFieldInfo.GetValue(parentValue) : parentValue);
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00003ACB File Offset: 0x00001CCB
		public ReadOnlyList<ObjectViewerNode> Children
		{
			get
			{
				return this._children.AsReadOnlyList<ObjectViewerNode>();
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003AD8 File Offset: 0x00001CD8
		public void AddChild(ObjectViewerNode child)
		{
			this._children.Add(child);
			this.Content.Add(child.Root);
			child._parent = this;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003AFE File Offset: 0x00001CFE
		public void ClearChildren()
		{
			this.Content.Clear();
			this._children.Clear();
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003B16 File Offset: 0x00001D16
		public bool IsOddInHierarchy()
		{
			return this._parent == null || !this._parent.IsOddInHierarchy();
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00003B30 File Offset: 0x00001D30
		public VisualElement Content
		{
			get
			{
				VisualElement result;
				if ((result = this._content) == null)
				{
					result = (this._content = (UQueryExtensions.Q<VisualElement>(this.Root, "Content", null) ?? this.Root));
				}
				return result;
			}
		}

		// Token: 0x0400005C RID: 92
		public readonly FieldInfo _nodeFieldInfo;

		// Token: 0x0400005D RID: 93
		public readonly List<ObjectViewerNode> _children = new List<ObjectViewerNode>();

		// Token: 0x0400005E RID: 94
		public VisualElement _content;

		// Token: 0x0400005F RID: 95
		public ObjectViewerNode _parent;
	}
}
