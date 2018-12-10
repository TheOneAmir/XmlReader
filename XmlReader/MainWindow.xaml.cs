using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Xml;
using System.Xml.Linq;

namespace XmlReader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //public TextBox XmlKey;
        private string FILE_PATH = "this.xml";
        private static DataSet _dsSet;
        private static System.Xml.XmlReader reader; 

        public MainWindow()
        {
            InitializeComponent();
        }

        public void ParseXMLReader(object o, EventArgs e)
        {
            if (reader == null)
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.IgnoreWhitespace = true;
            }

            using (reader = System.Xml.XmlReader.Create(FILE_PATH))
            {
                var sb = new StringBuilder();
                while (reader.Read())
                {
                    reader.MoveToContent();
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        if (reader.Name == XmlKey.Text)
                        {
                            XElement el = XNode.ReadFrom(reader) as XElement;
                            if (el != null)
                            {
                                foreach (XElement node in el.DescendantNodes().Where(w => w is XElement))
                                {
                                    //var v = node.CreateReader();
                                        sb.Append(((XElement)node).Name);
                                        sb.Append(": ");
                                        sb.Append(((XElement)node).Value);
                                        sb.Append("\n");
                                }
                            }

                            break;
                        }
                    }
                }

                XmlValues.Text = sb.ToString();
            }
        }

        public void ParseXML(object o, EventArgs e)
        {
            XmlValues.Text = "Loading...";
            if (_dsSet == null || _dsSet.Tables.Count == 0)
            {
                var xmlString = File.ReadAllText(FILE_PATH);
                var stringReader = new StringReader(xmlString);
                _dsSet = new DataSet();
                _dsSet.ReadXml(stringReader, XmlReadMode.InferSchema);
            }

            if (XmlKey.Text == "all")
            {
                var sb = new StringBuilder();
                var dataTables = new List<DataTable>();
                foreach (DataTable table in _dsSet.Tables)
                {
                    dataTables.Add(table);
                    sb.Append(table.TableName);
                    sb.Append(": ");
                    sb.Append("\n");
                }

                XmlValues.Text = sb.ToString();
            }
            else
            {
                var table = _dsSet.Tables[XmlKey.Text];
                var sb = new StringBuilder();
                foreach (DataRow row in table.Rows)
                {
                    foreach (var item in row.ItemArray)
                    {
                        sb.Append(item);
                        sb.Append(", ");
                    }
                    sb.Append("\n");
                }

                XmlValues.Text = sb.ToString();
            }

            return;
        }
    }
}
