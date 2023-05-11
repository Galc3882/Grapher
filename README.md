# Grapher: A 3D Graph Drawing Tool

## Introduction

Grapher is a Windows Forms application developed in C# using Visual Studio. It allows users to input mathematical expressions and generate corresponding 3D graphs. The tool includes a parser that can handle functions, variables, and constants, as well as basic arithmetic operations and trigonometric functions.

## Table of Contents

- [Explanation of project/technology and code](#explanation-of-projecttechnology-and-code)
- [Special considerations](#special-considerations)
- [Examples](#examples)
- [Requirements and file architecture](#requirements-and-file-architecture)
- [Instructions](#instructions)
- [Next steps](#next-steps)

## Explanation of project/technology and code

Grapher is a 3D graph drawing tool that utilizes C# programming language and Windows Forms development platform. The tool is designed to allow users to input mathematical expressions and generate corresponding 3D graphs. The parser in the tool is designed to handle variables, constants, functions, basic arithmetic operations, and trigonometric functions.

The code is organized into classes that correspond to different components of the application, such as the graphing area, the parser, and the UI elements. The tool also includes error handling to ensure that user input is properly parsed and that the application does not crash.

## Special considerations

One special consideration for this tool is the handling of complex mathematical expressions. To ensure that the parser can handle complex expressions, it was necessary to create a robust set of rules and use a variety of programming techniques to ensure that the parser works correctly.

Another special consideration is the performance of the tool. Generating 3D graphs can be computationally intensive, and so the tool was designed to be efficient and to use as few system resources as possible.

## Examples

![image](https://user-images.githubusercontent.com/86870298/124349266-ed068f80-dbf6-11eb-954d-fb168aa81b26.png)
![image](https://user-images.githubusercontent.com/86870298/124349278-fb54ab80-dbf6-11eb-8669-56ae038ea936.png)

## Requirements and file architecture

### Imports:

- Windows Forms development platform

### File Architecture:

```
├── graph
│   ├── graph.csproj
│   └── ...
├── graph.sln
├── README.md
```

## Instructions

To use the Grapher tool, simply enter a mathematical expression into the input box and click "Graph". The tool will then generate a 3D graph of the expression. Users can rotate the graph and change its viewing angle.

## Next steps

- [ ] Add support for additional mathematical functions
- [ ] Add support for exporting graphs to other file formats
- [ ] Improve performance


