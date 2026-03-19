using System;
using System.Collections;
using System.Collections.Immutable;
using System.Reflection;
using UnityEngine.UIElements;

namespace Timberborn.DebuggingUI
{
	// Token: 0x02000011 RID: 17
	public class ObjectViewerEnumerableNode : ObjectViewerFoldableNode
	{
		// Token: 0x06000064 RID: 100 RVA: 0x00003730 File Offset: 0x00001930
		public ObjectViewerEnumerableNode(Foldout root, FieldInfo nodeFieldInfo, ObjectViewerNodeFactory objectViewerNodeFactory) : base(root, nodeFieldInfo, objectViewerNodeFactory)
		{
		}

		// Token: 0x06000065 RID: 101 RVA: 0x0000373C File Offset: 0x0000193C
		public override void Update(object parent)
		{
			if (base.IsUnfolded)
			{
				base.Update(parent);
				int ienumerableHashCode = this.GetIEnumerableHashCode();
				if (ienumerableHashCode != this._lastHashCode)
				{
					this.RecreateChildren();
				}
				this._lastHashCode = ienumerableHashCode;
				IEnumerable enumerable;
				if (ObjectViewerEnumerableNode.IsValidEnumerable(base.Value, out enumerable))
				{
					int num = 0;
					foreach (object parentValue in enumerable)
					{
						base.Children[num++].Update(parentValue);
					}
				}
			}
		}

		// Token: 0x06000066 RID: 102 RVA: 0x000037E0 File Offset: 0x000019E0
		public override void RecreateChildren()
		{
			base.ClearChildren();
			IEnumerable enumerable = base.Value as IEnumerable;
			if (enumerable != null)
			{
				int num = 0;
				foreach (object obj in enumerable)
				{
					string str = string.Format("[{0}]", num++);
					if (obj == null)
					{
						base.AddChild(base.ObjectViewerNodeFactory.CreateLabel("(null)"));
					}
					else
					{
						Type type = obj.GetType();
						base.AddChild(base.ObjectViewerNodeFactory.CreateNode(str + ": " + type.Name, null, type));
					}
				}
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000038A8 File Offset: 0x00001AA8
		public int GetIEnumerableHashCode()
		{
			IEnumerable enumerable;
			if (ObjectViewerEnumerableNode.IsValidEnumerable(base.Value, out enumerable))
			{
				HashCode hashCode = default(HashCode);
				foreach (object obj in enumerable)
				{
					hashCode.Add<object>(obj);
				}
				return hashCode.ToHashCode();
			}
			return 0;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000391C File Offset: 0x00001B1C
		public static bool IsValidEnumerable(object value, out IEnumerable enumerable)
		{
			enumerable = (value as IEnumerable);
			if (enumerable == null)
			{
				return false;
			}
			Type type = enumerable.GetType();
			if (!type.IsGenericType)
			{
				return true;
			}
			if (type.GetGenericTypeDefinition() != typeof(ImmutableArray<>))
			{
				return true;
			}
			PropertyInfo property = type.GetProperty("IsDefault");
			return property == null || !(bool)property.GetValue(enumerable);
		}

		// Token: 0x04000054 RID: 84
		public int _lastHashCode;
	}
}
