using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ShellBoost.Core;
using ShellBoost.Core.WindowsShell;

namespace DragDropViewer
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            AllowDrop = true;
            labelHint.BackColor = textBoxDragZone.BackColor;
            textBoxDragZone.ContextMenuStrip = contextMenuStripZone;
        }

        protected override void OnResize(EventArgs e)
        {
            labelHint.Left = (Width - labelHint.Width) / 2;
            labelHint.Top = (Height - labelHint.Height) / 2;
            base.OnResize(e);
        }

        protected override void OnDragEnter(DragEventArgs e)
        {
            labelHint.Visible = false;
            e.Effect = DragDropEffects.Copy;
            DumpDataObject(e.Data);
            base.OnDragEnter(e);
        }

        private void DumpDataObject(IDataObject dataObject)
        {
            var sb = new StringBuilder();
            if (dataObject != null)
            {
                foreach (var format in dataObject.GetFormats())
                {
                    try
                    {
                        DumpFormat(dataObject, sb, format);
                        sb.AppendLine();
                    }
                    catch (Exception e)
                    {
                        sb.AppendLine(" *** Error with format '" + format + "': " + e.Message);
                        sb.AppendLine();
                    }
                }
            }
            textBoxDragZone.Text = sb.ToString();
        }

        private static void DumpFormat(IDataObject dataObject, StringBuilder sb, string format)
        {
            sb.AppendLine(format);

            if (format == ShellDataObjectFormat.CFSTR_FILECONTENTS)
            {
                sb.AppendLine(" <parsed with " + ShellDataObjectFormat.CFSTR_FILEDESCRIPTORW + "> ");
                return;
            }

            if (format == ShellDataObjectFormat.CFSTR_FILEDESCRIPTORW)
            {
                var files = ShellDataObjectFormat.GetCFSTR_FILECONTENTS(dataObject);
                if (files.Count > 0)
                {
                    for (var i = 0; i < files.Count; i++)
                    {
                        var file = files[i];
                        sb.AppendLine(" file#" + i + ": " + file.Descriptor.cFileName + " (" + file.Stream?.Length + " bytes)");
                    }
                    return;
                }
            }

            var data = dataObject.GetData(format);
            if (data == null)
            {
                sb.AppendLine(" <nothing>");
                return;
            }

            if (ShellDataObjectFormat.TryParseDataUsingKnownFormats(format, data, out var obj))
            {
                if (obj is IReadOnlyList<ShellItemIdList> idls)
                {
                    for (var i = 0; i < idls.Count; i++)
                    {
                        var idl = idls[i];
                        sb.AppendLine(" idl#" + i + ": " + idl.GetPath());
                    }
                }
                else if (obj is IReadOnlyList<FILEDESCRIPTOR> descs)
                {
                    for (var i = 0; i < descs.Count; i++)
                    {
                        var desc = descs[i];
                        sb.AppendLine(" desc#" + i + ": " + desc);
                    }
                }
                else if (obj is IList list)
                {
                    foreach (var l in list)
                    {
                        sb.AppendLine(" obj: " + l);
                    }
                }
                else
                {
                    sb.AppendLine(" " + obj + " (" + (obj?.GetType().Name) + ")");
                }
            }
            else
            {
                if (data is MemoryStream ms && ms.Length == 4)
                {
                    var value = BitConverter.ToInt32(ms.ToArray(), 0);
                    sb.AppendLine(" " + GetDataString(data) + " (" + (data?.GetType().Name) + "): " + value + " 0x" + value.ToString("X8"));
                }
                else if (data is MemoryStream ms2 && ms2.Length == 8)
                {
                    var value = BitConverter.ToInt64(ms2.ToArray(), 0);
                    sb.AppendLine(" " + GetDataString(data) + " (" + (data?.GetType().Name) + "): " + value + " 0x" + value.ToString("X16"));
                }
                else
                {
                    sb.AppendLine(" " + GetDataString(data) + " (" + (data?.GetType().Name) + ")");
                }
            }
        }

        private static string GetDataString(object data)
        {
            if (data == null)
                return "<null>";

            if (data is string[] strings)
                return string.Join(" | ", strings);

            if (data is MemoryStream ms)
                return "len:" + ms.Length;

            return string.Format("{0}", data);
        }

        private void ToolStripMenuItemShowClipboard_Click(object sender, EventArgs e)
        {
            labelHint.Visible = false;
            DumpDataObject(Clipboard.GetDataObject());
        }
    }
}
