using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace treeTest
{
    class TreeBuilder
    {
        private TreeNode rootTree;
        private String[] data;
        private List<List<String>> headers;
        private List<String> dataType;
        private int nbData;
        private int nbHeader;
        private int lastData;


        public TreeBuilder(String[] _data, List<List<String>> depthCorrepondance) : this(_data, depthCorrepondance, null)
        {
        }

        public TreeBuilder(String[] _data, List<List<String>> depthCorrepondance, List<String> extraInfo)
        {
            // Create the tree
            rootTree = new TreeNode();
            // Assigned the data and headers in the builder
            data = _data;
            headers = depthCorrepondance;
            // Setting counting elements to optimize the processing
            nbData = new List<String>(_data).Count;
            nbHeader = headers.Count;
            dataType = extraInfo;

            lastData = -1;
        }

        // Build the tree with these recursive function by deep way
        private void TreeProcess(TreeNode node, int index, int depth)
        {
            // Exit condition
            if (index <= lastData || index >= nbData)
                return;

            // If we find the a corresdance at the current level
            if (depth < nbHeader && headers[depth].Contains(data[index]))
            {
                // We create a new children
                TreeNode children = AddChildren(node, data[index]);
                lastData = index;
                // Recursive call with the children
                TreeProcess(children, index + 1, depth + 1);

                // The new node can be at a depth upper
                if (node.Parent != null)
                    TreeProcess(node.Parent, index + 1, depth - 1);
                //The new node can be at the same level
                TreeProcess(node, index + 1, depth);
            }
            else
            {
                if (dataType.Contains(data[index]))
                {
                    // We create a new children
                    TreeNode children = AddChildren(node, data[index]);
                    lastData = index;
                    // Recursive call with the children
                    TreeProcess(children, index + 1, depth + 1);

                    // The new node can be at a depth upper
                    if (node.Parent != null)
                        TreeProcess(node.Parent, index + 1, depth - 1);
                    //The new node can be at the same level
                    TreeProcess(node, index + 1, depth);
                }
                else
                    // Maybe the node is at an higher depth than before
                    if (node.Parent != null)
                    TreeProcess(node.Parent, index, depth - 1);
            }
        }

        private TreeNode AddChildren(TreeNode node, String data)
        {
            if (data == "")
                return null;
            // We create a new children
            TreeNode newNode = new TreeNode();
            // We assigned it's data
            newNode.Text = data;
            // We linked it with it's parent
            node.Nodes.Add(newNode);

            return newNode;
        }

        // Large processing
        private void TreeProcess(TreeNode node, List<String> datas, int depth)
        {
            // Stock the data of the treeNode
            String prevData = "";
            // Children of the current node
            List<String> dataChildren = new List<String>();
            bool first = true;

            // If the depth is higher than the number of header we go out
            if (depth > nbHeader)
                return;

            // For all data (so all potential children)
            foreach (String oneData in datas)
            {
                // If we find a data at our current depth
                if (headers[depth].Contains(oneData))
                {
                    if (!first)
                    {
                        // We add it's data and it's children
                        TreeNode childNodes = AddChildren(node, prevData);

                        // If the node have children 
                        if (dataChildren.Any())
                            // Do a recursive call
                            TreeProcess(childNodes, new List<String>(dataChildren), depth + 1);
                        // We clear the old children we get
                        dataChildren.Clear();
                    }
                    // We stock the data
                    prevData = oneData;
                    if (first)
                        first = false;
                }
                else
                {
                    if (dataType.Contains(oneData) && depth != 0)
                    {
                        // We add it's data and it's children
                        TreeNode childNodes = AddChildren(node, prevData);

                        // If the node have children 
                        if (dataChildren.Any())
                            // Do a recursive call
                            TreeProcess(childNodes, new List<String>(dataChildren), depth + 1);
                        // We clear the old children we get
                        dataChildren.Clear();

                        // We stock the data
                        prevData = oneData;
                        first = false;
                    }
                    else
                        // If the data is not at our level 
                        // It is probably a children
                        dataChildren.Add(oneData);
                }
            }

            // We need to add the last element (we skip the first one)
            TreeNode childNode = AddChildren(node, prevData);
            // If the node have children 
            if (dataChildren.Any())
                // Do a recursive call
                TreeProcess(childNode, new List<String>(dataChildren), depth + 1);
        }

        // If you have high deep this one is faster
        public TreeNode result()
        {
            // Call the recursive function with default arguments by deep way
            TreeProcess(rootTree, 0, 0);
            return rootTree;
        }

        // If you have a lot of node with a low deep these one is faster
        public TreeNode resultLarge()
        {
            // Call the recursive function with default arguments by large way
            TreeProcess(rootTree, data.ToList<String>(), 0);
            return rootTree;
        }
    }
}
