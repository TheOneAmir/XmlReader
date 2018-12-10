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
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        public MainWindow()
        {
            InitializeComponent();
        }

        public void ParseXML(object o, EventArgs e)
        {
            XmlValues.Text = "Loading...";
            if (_dsSet == null || _dsSet.Tables.Count == 0)
            {
                var xmlString = File.ReadAllText(FILE_PATH);
                var stringReader = new StringReader(xmlString);
                _dsSet = new DataSet();
                _dsSet.ReadXml(stringReader);
            }

            var values = _dsSet.Tables[XmlKey.Text];
            var toDisplay = XmlKey.Text;
            XmlValues.Text = Convert.ToString(values);

            return;
        }
    }
}
