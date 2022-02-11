# NOTICE
This repo has been moved to [my Gitea server](https://git.innovation-inc.org/Innovation/dremdos/). Please go there for updated code!

# DremDOS - a tiny disk operating system built with COSMOS

## What is DremDOS?
DremDOS is a simple disk operating system made with C# that is built with COSMOS, a set of building blocks to make operating systems with high-level programming languages such as C#, VB, F#, etc.

<img src="https://raw.githubusercontent.com/sparrdrem/dremdos/master/docs/0.0.1.PNG" />

## DremDOS's Features
DremDOS is currently very basic and is unable to do much.<br />
However, these are it's current features:

- Create and delete directories
- List directory contents
- Change the current directory
- Create and edit files with Kitty, which is not a reference to the Linux command "cat" at all
- Copy and move files
- Delete files
- Clear the screen
- Check the time
- Calculate basic math problems
- The ability to use every CPU cycle to beep
- List all\* connected drives
- A very basic GUI (with a mouse driver). Thanks SparrOSDeveloperTeam!
- Shut down the computer once you're bored of it

\*It seems COSMOS only supports one drive though. Technically the code should allow DremDOS to see multiple drives once the filesystem/hard drive drivers are fully implimented.

## Building DremDOS
Building DremDOS is simple.

1. Go to [COSMOS's website](https://www.gocosmos.org/download/) and install the UserKit and DevKit\*\*. Every prerequisite there is a prerequisite for DremDOS!
2. Download this repository and copy everything in the "src" folder to a new folder named DremDOS\*\*\*. It should be located at `C:\Users\[your username]\sources\repos\DremDOS`.
3. Open the project in Visual Studio 2019. Modify it as you like and build it. The ISO will be outputted in the `PATH\TO\DremDOS\DremDOS\bin\Debug\netcoreapp2.0\cosmos` folder and be named `DremDOS.iso`.

\*\* DevKit is technically not required right now, but will be required in a later release.<br />
\*\*\* This is only needed if you want to edit DremDOS as your own. If you just want to build it, you should be able to just open the solution file and build it. This is untested, though.

Better documentation coming soon.
