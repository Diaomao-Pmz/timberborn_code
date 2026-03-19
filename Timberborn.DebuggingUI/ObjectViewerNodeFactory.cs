using System;
using System.Collections;
using System.Reflection;
using Timberborn.CoreUI;
using UnityEngine.UIElements;

namespace Timberborn.DebuggingUI
{
	// Token: 0x02000015 RID: 21
	public class ObjectViewerNodeFactory
	{
		// Token: 0x0600007D RID: 125 RVA: 0x00003B6B File Offset: 0x00001D6B
		public ObjectViewerNodeFactory(VisualElementLoader visualElementLoader)
		{
			this._visualElementLoader = visualElementLoader;
		}

		// Token: 0x0600007E RID: 126 RVA: 0x00003B7A File Offset: 0x00001D7A
		public ObjectViewerNode CreateRoot(string title)
		{
			ObjectViewerObjectNode objectViewerObjectNode = this.CreateObject(title, null);
			objectViewerObjectNode.Unfold();
			return objectViewerObjectNode;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x00003B8A File Offset: 0x00001D8A
		public ObjectViewerNode CreateLabel(string text)
		{
			return new ObjectViewerNode(this, new Label(text), null);
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003B9C File Offset: 0x00001D9C
		public ObjectViewerNode CreateNode(string label, FieldInfo fieldInfo, Type type)
		{
			ObjectViewerNode result;
			try
			{
				if (ObjectViewerNodeFactory.IsEnumerableType(type))
				{
					result = this.CreateEnumerable(label, fieldInfo);
				}
				else if (ObjectViewerNodeFactory.IsPrimitiveType(type))
				{
					result = this.CreateField(label, fieldInfo);
				}
				else
				{
					result = this.CreateObject(label, fieldInfo);
				}
			}
			catch (Exception ex)
			{
				result = this.CreateLabel("Error: " + ex.Message);
			}
			return result;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003C08 File Offset: 0x00001E08
		public ObjectViewerFieldNode CreateField(string label, FieldInfo fieldInfo)
		{
			VisualElement visualElement = this._visualElementLoader.LoadVisualElement("Common/DebuggingPanel/ObjectViewerField");
			UQueryExtensions.Q<Label>(visualElement, "FieldName", null).text = ObjectViewerNodeFactory.FormatLabel(label);
			TextField textField = UQueryExtensions.Q<TextField>(visualElement, "FieldValue", null);
			return new ObjectViewerFieldNode(visualElement, fieldInfo, this, textField);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003C51 File Offset: 0x00001E51
		public ObjectViewerObjectNode CreateObject(string title, FieldInfo fieldInfo)
		{
			ObjectViewerObjectNode objectViewerObjectNode = new ObjectViewerObjectNode(this.CreateFoldout(title), fieldInfo, this);
			objectViewerObjectNode.InitializeFoldout();
			return objectViewerObjectNode;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00003C67 File Offset: 0x00001E67
		public ObjectViewerEnumerableNode CreateEnumerable(string title, FieldInfo fieldInfo)
		{
			ObjectViewerEnumerableNode objectViewerEnumerableNode = new ObjectViewerEnumerableNode(this.CreateFoldout(title), fieldInfo, this);
			objectViewerEnumerableNode.InitializeFoldout();
			return objectViewerEnumerableNode;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00003C80 File Offset: 0x00001E80
		public Foldout CreateFoldout(string title)
		{
			string elementName = "Common/DebuggingPanel/ObjectViewerFoldout";
			Foldout foldout = (Foldout)this._visualElementLoader.LoadVisualElement(elementName);
			foldout.text = ObjectViewerNodeFactory.FormatLabel(title);
			foldout.value = false;
			return foldout;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003CB8 File Offset: 0x00001EB8
		public static bool IsPrimitiveType(Type type)
		{
			return type.IsPrimitive || type.IsEnum || (type == typeof(string) || type == typeof(decimal) || type == typeof(DateTime) || type == typeof(Guid));
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00003D22 File Offset: 0x00001F22
		public static bool IsEnumerableType(Type type)
		{
			return type != typeof(string) && typeof(IEnumerable).IsAssignableFrom(type);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x00003D48 File Offset: 0x00001F48
		public static string FormatLabel(string name)
		{
			if (name.StartsWith("<") && name.Contains(">"))
			{
				int num = name.IndexOf('>');
				if (num > 1)
				{
					name = name.Substring(1, num - 1);
				}
			}
			return name;
		}

		// Token: 0x04000060 RID: 96
		public readonly VisualElementLoader _visualElementLoader;
	}
}
