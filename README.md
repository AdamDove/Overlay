# Overlay
After running into problems with an older version of Rainmeter, I decided it was finally time to write my own set of desktop widgets. Note: simply upgrading to a newer version of Rainmeter cleared that up, but I wanted an excuse.

This project is very much a work in progress and basically a sandbox for me to play in.
I wanted to try a few things out and this seemed like a good side project to do just that.

The goal is to complete one simple task or subtask every night I sit down at my computer (I'd guess anywhere from 1-5 times a week).  These tasks can range anywhere from working on the view of an existing widget, creating a new widget, working on the project framework or configuration, or even tinkering with Git.  Basic goal is to make small incremental progress or learn something everytime I sit down.

My basic plan... Create a set of widgets hosted in a single process, clean, low resource consumption.

Widgets:
 * Clock (Basic Date/Time)
 * Drive (Physical drive space)
 * Sound Switcher (Change between my specific Audio Devices at the push of a button)
 * Volume Slider
 * CPU Usage (Overall % and possibly top 5-10 processes?)
 * Memory Usage
 * Network Up/Down
 
First commit to GitHub contains the first weeks worth of work, including:
 * a very rough MVVM framework
 * first cut at Clock and Drive Widgets (both need some View work)
 * Show NotifyIcon instead of in Taskbar
 * OnNotifyIconClick brings up an empty window that will be used for widget settings and to exit (window hides on loss of focus)
 * Widgets are draggable
 * Widget position is saved to User config on mouse up, and position is restored on next load.
 * Target Framework is now 4.6 as this is included natively in Windows 10 (I've had some interest in this from friends and family)
