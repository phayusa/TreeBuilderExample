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
        private int nbData;
        private int nbHeader;

        
        public TreeBuilder(String[] _data, List<List<String>> depthCorrepondance)
        {
            // Create the tree
            rootTree = new TreeNode();
            // Assigned the data and headers in the builder
            data = _data;
            headers = depthCorrepondance;
            // Setting counting elements to optimize the processing
            nbData = new List<String>(_data).Count;
            nbHeader = headers.Count;
        }

        // Build the tree with these recursive function by deep way
        private void TreeProcess(ref TreeNode node, int index, int depth)
        {
            // Processing all the data
            // Maybe we can optimize by adding static variable to avoid to repass by seen element
            for (int i = index; i < nbData; i++)
            {
                // If we find the a corresdance at the current level
                if (depth < nbHeader && headers[depth].Contains(data[i]))
                {
                    // We create a new children
                    TreeNode children = new TreeNode();
                    children.Text = data[i];
                    // We linked it with it's parent
                    node.Nodes.Add(children);
                    // Recursive call with the children
                    TreeProcess(ref children, i + 1, depth + 1);
                }
                else
                {
                    // If it has a parent 
                    if (node.Parent != null)
                        for (int indexPrevDepth = 0; indexPrevDepth < depth; indexPrevDepth++)
                        {
                            if (headers[indexPrevDepth].Contains(data[i]))
                            {
                                // Go out of the recursive call (only the first level of the tree can have childrens)
                                return;
                            }
                        }
                }
            }
        }

        private void AddChildren(ref TreeNode node, String data, List<String> childrens, int nextDepth) {
            // We create a new children
            TreeNode newNode = new TreeNode();
            // We assigned it's data
            newNode.Text = data;
            // We linked it with it's parent
            node.Nodes.Add(newNode);

            // If the node have children 
            if (childrens.Any())
                // Do a recursive call
                TreeProcess(ref newNode, childrens,nextDepth);
        }

        // Large processing
        private void TreeProcess(ref TreeNode node, List<String> datas, int depth)
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
                if (depth < nbHeader && headers[depth].Contains(oneData))
                {
                    if (!first)
                    {
                        // We add it's data and it's children
                        AddChildren(ref node, prevData, new List<String>(dataChildren), depth + 1);
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
                    // If the data is not at our level 
                    // It is probably a children
                    dataChildren.Add(oneData);
                }
            }

            // We need to add the last element (we skip the first one)
            AddChildren(ref node, prevData, new List<String>(dataChildren), depth + 1);
        }

        // If you have high deep this one is faster
        public TreeNode result()
        {
            // Call the recursive function with default arguments by deep way
            TreeProcess(ref rootTree, 0, 0);
            return rootTree;
        }

        // If you have a lot of node with a low deep these one is faster
        public TreeNode resultLarge()
        {
            // Call the recursive function with default arguments by large way
            TreeProcess(ref rootTree, data.ToList<String>(), 0);
            return rootTree;
        }
    }
}
