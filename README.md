# IELTS Test App

**IELTS Test App** is a personal project built to simulate the official IELTS Writing test environment. It provides a distraction-free, feature-restricted space to help candidates improve their writing skills under realistic exam conditions.

## Purpose

This application aims to replicate the **IELTS Academic Writing module** in terms of format, timing, and writing restrictions, offering a focused platform for self-practice and preparation.

---

## Features

### Implemented

- **English-only** input support  
- Real-time **word count** display  
- Adjustable **font size** with a slider  
- Built-in **countdown timer** with reset  
- **Fullscreen mode** to eliminate distractions  
- **Dark / Light mode** toggle for comfort  
- Disable **text suggestions, autocorrect, spellcheck, predictive input**  
- Block **Vietnamese input** from common IMEs  
- **Auto-save** writing every 10 seconds

### Planned Features

- Block **copy, paste, and text selection**  
- **Topic selector** and **typing box** for Writing Task 1 and Task 2 separately
- **Export**  responses to `.txt` file  
- **Track writing** history and session performance  
- **Print** directly from the app
- **AI** intergrated to assess the writing
- Intergrate mechanism **blocking switching app** during session

---

## Technology Stack

- **Language**: C#  
- **Framework**: .NET 8  
- **UI Framework**: Windows Presentation Foundation (**WPF**)  
- **Target Platform**: Windows (x64)

> This stack allows for high-performance, smooth UI rendering, and deep OS-level integration — ideal for building lightweight and fast native Windows desktop applications.

---

## Installation & Usage

### Installation

If no modification is intended to made, you can install the app directly from [*Releases*](https://github.com/hieunguyenarc03/IELTSTestApp/releases) on github.

This is a standalone Windows application. If you modify the source code, you shall build the source again to generate program, at this point dotnet is required. Once built, you can run the `.exe` directly — no installation required.

This Bash command builds and publishes a self-contained, single-file .NET application for Windows 64-bit

```bash
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true
```

### Usage

<img width="1920" height="1080" alt="Image" src="https://github.com/user-attachments/assets/a6c14708-bd3c-4958-a988-19b2a93462d8" />
*App Screenshot*

---

## Future Vision

While the current focus is on replicating the writing test environment, the long-term goal is to support:

- Listening and Reading mock environments  
- Speaking timer + question prompts  
- Cloud sync and account-based progress tracking  
- Integration with IELTS writing scoring tools  

---

## Author

Developed by **Hieu Nguyen**, a Computer Engineering student passionate about educational software and real-world applications of technology.

---

## License

This project is currently closed-source for development and testing purposes. Future releases may be published under a suitable open-source license.
