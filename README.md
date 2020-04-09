# DremDOS - a tiny disk operating system built with COSMOS

## What is DremDOS?
DremDOS is a basic disk operating system that is built with COSMOS, a set of building blocks to make operating systems with high-level programming languages such as C#, VB, F#, etc.

<img src="https://raw.githubusercontent.com/sparrdrem/dremdos/master/docs/0.0.1-RC1.PNG" />

## DremDOS's Features
DremDOS is currently very basic and is unable to do much. It can't even do some basic disk management things (due to bugs or, in the case of copying and moving, because they are not yet implimented. Maybe in 0.0.1-RC2) or load external programs. 
However, these are it's current features:

- Create and delete directories
- List directory contents
- Change the current directory
- Create and edit files with Kitty, which is not a reference to the Linux command "cat" at all
- Delete files
- Clear the screen
- Check the time
- Calculate basic math problems
- The ability to use every CPU cycle to beep
- List all\* connected drives
- Shut down the computer once you're bored of it

\*It seems COSMOS only supports one drive though. Technically the code should allow DremDOS to see multiple drives once the filesystem/hard drive drivers are fully implimented.

## Building DremDOS
To build it, please go to [their website](https://www.gocosmos.org/download/) and install the UserKit and DevKit\*\*.

\*\* DevKit is technically not required right now, but will be required in a later release.


Better documentation coming soon.
