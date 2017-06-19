using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace treeTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //String[] testData = { "A", "H","B", "B", "C","D","H","B1", "B2", "A", "B", "A" };
            String[] testData = { "A", "H", "B", "B", "C", "D", "H","B1", "B2","H", "A", "B", "A", "H"};

            List<List<String>> testHeaders = new List<List<String>>();
            testHeaders.Add(new List<String>());
            testHeaders.Add(new List<String>());
            testHeaders.Add(new List<String>());
            testHeaders.Add(new List<String>());

            testHeaders[0].Add("A");
            testHeaders[1].Add("B");
            testHeaders[1].Add("B1");
            testHeaders[1].Add("B2");
            testHeaders[2].Add("C");
            testHeaders[3].Add("D");

            List<String> extraHeader = new List<String>();
            extraHeader.Add("H");
            extraHeader.Add("H2");

            foreach (TreeNode node in new TreeBuilder(testData, testHeaders,extraHeader).resultLarge().Nodes){
                treeView1.Nodes.Add(node);
            }
            //this.treeView1.Nodes.Add()
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }
    }
}
