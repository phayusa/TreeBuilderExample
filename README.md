# TreeBuilderExample

Example of Builder for a Tree.
This example can be used in any project.
Simply add the builder (TreeBuilder.cs) to your project and adapt it to your tree

## Requirements

* `.Net Framework 4.5`
* `Windows (obviously)`

## Run

Build the project and launch the binary to have an example.

## Explanation

### Data format
In the example, we have an array of String as data to create the tree.
One string is characters which symbolise the depth of the element.
An array of correpondance contain the depth of the characters.

### Example Data
* `String[] testData = { "A", "B", "B","A", "B", "A" };`
* `The correpondance array can be : { {"A"}, {"B"}}`

### Tree Creation
The code are well comented so the next explanation will be simple.

#### Deep way (result method)
Create node by node.

#### Large way (resultLarge method)
Create node by level.
