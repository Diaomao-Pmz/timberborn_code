using System;
using System.Reflection;
using UnityEngine.UIElements;

namespace Timberborn.DebuggingUI
{
	// Token: 0x02000016 RID: 22
	public class ObjectViewerObjectNode : ObjectViewerFoldableNode
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00003730 File Offset: 0x00001930
		public ObjectViewerObjectNode(Foldout root, FieldInfo nodeFieldInfo, ObjectViewerNodeFactory objectViewerNodeFactory) : base(root, nodeFieldInfo, objectViewerNodeFactory)
		{
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003D8C File Offset: 0x00001F8C
		public override void Update(object parentValue)
		{
			if (base.IsUnfolded)
			{
				object value = base.Value;
				base.Update(parentValue);
				if (!object.Equals(value, base.Value))
				{
					this.RecreateChildren();
				}
				foreach (ObjectViewerNode objectViewerNode in base.Children)
				{
					objectViewerNode.Update(base.Value);
				}
			}
		}

		// Token: 0x0600008A RID: 138 RVA: 0x00003E10 File Offset: 0x00002010
		public override void RecreateChildren()
		{
			base.ClearChildren();
			if (base.Value == null)
			{
				base.AddChild(base.ObjectViewerNodeFactory.CreateLabel("(null)"));
				return;
			}
			Type type = base.Value.GetType();
			BindingFlags flags = BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			this.AddTypeFields(type, flags);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x00003E5C File Offset: 0x0000205C
		public void AddTypeFields(Type type, BindingFlags flags)
		{
			foreach (FieldInfo fieldInfo in type.GetFields(flags))
			{
				if (!typeof(Delegate).IsAssignableFrom(fieldInfo.FieldType))
				{
					base.AddChild(base.ObjectViewerNodeFactory.CreateNode(fieldInfo.Name, fieldInfo, fieldInfo.FieldType));
				}
			}
			if (type.BaseType != null)
			{
				this.AddTypeFields(type.BaseType, flags);
			}
		}
	}
}
