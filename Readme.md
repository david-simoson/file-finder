# Finder

The purpose of this project is to allow the user to easily search directories for files containing search strings. Both plain strings and Regex are accepted.

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

What things you need to install the software and how to install them

```
This application was developed targetting .NET 4.6.1. 
While it has not been tested on previous versions it does not contain features which should prevent it from working on Windows systems with earlier versions of .NET installed. 
```

### Installing

In order to get Finder running on your system...

First clone the repository and build.

Next take the created executable and place it in a directory of your choosing

```
This would be a directory where you would store utilities and such, or you could optionally keep it in the project directory
```

Next Add the directory containing the Finder executable to the list of PATH environment variables in Windows

You should now be able to run finder from any directory on your PC

### Usage

Open command prompt in directory where search is intended

Type finder [searchString] [args]

```
finder mySearchString
```
Will return all files which contain the string "mySearchString"

```
finder -h
```
Additional usage information available in inline "help" documentation