# DataMatrix for Windows written in C# (.NET6)

## Application description:

With this program, or rather library, you can create DataMatrix, in .NET6 C# the idea, that is the inspiration, came from DvorakDwarf and his associated repo is (https://github.com/DvorakDwarf/Infinite-Storage-Glitch).

It's not as developed as much as his yet. But let's see how far I get.

## How the System Works

### DataMatrix

![DataMatrix](DataMatrix_Image.png)

1. All pixels are read (from top left to bottom right) and the black pixels `(255,0,0,0)` are assigned a value of `"1"`, and the white pixels `(255,255,255,255)` are assigned a value of `"0"` (binary). All other pixels are simply ignored.

    Output: `"0101 0011 0100 0111 0101 0110 0111 0011 0110 0010 0100 0111 0011 1000 0110 1000"`

2. After that, the binary chain is encoded to base64.

    Output: `"SGVsbG8h"`

### With a String:

3. Finally, the base64 string is decoded to an ASCII phrase.

    Output: `"Hello!"`

### With a File:

3. Finally, the base64 string is converted to a byte array and saved as a file.


## Features:

✔️ You can create a DataMatrix where the content is a string (DataMatrix grows dynamically, but don't know when it stops)<br/>
✔️ You can create a DataMatrix where the content is a file (it's damn slow and inefficient)<br/>
✔️ You can read your generated DataMatrix. (Currently supports only those from this program)<br/>

# CHANGELOG

## 1.0.0.0
- DataMatrix can be generated (content: string)
- DataMatrix can be read (content: string)

## 1.1.0.0
- DataMatrix can be generated (content: file) (it's damn slow and inefficient)

## 1.1.1.0
- The console inputs are now handled better (expectation handling)
