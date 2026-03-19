using System;
using System.Reflection;
using UnityEngine.UIElements;

namespace Timberborn.DebuggingUI
{
	// Token: 0x02000012 RID: 18
	public class ObjectViewerFieldNode : ObjectViewerNode
	{
		// Token: 0x06000069 RID: 105 RVA: 0x00003989 File Offset: 0x00001B89
		public ObjectViewerFieldNode(VisualElement root, FieldInfo nodeFieldInfo, ObjectViewerNodeFactory objectViewerNodeFactory, TextField textField) : base(objectViewerNodeFactory, root, nodeFieldInfo)
		{
			this._textField = textField;
		}

		// Token: 0x0600006A RID: 106 RVA: 0x0000399C File Offset: 0x00001B9C
		public override void Update(object parent)
		{
			base.Update(parent);
			BaseField<string> textField = this._textField;
			object value = base.Value;
			textField.SetValueWithoutNotify(((value != null) ? value.ToString() : null) ?? "(null)");
		}

		// Token: 0x04000055 RID: 85
		public readonly TextField _textField;
	}
}
