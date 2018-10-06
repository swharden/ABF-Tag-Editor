# ABF Tag Editor

**ABF Tag Editor is a program created for scientists to easily add and edit comment tags in ABF (Axon Binary Format) files.** ABF files are typically recorded with software like ClampEx and edited with software like ClampFit, but these packages do not allow tags to be added or modified after ABF files have been recorded. Since comment tags are often used to store important experiment information, it is critical that the scientist maintain the ability to correct erroneous tag comments or tag times, and to add additional tags as analytical needs evolve.

![](doc/screenshot.png)

## Download
A click-to-run EXE version of this program can be downloaded on the [Releases](https://github.com/swharden/ABF-Tag-Editor/releases) page:
* [ABFtagEditor.exe](https://github.com/swharden/ABF-Tag-Editor/releases/download/1.0.0/ABFtagEditor.exe) (v1.0.0)

## Features
* ABF1 and ABF2 files are supported. 
* An original ABF file backup is created automatically before an ABF is modified.

## Limitations
* Only comment tags are supported (not voice tags)
* Tags can only be modified (not created or deleted)

## Technical Notes and Resources
* The source code is fully self-contained. Although a vsABF library exists for full-featured support to read ABF file header and data, it was not used in an effort to simplify maintenance of this program. All ABF file reading and writing is performed by [AbfTagEdit.cs](src/ABFtagEditor/ABFtagEditor/AbfTagEdit.cs).
* [pyABF](https://github.com/swharden/pyABF) - An ABF reader for Python
* [vsABF](https://github.com/swharden/vsABF) - An ABF reader for Visual Studio (written in C#)
* [Unofficial Guide to the ABF File Format](https://github.com/swharden/pyABF/tree/master/docs/advanced/abf-file-format)

## Author
Scott W Harden, D.M.D., Ph.D.\
President & CEO\
[Harden Technologies, LLC](https://tech.swharden.com)
