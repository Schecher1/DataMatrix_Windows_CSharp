# DataMatrix for Windows written in C# (.NET6)

## Application description:

With this program, or rather library, you can create DataMatrix, in .NET6 C# the idea, that is the inspiration, came from DvorakDwarf and his associated repo is (https://github.com/DvorakDwarf/Infinite-Storage-Glitch).

It's not as developed as much as his yet. But let's see how far I get.

## Features:

✔️ You can create a DataMatrix (if the file is larger, then it will be more than one) where the content is a file (even big files)


# CHANGELOG

## 1.0.0.0
- DataMatrix can be generated (content: string)
- DataMatrix can be read (content: string)

## 1.0.1.0
- DataMatrix can be generated (content: file)

## 2.0.0.0
- Libv02 added ( only File To DataMatrix until now ):
  - Multi Threading
  - More efficient
  - Faster
  - consumes much less ram
  - more dynamically adjustable
  - supports also very large files
  - no XXL DataMatrixes are created but several small ones (will be summarized later as a video)
