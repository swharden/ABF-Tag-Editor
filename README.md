# ABF Tag Editor

**ABF Tag Editor is a program created for scientists to easily add and edit comment tags in ABF (Axon Binary Format) files.** ABF files are typically recorded with software like ClampEx and edited with software like ClampFit, but these packages do not allow tags to be added or modified after ABF files have been recorded. Since comment tags are often used to store important experiment information, it is critical that the scientist maintain the ability to correct erroneous tag comments or tag times, and to add additional tags as analytical needs evolve.

![](doc/screenshot.png)

## Download
A click-to-run EXE version of this program can be downloaded on the [Releases](https://github.com/swharden/ABF-Tag-Editor/releases) page:
* [ABFtagEditor.exe](https://github.com/swharden/ABF-Tag-Editor/releases/download/1.1.0/ABFtagEditor.exe) (v1.1.0)

## Features
* ABF1 and ABF2 files are supported.
* Episodic and gap-free recordings are supported
* The original ABF file is never modified (a backup is created automatically)
* An original ABF file backup can be restored at any time
* Tags can be deleted from ABF files
* Tags can be added to files which have no tags
* One-click loading of ABF file into ClampFit for inspection

## Limitations
* Only comment tags are supported (not voice tags)
* Tag times can become distorted when
  * gaps exist between sweeps
  * tags are added to ABF files containing no tags
  
## Technical Notes and Resources
* ABF file reading and writing is performed by [AbfTagEdit.cs](src/ABFtagEditor/ABFtagEditor/AbfTagEdit.cs) (a solution not based on external DLLs).
* [pyABF](https://github.com/swharden/pyABF) - An ABF reader for Python
* [ABFsharp](https://github.com/swharden/ABFsharp) - An ABF reader for Visual Studio (written in C#)
